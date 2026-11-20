using Finder.Gameplay;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DebugFeatures
{
    public class DebugPanel : MonoBehaviour
    {
        [SerializeField] private Button _showAndHideButton;
        [SerializeField] private TMP_Text _showAndHideButtonText;
        [SerializeField] private GameObject _scrollView;
        [SerializeField] private Transform _container;
        [SerializeField] private DebugElementSimple _debugElementSimplePrefab;
        [SerializeField] private DebugElementTextInput _debugElementTextInputPrefab;
        [SerializeField] private GameGrid _gameGrid;

        private void Awake()
        {
#if UNITY_EDITOR
            ShowOrHideDebugPanel();
            Init();
#else
            gameObject.SetActive(false);
#endif
        }

        private void OnEnable()
        {
            _showAndHideButton.onClick.AddListener(ShowOrHideDebugPanel);
        }

        private void OnDisable()
        {
            _showAndHideButton.onClick.RemoveListener(ShowOrHideDebugPanel);
        }

        private void Init()
        {
            CreateFunkBlockSimple("ReloadScene", ReloadScene);
            CreateFunkBlockTextInput("SetSizeOfGrid", SetGridSize);
        }

        private void SetGridSize(string obj)
        {
            _gameGrid.SetSize((int)char.GetNumericValue(obj[0]), (int)char.GetNumericValue(obj[^1]));
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        #region Base

        private void ShowOrHideDebugPanel()
        {
            _scrollView.SetActive(!_scrollView.activeSelf);
            _showAndHideButtonText.text = _scrollView.activeSelf ? "Hide" : "Show";
        }

        private void CreateFunkBlockSimple(string buttonName, Action action)
        {
            DebugElementSimple debugPrefab = Instantiate(_debugElementSimplePrefab, _container);
            debugPrefab.Init(buttonName, action);
        }

        private void CreateFunkBlockTextInput(string buttonName, Action<string> action)
        {
            DebugElementTextInput debugPrefab = Instantiate(_debugElementTextInputPrefab, _container);
            debugPrefab.Init(buttonName, action);
        }

        #endregion
    }
}
