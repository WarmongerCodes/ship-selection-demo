using System;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Helper class for UI button actions
    /// </summary>
    public class UIPromptBehaviour : MonoBehaviour
    {
        private void OnEnable() => Invoke(nameof(Disable), 1f);

        internal void Disable() => gameObject.SetActive(false);
    }
}
