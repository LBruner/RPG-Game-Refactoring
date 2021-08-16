using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        public enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = 1;
        [SerializeField] Transform spawnPosition;

        [SerializeField] DestinationIdentifier destination;

        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeWaitTime = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not defined");
            }

            DontDestroyOnLoad(this.gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return StartCoroutine(fader.FadeOut(fadeOutTime));

            FindObjectOfType<SavingWrapper>().Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            FindObjectOfType<SavingWrapper>().Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return StartCoroutine(fader.FadeIn(fadeInTime));

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            if (otherPortal != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.GetComponent<NavMeshAgent>().Warp(otherPortal.GetSpawnPosition().position);
                player.transform.rotation = otherPortal.GetSpawnPosition().rotation;
            }
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) { continue; }
                if (portal.destination != this.destination) { continue; }

                return portal;
            }
            return null;
        }

        public Transform GetSpawnPosition()
        {
            return spawnPosition;
        }

    }
}
