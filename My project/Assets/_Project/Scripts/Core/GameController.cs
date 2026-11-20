using Finder.Configs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Finder.Gameplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private List<Difficulty> _difficulties;
        [SerializeField] private List<CardBundleData> _cards;
        [SerializeField] private GameGrid _gameGrid;
        private int _currentDiffigultyIndex;
        private int _currentBundleIndex;

        private void Awake()
        {
            StartNewSession();
        }

        private void OnEnable()
        {
            _gameGrid.OnRightAnswerPicked += LevelUpDifficultyAndRefresh;
        }

        private void OnDisable()
        {
            _gameGrid.OnRightAnswerPicked -= LevelUpDifficultyAndRefresh;
        }

        private void StartNewSession()
        {
            _currentDiffigultyIndex = 0;
            _currentBundleIndex = UnityEngine.Random.Range(0, _cards.Count);
            FillGrid();
        }

        private void LevelUpDifficultyAndRefresh()
        {
            _currentDiffigultyIndex++;
            if (_currentDiffigultyIndex >= _difficulties.Count)
            {
                StartNewSession();
            }
            else
            {
                FillGrid();
            }
        }

        private void FillGrid()
        {
            _gameGrid.FillAndSetAnswer(_difficulties[_currentDiffigultyIndex], _cards[_currentBundleIndex]);
        }
    }
}