using Services.PersistentProgress;
using Services.StaticDataService;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Logic.Levels;
using UnityEngine;
using Zenject;

namespace Logic.Buttons
{
    public class LevelStartButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        [SerializeField] private LevelStartButtonAnimation _buttonAnimation;
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;
        [SerializeField] private CurrentLevel _currentLevel;
        
        private IStaticDataService _staticData;
        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _staticData = staticData;
            _progressService = progressService;
        }

        private void OnEnable() =>
            _button.onClick.AddListener(_currentLevel.StartLevel);

        private void Start()
        {
            ChangeTranslationKey();
            _buttonAnimation.ShowButtonAnimation();
        }

        private void ChangeTranslationKey()
        {
            LocalizedString localizedString = _staticData.GetLevelConfig().LevelConfig[_progressService.GetUserProgress.Progress - 1].LocalizedText;
            _localizeStringEvent.StringReference = localizedString;
            _localizeStringEvent.RefreshString();
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(_currentLevel.StartLevel);
    }
}