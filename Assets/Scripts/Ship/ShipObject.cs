using UnityEngine;

namespace Ship
{
    /// <summary>
    /// Class handle for ship gameobject data, variant handler
    /// </summary>
    public sealed class ShipObject : ShipBase
    {
        public MeshRenderer meshRenderer;

        public void LoadShip(ShipSpec shipSpec)
        {
            currSpec = shipSpec;
            var variant = GetVariant(currSpec.sVar);
            LoadVariant(variant.material);
            Activate();
        }

        private void LoadVariant(Material mat)
        {
            meshRenderer.material = mat;
        }

        private ShipVariant GetVariant(ShipVar shipVar)
        {
            return VariantsList.Find(i => i.shipVar.Equals(shipVar));
        }
    }
}
