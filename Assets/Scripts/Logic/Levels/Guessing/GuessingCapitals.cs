using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using DG.Tweening;
using UniRx;

namespace Flags.Logic
{
    public class GuessingCapitals : MonoBehaviour
    {
        public readonly ReactiveCommand<bool> QuizCompleted = new();
        
        [Header("Ссылки на компоненты")]
        [SerializeField] private RectTransform _rectTransform;
        
        [Header("Тексты вариантов")]
        [SerializeField] private LocalizeStringEvent[] _localizeStringEvents;

        private readonly Vector2 _startingPosition = new(0, -300);
        
        private const float PositionY = 270;
        private const float AnimationDuration = 0.3f;
        private const float DelayBeforeAnimation = 0.1f;

        private int _correctAnswer;
        private bool _isAnswered;

        public void ChangeGuessingActivity(bool state) =>
            _rectTransform.gameObject.SetActive(state);

        public void ArrangeOptions(LocalizedString[] variants, int correctAnswer)
        {
            _correctAnswer = correctAnswer;
            
            for (int i = 0; i < _localizeStringEvents.Length; i++)
            {
                _localizeStringEvents[i].StringReference = variants[i];
                _localizeStringEvents[i].RefreshString();
            }
        }

        public void ShowSpawnAnimation() =>
            _rectTransform.DOAnchorPosY(endValue: PositionY, AnimationDuration, snapping: true).SetDelay(DelayBeforeAnimation);
        
        public void CheckAnswer(VariantButton variantButton)
        {
            if (_isAnswered) return;
            _isAnswered = true;
            
            if (variantButton.Number.Equals(_correctAnswer))
            {
                variantButton.ChangeButtonColor(Color.green);
                QuizCompleted.Execute(parameter: true);
                return;
            }
            
            variantButton.ChangeButtonColor(Color.red);
            QuizCompleted.Execute(parameter: false);
        }

        public void ResetPanelCapitals()
        {
            if (_rectTransform)
                _rectTransform.anchoredPosition = _startingPosition;
            
            _isAnswered = false;
        }
    }
}