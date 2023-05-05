using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// base class for ui view handlers
    /// </summary>
    public abstract class UIView : MonoBehaviour
    {
        public bool defaultActive;
        public bool isActive;
        public bool isInitialized;

        public SceneContext sceneContext;
        public Button backButton;
        
        private void Awake()
        {
            Initialize();
            
            if (!defaultActive) Deactivate();
            else Activate();
        }

        private void Activate()
        {
            if (this.gameObject != null)
                this.gameObject.SetActive(true);
            
            OnActivate();

            isActive = true;
        }

        private void Deactivate()
        {
            if (this.gameObject != null)
                this.gameObject.SetActive(false); 
            
            OnDeactivate();
        
            isActive = false;
        }
        
        private void Initialize()
        {
            OnInitialize();

            isInitialized = true;
        }

        private void DeInitialize()
        {
            OnDeInitialize();

            isInitialized = false;
        }

        protected T Open<T>() where T : UIView
        {
            return sceneContext.Open<T>();
        }
        
        protected T Close<T>() where T : UIView
        {
            return sceneContext.Close<T>();
        }
        
        public void OpenInternal()
        {
            Activate();
            OnOpen();
        }

        public void CloseInternal()
        {   
            Deactivate();   
            OnClose();
        }

        protected virtual void OnActivate() { }
        protected virtual void OnDeactivate() { }
        protected virtual void OnInitialize() { }
        protected virtual void OnDeInitialize() { }
        protected virtual void OnOpen() { }
        protected virtual void OnClose() { }
        protected virtual void GoBack() { }
    }
}
