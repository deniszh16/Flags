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

        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void OnEnable() =>
            _button.onClick.AddListener(StartLevel);

        private void Start() =>
            ShowButtonAnimation();

        private void StartLevel() =>
            _gameStateMachine.Enter<DrawingState>();

        private void ShowButtonAnimation() =>
            transform.DOScale(Vector3.one, duration: 0.5f).SetEase(Ease.InOutQuad);

        private void OnDisable() =>
            _button.onClick.RemoveListener(StartLevel);
    }
}