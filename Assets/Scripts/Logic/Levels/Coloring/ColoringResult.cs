using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Flags.Logic
{
    public class ColoringResult : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Image _result;

        [Header("Спрайты результата")]
        [SerializeField] private Sprite[] _sprites;
        
        private const float AnimationDuration = 0.2f;

        public void ShowVictoryIcon()
        {
            _result.sprite = _sprites[(int)ResultTypes.Victory];
            StartIconAnimation();
        }

        public void ShowLossIcon()
        {
            _result.sprite = _sprites[(int)ResultTypes.Losing];
            StartIconAnimation();
        }
        
        public void HideResultIcon() =>
            _result.gameObject.SetActive(false);

        private void StartIconAnimation()
        {
            _result.gameObject.SetActive(true);
            _result.transform.localScale = Vector3.zero;
            _result.transform.DOScale(Vector3.one, AnimationDuration);
        }
    }
}