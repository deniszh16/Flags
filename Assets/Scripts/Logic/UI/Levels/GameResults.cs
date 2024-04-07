using UnityEngine;
using UnityEngine.Localization;
using TMPro;
using Data;

namespace Logic.UI.Levels
{
    public class GameResults : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [Header("Раскрашенные флаги")]
        [SerializeField] private LocalizedString _coloredFlagsLocalizedString;
        [SerializeField] private TextMeshProUGUI _coloredFlagsText;
        
        [Header("Правильные ответы")]
        [SerializeField] private LocalizedString _correctAnswersLocalizedString;
        [SerializeField] private TextMeshProUGUI _correctAnswersText;
        
        [Header("Неправильные ответы")]
        [SerializeField] private LocalizedString _incorrectAnswersLocalizedString;
        [SerializeField] private TextMeshProUGUI _incorrectAnswersText;
        
        [Header("Использованные подсказки")]
        [SerializeField] private LocalizedString _hintsUsedLocalizedString;
        [SerializeField] private TextMeshProUGUI _hintsUsedText;
        
        private int _flags;
        private int _correctAnswers;
        private int _incorrectAnswers;
        private int _hintsUsed;

        public void ChangeActivityOfResultsPanel(bool state) =>
            gameObject.SetActive(state);

        public void ChangeVisibilityOfDrawingSection(bool state)
        {
            int alpha = state ? 1 : 0;
            _canvasGroup.alpha = alpha;
            _canvasGroup.interactable = state;
            _canvasGroup.blocksRaycasts = state;
        }

        private void OnEnable()
        {
            _coloredFlagsLocalizedString.Arguments = new object[] { _flags };
            _coloredFlagsLocalizedString.StringChanged += UpdateNumberOfColoredFlags;
            
            _correctAnswersLocalizedString.Arguments = new object[] { _correctAnswers };
            _correctAnswersLocalizedString.StringChanged += UpdateNumberOfCorrectAnswers;
            
            _incorrectAnswersLocalizedString.Arguments = new object[] { _incorrectAnswers };
            _incorrectAnswersLocalizedString.StringChanged += UpdateNumberOfIncorrectAnswers;
            
            _hintsUsedLocalizedString.Arguments = new object[] { _hintsUsed };
            _hintsUsedLocalizedString.StringChanged += UpdateNumberOfHintsUsed;
        }
        
        private void UpdateNumberOfColoredFlags(string value) =>
            _coloredFlagsText.text = value;
        
        private void UpdateNumberOfCorrectAnswers(string value) =>
            _correctAnswersText.text = value;
        
        private void UpdateNumberOfIncorrectAnswers(string value) =>
            _incorrectAnswersText.text = value;
        
        private void UpdateNumberOfHintsUsed(string value) =>
            _hintsUsedText.text = value;
        
        public void UpdateStatistics(UserProgress progress)
        {
            _flags = progress.Progress;
            _correctAnswers = progress.RightAnswers;
            _incorrectAnswers = progress.WrongAnswers;
            _hintsUsed = progress.HintsUsed;

            _coloredFlagsLocalizedString.Arguments[0] = _flags;
            _coloredFlagsLocalizedString.RefreshString();

            _correctAnswersLocalizedString.Arguments[0] = _correctAnswers;
            _correctAnswersLocalizedString.RefreshString();

            _incorrectAnswersLocalizedString.Arguments[0] = _incorrectAnswers;
            _incorrectAnswersLocalizedString.RefreshString();

            _hintsUsedLocalizedString.Arguments[0] = _hintsUsed;
            _hintsUsedLocalizedString.RefreshString();
        }

        private void OnDisable()
        {
            _coloredFlagsLocalizedString.StringChanged -= UpdateNumberOfColoredFlags;
            _correctAnswersLocalizedString.StringChanged -= UpdateNumberOfCorrectAnswers;
            _incorrectAnswersLocalizedString.StringChanged -= UpdateNumberOfIncorrectAnswers;
            _hintsUsedLocalizedString.StringChanged -= UpdateNumberOfHintsUsed;
        }
    }
}