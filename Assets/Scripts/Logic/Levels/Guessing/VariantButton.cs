using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Levels.Guessing
{
    public class VariantButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        
        [Header("Номер кнопки")]
        [SerializeField] private int _number;

        public int Number => _number;

        private GuessingCapitals _guessingCapitals;

        [Inject]
        private void Construct(GuessingCapitals guessingCapitals) =>
            _guessingCapitals = guessingCapitals;

        private void OnEnable() =>
            _button.onClick.AddListener(() => _guessingCapitals.CheckAnswer(variantButton: this));
        
        public void ChangeButtonColor(Color color) =>
            _image.color = color;

        private void OnDisable()
        {
            ChangeButtonColor(Color.white);
            _button.onClick.RemoveAllListeners();
        }
    }
}