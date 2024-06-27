using Flags.Services;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using VContainer;

namespace Flags.Logic
{
    public class LevelStartButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        
        private const float AnimationDuration = 0.5f;
        
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void OnEnable() =>
            _button.onClick.AddListener(() => _gameStateMachine.Enter<DrawingState>());
        
        private void OnDisable() =>
            _button.onClick.RemoveAllListeners();

        public void ShowButtonAnimation()
        {
            _button.transform.localScale = Vector3.zero;
            _button.transform.DOScale(Vector3.one, AnimationDuration).SetEase(Ease.InOutQuad);
        }
        
        public void DisableStartButton()
        {
            _button.transform.localScale = Vector3.zero;
            _button.interactable = false;
        }
    }
}