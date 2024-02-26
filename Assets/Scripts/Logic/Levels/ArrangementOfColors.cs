using System.Collections.Generic;
using UnityEngine;

namespace Logic.Levels
{
    public class ArrangementOfColors : MonoBehaviour
    {
        [Header("Кнопки с цветами")]
        [SerializeField] private ColorButton[] _colorButtons;
        
        public void ArrangeColors(Color[] colors)
        {
            List<Color> listOfColors = new List<Color>();
            listOfColors.AddRange(colors);

            int numberOfColors = listOfColors.Count;
            for (int i = 0; i < numberOfColors; i++)
            {
                int number = Random.Range(0, listOfColors.Count - 1);
                _colorButtons[i].gameObject.SetActive(true);
                _colorButtons[i].SetColor(listOfColors[number]);
                listOfColors.RemoveAt(number);
            }
        }
    }
}