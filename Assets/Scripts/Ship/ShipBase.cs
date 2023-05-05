using System.Collections.Generic;
using UnityEngine;

namespace Ship
{
    /// <summary>
    /// base class for ship gameobjects
    /// </summary>
    public abstract class ShipBase : MonoBehaviour
    {
        public bool defaultActive;
        public bool isInitialized;
        public bool isActive;
        
        internal VariantSelectionHandler VariantSelectionHandler;
        
        [SerializeField] internal ShipSpec currSpec;
        internal List<ShipVariant> VariantsList = new();

        private void Awake()
        {
            Initialize();

            if (defaultActive) Activate();
            else Deactivate();
        }

        private void Initialize()
        {
            OnInitialize();

            isInitialized = true;
        }

        public void DeInitialize()
        {
            OnDeInitialize();
            Deactivate();

            isInitialized = false;
        }

        internal void Activate()
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

        protected virtual void OnInitialize() { }
        protected virtual void OnDeInitialize() { }
        protected virtual void OnActivate() { }
        protected virtual void OnDeactivate() { }
    }
}
