using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DebugFeatures
{
    public class DebugElementTextInput : MonoBehaviour
    {
        [SerializeField] private Button _debugButton;
        [SerializeField] private TMP_Text _hint;
        [SerializeField] private TMP_InputField _inputField;
        public void Init(string texthint, Action<string> action)
        {
            _hint.text = texthint;
            _debugButton.onClick.AddListener(() => action(_inputField.text));
        }
    }
}
