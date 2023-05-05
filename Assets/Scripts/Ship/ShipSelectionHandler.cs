using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Spawn ships, setup ship data and ship quaternion
    /// </summary>
    public sealed class ShipSelectionHandler : ShipSelection
    {
        [SerializeField] private List<ShipObject> shipPrefabs;
        [SerializeField] private Transform shipContainer;

        private readonly List<ShipObject> _shipSpawnedObjects = new ();

        protected override void OnInitialize()
        {
            base.OnInitialize();

            SpawnShips();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            motionEnabled = true;
        }

        private void SpawnShips()
        {
            for (var i = 0; i < ShipDataList.Count; i++)
            {
                var shipData = ShipDataList[i];

                var ship = Instantiate(shipPrefabs[i], shipContainer);
                ship.currSpec = new ShipSpec
                {
                    sId = shipData.spec.sId,
                    sName = shipData.spec.sName,
                    sClass = shipData.spec.sClass,
                    sVar = shipData.spec.sVar
                };
                ship.VariantsList.AddRange(GetShipVariants(shipData.spec.sId));
                _shipSpawnedObjects.Add(ship);
            }

            SetupShipWithSpecs();

            var shipSelectionUI = sceneContext.Get<UIShipSelection>();
            shipSelectionUI.GenerateUiItems(_shipSpawnedObjects);
        }

        private void SetupShipWithSpecs()
        {
            var newSpec = Utils.GetCurrShipSpec();

            var shipObjNew = GetShipObject(newSpec.sId);

            shipObjNew.currSpec = new ShipSpec
            {
                sId = newSpec.sId,
                sName = newSpec.sName,
                sClass = newSpec.sClass,
                sVar = newSpec.sVar
            };

            shipObjNew.LoadShip(shipObjNew.currSpec);
        }

        // Update is called once per frame
        private void Update()
        {
            if (motionEnabled)
                shipContainer.Rotate(new Vector3(0, .1f, 0));
        }

        #region Helper Functions
        
        internal void DeactivateAllShips()
        {
            foreach (var ship in _shipSpawnedObjects)
            {
                ship.isActive = false;
                ship.gameObject.SetActive(false);
            }
        }

        private List<ShipVariant> GetShipVariants(string id)
        {
            return sceneContext.variantSelectionHandler.ShipVariantDataList.Find(sh => sh.shipId.Equals(id))
                .shipVariantList;
        }

        internal ShipObject GetShipObject(string sId)
        {
            var shipObject = _shipSpawnedObjects.Find(v => v.currSpec.sId.Equals(sId));
            return shipObject;
        }

        internal ShipSpec GetShipSpec(string sId)
        {
            var shipObject = _shipSpawnedObjects.Find(v => v.currSpec.sId.Equals(sId));
            return shipObject.currSpec;
        }

        #endregion
    }
}
