using Services.StateMachine;
using Services.StateMachine.States;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Zenject;

namespace Logic.Buttons
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
            _button.onClick.AddListener(StartLevel);

        private void StartLevel() =>
            _gameStateMachine.Enter<DrawingState>();

        public void ShowButtonAnimation()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, AnimationDuration).SetEase(Ease.InOutQuad);
        }
        
        public void DisableStartButton()
        {
            transform.localScale = Vector3.zero;
            _button.interactable = false;
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(StartLevel);
    }
}