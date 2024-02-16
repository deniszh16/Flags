using DG.Tweening;
using UnityEngine;

namespace Logic.Buttons
{
    public class LevelStartButtonAnimation : MonoBehaviour
    {
        public void ShowButtonAnimation() =>
            transform.DOScale(Vector3.one, duration: 0.5f).SetEase(Ease.InOutQuad);
    }
}