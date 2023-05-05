using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ship
{
    [CreateAssetMenu(fileName = "ShipAttachmentData", menuName = "ScriptableObjects/CreateShipAttachmentData",
        order = 1)]
    public class ShipVariantData : ScriptableObject
    {
        public string shipId;
        public List<ShipVariant> shipVariantList;
    }

    [Serializable]
    public struct ShipVariant
    {
        [SerializeField] internal ShipVar shipVar;
        [SerializeField] internal Material material;
    }
}
