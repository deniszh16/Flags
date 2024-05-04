using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DZGames.Flags.Logic
{
    public class VariantButton : MonoBehaviour
    {
        public int Number => _number;
        
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        
        [Header("Номер кнопки")]
        [SerializeField] private int _number;

        private GuessingCapitals _guessingCapitals;

        [Inject]
        private void Construct(GuessingCapitals guessingCapitals) =>
            _guessingCapitals = guessingCapitals;

        private void OnEnable()
        {
            _button.onClick.AddListener(() => _guessingCapitals.CheckAnswer(variantButton: this));
            Debug.Log("Подписка");
        }

        private void OnDisable()
        {
            ChangeButtonColor(Color.white);
            _button.onClick.RemoveAllListeners();
            Debug.Log("Отписка");
        }
        
        public void ChangeButtonColor(Color color) =>
            _image.color = color;
    }
}