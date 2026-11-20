using DG.Tweening;
using Finder.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Finder.Gameplay
{
    public class GameCell : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Image _backGround;

        private CardData _cardData;
        private Sequence _animSequence;

        public CardData CardData => _cardData;
        public event Action<GameCell> OnThisGameCellClicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(Clicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Clicked);
        }

        private void OnDestroy()
        {
            _animSequence?.Kill();
        }

        public void Init(CardData data)
        {
            _cardData = data;
            _image.sprite = data.Sprite;
            _backGround.color = UnityEngine.Random.ColorHSV();

            switch (data.NeededRotation)
            {
                case Rotation.Left:
                    _image.rectTransform.localEulerAngles = new Vector3(0f, 0f, 90f);
                    break;
                case Rotation.Right:
                    _image.rectTransform.localEulerAngles = new Vector3(0f, 0f, -90f);
                    break;
                case Rotation.UpsideDown:
                    _image.rectTransform.localEulerAngles = new Vector3(0f, 0f, 180f);
                    break;
                default:
                    _image.rectTransform.localEulerAngles = Vector3.zero;
                    break;
            }
        }

        public void Popup()
        {
            _animSequence?.Kill();
            RectTransform imageRect = _image.rectTransform;
            imageRect.localScale = Vector3.one;

            _animSequence = DOTween.Sequence();
            _animSequence.Append(imageRect.DOScale(0.5f, 0.1f).SetEase(Ease.InQuad));
            _animSequence.Append(imageRect.DOScale(1.2f, 0.15f).SetEase(Ease.OutQuad));
            _animSequence.Append(imageRect.DOScale(1f, 0.1f).SetEase(Ease.InOutQuad));
        }

        public void Shake()
        {
            _animSequence?.Kill();
            RectTransform imageRect = _image.rectTransform;
            imageRect.localScale = Vector3.one;
            imageRect.anchoredPosition = Vector2.zero;

            _animSequence = DOTween.Sequence();
            _animSequence.Append(imageRect.DOScale(0.8f, 0.08f).SetEase(Ease.OutQuad));
            _animSequence.Append(imageRect.DOShakeAnchorPos(0.3f, strength: 15f, vibrato: 20, randomness: 90f));
            _animSequence.Append(imageRect.DOScale(1f, 0.1f).SetEase(Ease.OutBack));
            _animSequence.Join(imageRect.DOAnchorPos(Vector2.zero, 0.1f));
        }

        private void Clicked()
        {
            OnThisGameCellClicked?.Invoke(this);
        }
    }
}