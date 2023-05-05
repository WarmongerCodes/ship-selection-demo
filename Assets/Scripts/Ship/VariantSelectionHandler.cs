namespace Ship
{
    /// <summary>
    /// main class to load selected ship and change variant
    /// </summary>
    public sealed class VariantSelectionHandler : VariantSelection
    {
        private ShipObject _currShipObject;
        internal ShipSpec ShipSpec { get; set; }

        public void LoadCurrentShip()
        {
            ShipSpec = Utils.GetCurrShipSpec();
            _currShipObject = sceneContext.shipSelectionHandler.GetShipObject(ShipSpec.sId);
        }

        public void UpdateShipVariant(int index)
        {
            ShipSpec.sVar = (ShipVar)index;
            _currShipObject.LoadShip(ShipSpec);
            Utils.SetCurrShipSpec(ShipSpec);
        }
    }
}
