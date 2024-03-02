using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UniRx;

namespace Logic.Levels.Coloring
{
    public class ArrangementOfColors : MonoBehaviour
    {
        [Header("Цветные кнопки")]
        [SerializeField] private ColorButton[] _colorButtons;

        public readonly ReactiveCommand CorrectColoring = new();
        public readonly ReactiveCommand IncorrectColoring = new();
        
        private List<Color> _flagColors;
        private List<ColorButton> _colorButtonsUsed;
        private ColorButton _activeButton;
        
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
            {
                if (button.ColorUsed == false)
                    button.ChangeButtonActivity(state: true);
            }
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
        
        public void ResetLastColorButton()
        {
            if (_colorButtonsUsed.Count > 0)
            {
                _colorButtonsUsed[^1].ColorUsed = false;
                _colorButtonsUsed.RemoveAt(_colorButtonsUsed.Count - 1);
                ActivateUnusedButtons();
            }
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