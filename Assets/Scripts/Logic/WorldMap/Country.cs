﻿using UnityEngine;
using UnityEngine.UI;

namespace Flags.Logic
{
    public class Country : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Image _image;
        
        [Header("Открытая страна")]
        [SerializeField] private Sprite _openSprite;
        [SerializeField] private Color _color;

        public void ShowCountry() =>
            _image.gameObject.SetActive(true);

        public void ShowOpenCountry()
        {
            _image.sprite = _openSprite;
            _image.color = _color;
        }
    }
}