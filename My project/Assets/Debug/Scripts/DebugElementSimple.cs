using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DebugFeatures
{
    public class DebugElementSimple : MonoBehaviour
    {
        [SerializeField] private Button debugButton;
        [SerializeField] private TMP_Text hint;
        public void Init(string texthint, Action action)
        {
            hint.text = texthint;
            debugButton.onClick.AddListener(() => action());
        }
    }
}
