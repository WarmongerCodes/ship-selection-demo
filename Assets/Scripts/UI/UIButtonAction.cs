using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Button feedback handler
    /// </summary>
    [Serializable]
    public class UIButtonAction : MonoBehaviour
    {
        [SerializeField] private Image selection;
        [SerializeField] internal TextMeshProUGUI title;
        internal bool IsVisible;

        private void Awake()
        {
            selection = GetComponent<Image>();
        }

        internal void Activate()
        {
            IsVisible = true;
            selection.color = Color.green;
        }

        internal void Deactivate()
        {
            IsVisible = false;
            selection.color = Color.white;
        }
    }
}
