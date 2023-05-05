using UnityEngine.UI;

namespace UI
{
    public class UIMainMenu : UIView
    {
        public Button shipSelectionButton;
        public Button attachmentSelectionButton;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            shipSelectionButton.onClick.RemoveAllListeners();
            attachmentSelectionButton.onClick.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
            
            shipSelectionButton.onClick.AddListener(OpenShipSelection);
            attachmentSelectionButton.onClick.AddListener(OpenAttachmentSelection);
            backButton.onClick.AddListener(GoBack);
        }
        
        private void OpenShipSelection()
        {
            sceneContext.audioHandler.PlayPrimarySfx();
            sceneContext.SwitchToSelectionCam();

            var sel = Open<UIShipSelection>();
            
            sel.OpenInternal();
            
            sel.backButton.onClick.RemoveAllListeners();
            sel.backButton.onClick.AddListener(() =>
            {
                GoBack();
                sel.CloseInternal();
            });
          
            CloseInternal();
        }

        private void OpenAttachmentSelection()
        {
            sceneContext.audioHandler.PlayPrimarySfx();

            var sel = Open<UIVariantSelection>();
            
            sel.OpenInternal();
            
            sel.backButton.onClick.RemoveAllListeners();
            sel.backButton.onClick.AddListener(() =>
            {
                GoBack();
                sel.CloseInternal();
            });
            
            CloseInternal();
        }
        protected override void GoBack()
        {
            sceneContext.audioHandler.PlayPrimarySfx();
            sceneContext.ResetToMainCam();
            
            var sel = Open<UIMainMenu>();
            sel.OpenInternal();

            Utils.SaveAllShipConfigurations();
        }
    }
}
