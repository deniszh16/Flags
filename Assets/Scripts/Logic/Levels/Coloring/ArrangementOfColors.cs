using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UniRx;

namespace Logic.Levels.Coloring
{
    public class ArrangementOfColors : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [Header("Цветные кнопки")]
        [SerializeField] private ColorButton[] _colorButtons;

        public readonly ReactiveCommand CorrectColoring = new();
        public readonly ReactiveCommand IncorrectColoring = new();
        
        private List<Color> _flagColors;
        private List<ColorButton> _colorButtonsUsed;
        private ColorButton _activeButton;

        public void ChangeVisibilityOfColors(bool state)
        {
            int value = state ? 1 : 0;
            _canvasGroup.alpha = value;
        }

        public void ArrangeColors(Color[] colors)
        {
            _flagColors = new List<Color>();
            _flagColors.AddRange(colors);
            
            _colorButtonsUsed = new List<ColorButton>();
            
            List<Color> listOfColors = new List<Color>();
            listOfColors.AddRange(colors);

            int numberOfColors = listOfColors.Count;
            for (int i = 0; i < numberOfColors; i++)
            {
                int number = Random.Range(0, listOfColors.Count - 1);
                _colorButtons[i].gameObject.SetActive(true);
                _colorButtons[i].Color = listOfColors[number];
                listOfColors.RemoveAt(number);
            }
        }
        
        public void DisableAllButtons()
        {
            foreach (ColorButton button in _colorButtons)
                button.ChangeButtonActivity(state: false);
        }
        
        public void SetActiveButton(ColorButton colorButton) =>
            _activeButton = colorButton;
        
        public void RecordSelectedColor()
        {
            _colorButtonsUsed.Add(_activeButton);
            _activeButton.ColorUsed = true;
        }
        
        public void ActivateUnusedButtons()
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
                ActivateUnusedButtons();
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
                ActivateUnusedButtons();
                return (i, _flagColors[i]);
            }

            int fragment = _colorButtonsUsed.Count;
            FindButtonWithColor(_flagColors[fragment]);
            ActivateUnusedButtons();
            return (fragment, _flagColors[fragment]);
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
        
        public void ResetLastColorButton()
        {
            if (_colorButtonsUsed.Count < 1) return;
            _colorButtonsUsed[^1].ColorUsed = false;
            _colorButtonsUsed.RemoveAt(_colorButtonsUsed.Count - 1);
            ActivateUnusedButtons();
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
    }
}