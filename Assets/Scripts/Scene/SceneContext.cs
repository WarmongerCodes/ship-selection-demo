using UnityEngine;
using System;
using System.Collections.Generic;
using Audio;
using Cinemachine;
using Ship;
using UI;

namespace Scene
{
    public abstract class SceneContext : MonoBehaviour
    {
        [SerializeField] private SceneState sceneId;
        [SerializeField] private bool isInitialized;

        [SerializeField] internal AudioHandler audioHandler;

        [SerializeField] internal CinemachineVirtualCamera mainViewVCam;
        [SerializeField] internal CinemachineVirtualCamera shipViewVCam;

        [SerializeField] internal ShipSelectionHandler shipSelectionHandler;
        [SerializeField] internal VariantSelectionHandler variantSelectionHandler;

        [SerializeField] internal List<UIView> uiViewList;
        [SerializeField] internal List<SelectionBase> selectionBases;

        private void Awake()
        {
            Initialize();
        }

        private void Activate()
        {
            audioHandler.PlayBgMusic();
        }

        private void Initialize()
        {
            Utils.InitializeGameData();

            OnInitialize();
            Activate();
            
            isInitialized = true;
        }

        private void DeInitialize()
        {
            OnDeInitialize();
            
            isInitialized = false;
        }

        public T Get<T>() where T : UIView
        {
            if (uiViewList == null) return null;

            for (var i = 0; i < uiViewList.Count; i++)
            {
                T view = uiViewList[i] as T;

                if (view != null)
                {
                    return view;
                }
            }

            return null;
        }
        
        public T Open<T>() where T : UIView
        {
            if (uiViewList == null) return null;

            for (var i = 0; i < uiViewList.Count; i++)
            {
                T view = uiViewList[i] as T;

                if (view != null)
                {
                    OpenView(view, i);
                    return view;
                }
            }

            return null;
        }

        public T Close<T>() where T : UIView
        {
            if (uiViewList == null) return null;

            foreach (var view in uiViewList)
            {
                T lView = view as T;

                if (lView != null)
                {
                    CloseView(lView);
                    return lView;
                }
            }

            return null;
        }

        private void OpenView(UIView view, int index)
        {
            if (view == null)
                return;

            if (view.isActive)
                return;

            sceneId = (SceneState)index;

            view.OpenInternal();

            OnViewOpened(view);
        }

        private void CloseView(UIView view)
        {
            if (view == null)
                return;

            if (!view.isActive)
                return;

            view.CloseInternal();

            OnViewClosed(view);
        }

        internal void CloseAll()
        {
            if (uiViewList == null)
                return;

            foreach (var handler in uiViewList)
            {
                CloseView(handler);
            }
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnDeInitialize()
        {
        }

        protected virtual void OnViewOpened(UIView view)
        {
        }

        protected virtual void OnViewClosed(UIView view)
        {
        }

        internal void ResetToMainCam()
        {
            mainViewVCam.gameObject.SetActive(true);
            shipViewVCam.gameObject.SetActive(false);
        }

        internal void SwitchToSelectionCam()
        {
            mainViewVCam.gameObject.SetActive(false);
            shipViewVCam.gameObject.SetActive(true);
        }
    }

    [Serializable]
    internal enum SceneState
    {
        MainMenu = 0,
        ShipSelection = 1,
        VariantSelection = 2
    }
}
