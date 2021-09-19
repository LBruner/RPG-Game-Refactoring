using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    using RPG.Stats;
    using UnityEngine;

    [CreateAssetMenu(menuName = "RPG/Inventory/Equipable Item")]

    public class StatsEquipableItem : EquipableItem
    {
        [SerializeField]
        Modifier[] addtiveModifiers;
        [SerializeField]
        Modifier[] percentageModifiers;

        [System.Serializable]
        struct Modifier
        {
            public Stat stat;
            public float value;
        }
    }
}