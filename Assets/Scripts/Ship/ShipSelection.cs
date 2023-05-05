using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ship
{
    public abstract class ShipSelection : SelectionBase
    {
        [SerializeField] internal bool motionEnabled;
        internal readonly List<ShipData> ShipDataList = new();

        protected override void OnInitialize()
        {
            base.OnInitialize();

            LoadShipsData();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            motionEnabled = true;
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            motionEnabled = false;
        }

        private void LoadShipsData()
        {
            var load = Resources.LoadAll<ShipData>($"/");

            foreach (var item in load.ToList())
            {
                ShipDataList.Add(item);
            }
        }

        internal ShipData GetShipObjectData(string sId)
        {
            var shipData = ShipDataList.Find(v => v.spec.sId.Equals(sId));
            return shipData;
        }
    }
}
