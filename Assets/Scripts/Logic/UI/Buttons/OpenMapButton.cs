using DZGames.Flags.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DZGames.Flags.Logic
{
    public class OpenMapButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        
        private const float AnimationDuration = 0.5f;
        
        private GameStateMachine _gameStateMachine;
        private IPersistentProgressService _progressService;
        private IAdsService _adsService;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine,
            IPersistentProgressService progressService, IAdsService adsService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _adsService = adsService;
        }
        
        private void Start() =>
            ShowButtonAnimation();

        private void OnEnable() =>
            _button.onClick.AddListener(OpenMap);
        
        private void OnDisable() =>
            _button.onClick.RemoveListener(OpenMap);
        
        private void OpenMap()
        {
            _gameStateMachine.Enter<MapState>();
            
            if (_progressService.GetUserProgress.Progress % 4 == 0)
                _adsService.ShowInterstitial();
        }

        private void ShowButtonAnimation() =>
            transform.DOScale(Vector3.one, AnimationDuration).SetEase(Ease.InOutQuad);
    }
}