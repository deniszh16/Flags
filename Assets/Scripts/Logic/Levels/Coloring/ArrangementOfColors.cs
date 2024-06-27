using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using UniRx;

namespace Flags.Logic
{
    public class ArrangementOfColors : MonoBehaviour
    {
        public readonly ReactiveCommand CorrectColoring = new();
        public readonly ReactiveCommand IncorrectColoring = new();
        public readonly ReactiveCommand ColoredButtonSelected = new();
        
        [Header("Ссылки на компоненты")]
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [Header("Цветные кнопки")]
        [SerializeField] private ColorButton[] _colorButtons;

        private bool _coloredButtonSelected;
        
        private List<Color> _flagColors;
        private List<ColorButton> _colorButtonsUsed;
        private ColorButton _activeButton;

        public void ChangeVisibilityOfColors(bool state)
        {
            int value = state ? 1 : 0;
            _canvasGroup.alpha = value;
        }

        public void ArrangeColors(Color[] colors, bool randomArrangement)
        {
            _flagColors = new List<Color>();
            _flagColors.AddRange(colors);
            
            _colorButtonsUsed = new List<ColorButton>();
            
            List<Color> listOfColors = new List<Color>();
            listOfColors.AddRange(colors);

            Random random = new();
            int numberOfColors = listOfColors.Count;
            for (int i = 0; i < _colorButtons.Length; i++)
            {
                if (i < numberOfColors)
                {
                    int number = randomArrangement ? random.Next(0, listOfColors.Count) : 0;
                    _colorButtons[i].gameObject.SetActive(true);
                    _colorButtons[i].Color = listOfColors[number];
                    listOfColors.RemoveAt(number);
                }
                else
                {
                    _colorButtons[i].gameObject.SetActive(false);
                }
            }
        }
        
        public void DisableInteractivityOfAllButtons()
        {
            foreach (ColorButton button in _colorButtons)
                button.ChangeButtonActivity(state: false);
        }
        
        public void SetActiveButton(ColorButton colorButton)
        {
            _activeButton = colorButton;

            if (_coloredButtonSelected != true)
            {
                ColoredButtonSelected.Execute();
                _coloredButtonSelected = true;
            }
        }

        public void RecordSelectedColor()
        {
            _colorButtonsUsed.Add(_activeButton);
            _activeButton.ColorUsed = true;
        }
        
        public void ActivateInteractivityOfUnusedButtons()
        {
            foreach (ColorButton button in _colorButtons)
                button.ChangeButtonActivity(state: button.ColorUsed == false);
        }
        
        public void CompareColorCollections()
        {
            for (int i = 0; i < _flagColors.Count; i++)
            {
                if (_flagColors[i].Equals(_colorButtonsUsed[i].Color)) continue;
                IncorrectColoring.Execute();
                return;
            }

            CorrectColoring.Execute();
        }

        public (int, Color) FindFragmentAndColorForHint()
        {
            if (_colorButtonsUsed.Count < 1)
            {
                FindButtonWithColor(_flagColors[0]);
                ActivateInteractivityOfUnusedButtons();
                return (0, _flagColors[0]);
            }

            for (int i = 0; i < _colorButtonsUsed.Count; i++)
            {
                if (_colorButtonsUsed[i].Color.Equals(_flagColors[i])) continue;
                
                for (int j = _colorButtonsUsed.Count; j > i; j--)
                {
                    _colorButtonsUsed[^1].ColorUsed = false;
                    _colorButtonsUsed.RemoveAt(_colorButtonsUsed.Count - 1);
                }
                
                FindButtonWithColor(_flagColors[i]);
                ActivateInteractivityOfUnusedButtons();
                return (i, _flagColors[i]);
            }

            int fragment = _colorButtonsUsed.Count;
            FindButtonWithColor(_flagColors[fragment]);
            ActivateInteractivityOfUnusedButtons();
            return (fragment, _flagColors[fragment]);
        }
        
        public void ResetLastColorButton()
        {
            if (_colorButtonsUsed.Count < 1) return;
            _colorButtonsUsed[^1].ColorUsed = false;
            _colorButtonsUsed.RemoveAt(_colorButtonsUsed.Count - 1);
            ActivateInteractivityOfUnusedButtons();
        }
        
        public void ResetColorButtons()
        {
            foreach (ColorButton button in _colorButtonsUsed)
            {
                button.ColorUsed = false;
                button.ChangeButtonActivity(state: true);
            }
            
            _colorButtonsUsed.Clear();
        }
        
        private void FindButtonWithColor(Color color)
        {
            for (int i = 0; i < _colorButtons.Length; i++)
            {
                if (_colorButtons[i].gameObject.activeInHierarchy && _colorButtons[i].Color.Equals(color))
                {
                    _activeButton = _colorButtons[i];
                    RecordSelectedColor();
                    break;
                }
            }
        }
    }
}