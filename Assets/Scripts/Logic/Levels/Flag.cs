using UnityEngine;
using UnityEngine.UI;

namespace Logic.Levels
{
    public class Flag : MonoBehaviour
    {
        [Header("Маршруты рисования")]
        [SerializeField] private EdgeCollider2D[] _colliders;

        public EdgeCollider2D[] Colliders => _colliders;
        
        [Header("Пунктирные линии")]
        [SerializeField] private GameObject[] _dotterLines;

        [Header("Сплошные линии")]
        [SerializeField] private GameObject[] _solidLines;
        
        [Header("Пустые фрагменты")]
        [SerializeField] private Image[] _emptyFragments;

        public int NumberOfEmptyFragments =>
            _emptyFragments.Length;

        private int _drawnLine;

        public void ShowDrawnLines()
        {
            if (_drawnLine < _solidLines.Length)
            {
                _solidLines[_drawnLine].SetActive(true);
                _dotterLines[_drawnLine].SetActive(false);
                _drawnLine++;
            }
        }
        
        public Image SelectActiveFragment(int fragmentNumber)
        {
            if (fragmentNumber >= NumberOfEmptyFragments) return null;
            _emptyFragments[fragmentNumber].transform.GetChild(0).gameObject.SetActive(true);
            return _emptyFragments[fragmentNumber];
        }

        public Vector2 GetPositionOfActiveFragment(int fragmentNumber) =>
            _emptyFragments[fragmentNumber].transform.position;

        public void DeselectFragment(int fragmentNumber) =>
            _emptyFragments[fragmentNumber].transform.GetChild(0).gameObject.SetActive(false);
    }
}