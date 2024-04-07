using Logic.Levels.StateMachine.States;
using Services.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

namespace Logic.UI.Buttons
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
            _button.onClick.RemoveAllListeners();
    }
}