﻿using UnityEngine;
using UnityEngine.UI;

namespace Logic.Levels
{
    public class ColorForColoring : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Image _image;

        public void SetColor(Color color) =>
            _image.color = color;
    }
}