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

        private int _drawnLine;
        private int _emptyFragment;

        public void SetNumberOfEmptyFragments() =>
            _emptyFragment = _emptyFragments.Length;

        public void ShowDrawnLines()
        {
            if (_drawnLine < _solidLines.Length)
            {
                _solidLines[_drawnLine].SetActive(true);
                _dotterLines[_drawnLine].SetActive(false);
                _drawnLine++;
            }
        }

        public void SelectActiveFragment() =>
            _emptyFragments[_emptyFragment].transform.GetChild(0).gameObject.SetActive(true);
    }
}