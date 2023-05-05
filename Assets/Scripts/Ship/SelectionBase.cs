using Scene;
using UnityEngine;

namespace Ship
{
    /// <summary>
    /// base class for selection handlers
    /// </summary>
    public abstract class SelectionBase : MonoBehaviour
    {
        public SceneContext sceneContext;
        public bool defaultActive;
        public bool isInitialized;
        public bool isActive;
        
        private void Awake()
        {
            if(!isInitialized) Initialize();
            
            if (defaultActive) Activate();
            else Deactivate();
        }

        private void Initialize()
        {
            sceneContext = FindObjectOfType<SceneHandler>();

            OnInitialize();
            
            isInitialized = true;
        }

        internal void DeInitialize()
        {
            OnDeInitialize();
            
            isInitialized = false;
        }

        internal void Activate()
        {
            if(gameObject!=null)
                gameObject.SetActive(true);
            
            OnActivate();
            
            isActive = true;
        }

        internal void Deactivate()
        {            
            if(gameObject!=null)
                gameObject.SetActive(false);
            
            OnDeactivate();
            
            isActive = false;
        }
        
        protected virtual void OnInitialize() { }
        protected virtual void OnDeInitialize() { }
        protected virtual void OnActivate() { }
        protected virtual void OnDeactivate() { }
    }
    
}
