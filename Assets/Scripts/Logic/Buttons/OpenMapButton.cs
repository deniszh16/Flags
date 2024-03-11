using Services.StateMachine;
using Services.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

namespace Logic.Buttons
{
    public class OpenMapButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        
        private const float AnimationDuration = 0.5f;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void OnEnable() =>
            _button.onClick.AddListener(OpenMap);

        private void Start() =>
            ShowButtonAnimation();

        private void OpenMap() =>
            _gameStateMachine.Enter<MapState>();

        private void ShowButtonAnimation() =>
            transform.DOScale(Vector3.one, AnimationDuration).SetEase(Ease.InOutQuad);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OpenMap);
    }
}