using System.Collections.Generic;
using UnityEngine;

namespace Logic.Levels
{
    public class ArrangementOfColors : MonoBehaviour
    {
        [Header("Контейнер кнопок")]
        [SerializeField] private GameObject _container;
        
        [Header("Кнопки с цветами")]
        [SerializeField] private ColorButton[] _colorButtons;

        private List<ColorButton> _сolorsUsed;
        private ColorButton _activeButton;
        
        public void ArrangeColors(Color[] colors)
        {
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

        public void PrepareListOfUsedColors() =>
            _сolorsUsed = new List<ColorButton>();

        public void ActivateUnusedButtons()
        {
            foreach (ColorButton button in _colorButtons)
            {
                if (button.ColorUsed == false)
                    button.ChangeButtonActivity(state: true);
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
            _сolorsUsed.Add(_activeButton);
            _activeButton.ColorUsed = true;
        }
    }
}