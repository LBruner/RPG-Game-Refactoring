using System;
using RPG.Combat;
using RPG.Movement;
using RPG.Resources;
using UnityEngine;

namespace RPG.Control
{
    [SelectionBase]

    public class PlayerController : MonoBehaviour
    {
        Mover mover = null;
        Health health = null;

        enum CursorType
        {
            None,
            Movement,
            Combat
        }

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;

        void Awake()
        {
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            if (health.GetIsDead()) { return; }

            if (InteractWithCombat()) { return; };
            if (InteractWithMovement()) { return; };

            SetCursor(CursorType.None);
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) { continue; }

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }

                SetCursor(CursorType.Combat);
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                    mover.StartMoveAction(hit.point);

                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }


        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, new Vector2(0, 0), CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
