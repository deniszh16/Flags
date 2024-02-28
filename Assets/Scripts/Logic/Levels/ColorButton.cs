using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Levels
{
    public class ColorButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        public bool ColorUsed { get; set; }

        public Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

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
            _button.onClick.AddListener(() => _coloringFlag.SetActiveColor(Color));
            _button.onClick.AddListener(_coloringFlag.CustomizeBrush);
            _button.onClick.AddListener(() => _arrangementOfColors.SetActiveButton(this));
        }

        public void ChangeButtonActivity(bool state) =>
            _button.interactable = state;

        private void OnDisable() =>
            _button.onClick.RemoveAllListeners();
    }
}