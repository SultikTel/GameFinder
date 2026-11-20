using Finder.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Finder.Gameplay
{
    public class GameGrid : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gameGrid;
        [SerializeField] private GameCell _cellPrefab;
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private Image _interactionBloker;
        private string _currentAnswer;
        public event Action OnRightAnswerPicked;

        public void FillAndSetAnswer(Difficulty difficulty, CardBundleData cardBundleData)
        {
            SetSize(difficulty.Height, difficulty.Width);
            Fill(difficulty, cardBundleData);
        }

        private void Fill(Difficulty difficulty, CardBundleData cardBundleData)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            List<int> randomIndexes = GetUniqueRandomNumbers(difficulty.Height * difficulty.Width, cardBundleData.CardData.Length);

            for (int i = 0; i < difficulty.Height * difficulty.Width; i++)
            {
                GameCell newCell = Instantiate(_cellPrefab, transform);
                newCell.Init(cardBundleData.CardData[randomIndexes[i]]);
                newCell.OnThisGameCellClicked += NewCell_OnThisGameCellClicked;
            }

            _currentAnswer = cardBundleData.CardData[randomIndexes[UnityEngine.Random.Range(0, randomIndexes.Count)]].Identifier;
            _questionText.text = $"FIND {_currentAnswer}";
            _interactionBloker.gameObject.SetActive(false);
        }

        private void NewCell_OnThisGameCellClicked(GameCell obj)
        {
            if(obj.CardData.Identifier == _currentAnswer)
            {
                StartCoroutine(WaitTillRefresh(1));
                obj.Popup();
                _interactionBloker.gameObject.SetActive(true);
            }
            else
            {
                obj.Shake();
            }
        }

        private IEnumerator WaitTillRefresh(float timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            OnRightAnswerPicked?.Invoke();
        }

        public void SetSize(int height, int width)
        {
            if (height > width)
            {
                _gameGrid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                _gameGrid.constraintCount = height;
            }
            else
            {
                _gameGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                _gameGrid.constraintCount = width;
            }
        }

        private List<int> GetUniqueRandomNumbers(int count, int maxExclusive)
        {
            HashSet<int> uniqueNumbers = new HashSet<int>();

            while (uniqueNumbers.Count < count)
            {
                int randomNum = UnityEngine.Random.Range(0, maxExclusive);
                uniqueNumbers.Add(randomNum);
            }

            return new List<int>(uniqueNumbers);
        }
    }
}