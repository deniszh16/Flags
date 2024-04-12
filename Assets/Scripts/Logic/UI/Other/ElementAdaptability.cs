using UnityEngine;

namespace Logic.UI.Other
{
    public class ElementAdaptability : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private RectTransform _rectTransform;
        
        [Header("Позиция и размер")]
        [SerializeField] private Vector2 _position;
        [SerializeField] private Vector2 _size;
        
        private void Start()
        {
            if (AspectRatio.Ratio <= 0.5f)
            {
                _rectTransform.anchoredPosition = _position;
                _rectTransform.sizeDelta = _size;
            }
        }
    }
}