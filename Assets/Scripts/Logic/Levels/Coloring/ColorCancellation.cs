using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DZGames.Flags.Logic
{
    public class ColorCancellation : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;

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
            _button.onClick.AddListener(_coloringFlag.ResetLastFragment);
            _button.onClick.AddListener(_arrangementOfColors.ResetLastColorButton);
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(_coloringFlag.ResetLastFragment);
        
        public void ChangeButtonActivity(bool state) =>
            _button.interactable = state;
    }
}