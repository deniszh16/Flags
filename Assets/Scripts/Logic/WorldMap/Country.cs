using UnityEngine;
using UnityEngine.UI;

namespace Logic.WorldMap
{
    public class Country : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Countries _countries;
        [SerializeField] private Image _image;

        [Header("Номер уровня")]
        [SerializeField] private int _number;

        [Header("Фрагменты страны")]
        [SerializeField] private Sprite _closedSprite;
        [SerializeField] private Sprite _openSprite;

        [Header("Цвет фрагмента")]
        [SerializeField] private Color _color;

        private void Start() =>
            CheckCountry();

        private void CheckCountry()
        {
            int progress = _countries.GetCurrentCountry();

            if (progress == _number)
            {
                _image.gameObject.SetActive(true);
                _image.sprite = _closedSprite;
            }
            else if (progress > _number)
            {
                _image.gameObject.SetActive(true);
                _image.sprite = _openSprite;
                _image.color = _color;
            }
        }
    }
}