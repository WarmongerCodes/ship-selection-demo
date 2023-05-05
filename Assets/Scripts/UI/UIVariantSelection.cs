using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Ship variant selection UI handler
    /// </summary>
    public class UIVariantSelection : UIView
    {
        public TextMeshProUGUI selectedVariantTitle;
        public Button variantSelectButton;
        public RectTransform layoutParent;

        private int _currentSelectedIndex;
        private readonly List<UIButtonAction> _variantSelectButtonsPool = new ();

        protected override void OnActivate()
        {
            base.OnActivate();

            sceneContext.ResetToMainCam();
            sceneContext.variantSelectionHandler.LoadCurrentShip();
            GenerateUiItems();
        }

        private void GenerateUiItems()
        {
            DestroyUiPoolItems();

            var savedShip = Utils.GetCurrShipSpec();
            selectedVariantTitle.text = $"{savedShip.sVar}";
            _currentSelectedIndex = (int)savedShip.sVar;

            for (int i = 0; i < 3; i++)
            {
                var varSelectButton = Instantiate(variantSelectButton, layoutParent);
                varSelectButton.gameObject.SetActive(true);

                var uba = varSelectButton.GetComponent<UIButtonAction>();
                uba.title.text = ((ShipVar)i).ToString();

                var index = i;
                if (_currentSelectedIndex != index)
                    uba.Deactivate();

                varSelectButton.onClick.AddListener(() =>
                {
                    if (uba.IsVisible)
                    {
                        varSelectButton.transform.GetChild(0).gameObject.SetActive(true);
                        sceneContext.audioHandler.PlayErrorSfx();
                        return;
                    }

                    selectedVariantTitle.text = $"{(ShipVar)index}";
                    _currentSelectedIndex = index;
                    
                    ResetButtons();
                    uba.Activate();

                    sceneContext.audioHandler.PlaySecondarySfx();
                    sceneContext.variantSelectionHandler.UpdateShipVariant(index);
                });
                
                _variantSelectButtonsPool.Add(uba);
            }
            
            SetButtonActive(_currentSelectedIndex);
        }

        #region Helper Functions
        
        private void SetButtonActive(int index)
        {
            _variantSelectButtonsPool[index].Activate();
        }
        private void ResetButtons()
        {
            foreach (var item in _variantSelectButtonsPool)
            {
                item.Deactivate();
            }
        }

        private void DestroyUiPoolItems()
        {
            for (var i = _variantSelectButtonsPool.Count - 1; i >= 0; i--)
            {
                var item = _variantSelectButtonsPool[i];
                Destroy(item.gameObject);
            }

            _variantSelectButtonsPool.Clear();
        }

        #endregion
    }
}
