using System;
using UnityEngine;

namespace Ship
{
    [CreateAssetMenu(fileName = "ShipData", menuName = "ScriptableObjects/CreateShipData", order = 1)]
    [Serializable]
    public class ShipData : ScriptableObject
    {
        [SerializeField] internal ShipSpec spec;
    }
}
