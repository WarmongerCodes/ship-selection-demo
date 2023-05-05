using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ship
{
    /// <summary>
    /// selection class to load data for variants 
    /// </summary>
    public abstract class VariantSelection : SelectionBase
    {
        internal readonly List<ShipVariantData> ShipVariantDataList = new();

        protected override void OnInitialize()
        {
            base.OnInitialize();

            LoadShipsData();
        }

        private void LoadShipsData()
        {
            var load = Resources.LoadAll<ShipVariantData>($"ShipVariantsData/");

            foreach (var item in load.ToList())
            {
                ShipVariantDataList.Add(item);
            }
        }
    }
}
