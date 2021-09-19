using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

namespace RPG.Inventories
{
    public class StatsEquipment : Equipment, IModifierProvider
    {
        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            throw new System.NotImplementedException();
        }
    }
}