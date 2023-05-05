using System.Collections.Generic;
using Ship;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Ship selection UI handler
    /// </summary>
    public class UIShipSelection : UIView
    {
        public TextMeshProUGUI shipNameText;
        public Button shipSelectButton;
        public RectTransform layoutParent;

        private int _currentSelectedIndex;
        private readonly List<UIButtonAction> _shipSelectButtonsPool = new ();

        protected override void OnActivate()
        {
            base.OnActivate();

            if (isActive) sceneContext.SwitchToSelectionCam();
        }

        internal void GenerateUiItems(List<ShipObject> shipObjects)
        {
            var savedShip = Utils.GetCurrShipSpec();
            shipNameText.text = savedShip.sName;
            _currentSelectedIndex = (int)savedShip.sClass;

            for (var i = 0; i < shipObjects.Count; i++)
            {
                var ship = shipObjects[i];
                var shipSpec = Utils.GetShipSpec(ship.currSpec.sId);

                var shSelectButton = Instantiate(shipSelectButton, layoutParent);
                shSelectButton.gameObject.SetActive(true);

                var uba = shSelectButton.GetComponent<UIButtonAction>();
                uba.title.text = shipSpec.sName;

                var index = i;
                shSelectButton.onClick.AddListener(() =>
                {
                    if (ship.isActive)
                    {
                        shSelectButton.transform.GetChild(0).gameObject.SetActive(true);
                        sceneContext.audioHandler.PlayErrorSfx();
                        return;
                    }

                    sceneContext.audioHandler.PlaySecondarySfx();
                    sceneContext.shipSelectionHandler.DeactivateAllShips();

                    _currentSelectedIndex = index;
                    ResetButtons();
                    uba.Activate();

                    shipNameText.text = shipSpec.sName;
                    sceneContext.variantSelectionHandler.ShipSpec = shipSpec;

                    ship.LoadShip(shipSpec);
                    Utils.SetCurrShipSpec(shipSpec);
                });

                _shipSelectButtonsPool.Add(uba);
            }

            SetButtonActive(_currentSelectedIndex);
        }

        #region Helper Functions

        private void ResetButtons()
        {
            foreach (var item in _shipSelectButtonsPool)
            {
                item.Deactivate();
            }
        }

        private void SetButtonActive(int index)
        {
            _shipSelectButtonsPool[index].Activate();
        }

        #endregion
    }
}
