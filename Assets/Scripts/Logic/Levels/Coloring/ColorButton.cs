﻿using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Flags.Logic
{
    public class ColorButton : MonoBehaviour
    {
        public bool ColorUsed { get; set; }

        public Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }
        
        [Header("Ссылки на компоненты")]
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private Image _frameImage;
        [SerializeField] private GameObject _shadedImage;
        
        [Header("Эффект нажатия")]
        [SerializeField] private ParticleSystem _pressingButton;

        private readonly Color _translucentColor = new(1, 1, 1, 0.5f);

        private ColoringFlag _coloringFlag;
        private ArrangementOfColors _arrangementOfColors;

        [Inject]
        private void Construct(ColoringFlag coloringFlag, ArrangementOfColors arrangementOfColors)
        {
            _coloringFlag = coloringFlag;
            _arrangementOfColors = arrangementOfColors;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(ShowTapEffect);
            _button.onClick.AddListener(() => _coloringFlag.SetActiveColor(Color));
            _button.onClick.AddListener(_coloringFlag.CustomizeBrush);
            _button.onClick.AddListener(() => _arrangementOfColors.SetActiveButton(this));
        }
        
        private void OnDisable() =>
            _button.onClick.RemoveAllListeners();

        public void ChangeButtonActivity(bool state)
        {
            _button.interactable = state;
            _frameImage.color = state ? Color.white : _translucentColor;
            _shadedImage.SetActive(!state);
        }
        
        private void ShowTapEffect() =>
            _pressingButton.Play();
    }
}