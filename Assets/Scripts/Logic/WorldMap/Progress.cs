using Services.PersistentProgress;
using Services.StaticDataService;
using UnityEngine.Localization;
using UnityEngine;
using Zenject;
using TMPro;

namespace Logic.WorldMap
{
    public class Progress : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private LocalizedString _localizedString;

        private float _progress;
        
        private IStaticDataService _staticData;
        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _staticData = staticData;
            _progressService = progressService;
        }

        private void OnEnable()
        {
            _localizedString.Arguments = new object[] { _progress };
            _localizedString.StringChanged += UpdateText;
        }

        private void Start() =>
            CalculatePassPercentage();

        private void UpdateText(string value) =>
            _textMeshPro.text = value;

        private void CalculatePassPercentage()
        {
            _progress = (float)(_progressService.GetUserProgress.Progress - 1) / _staticData.GetLevelConfig().LevelConfig.Count * 100;
            _localizedString.Arguments[0] = _progress;
            _localizedString.RefreshString();
        }

        private void OnDisable() =>
            _localizedString.StringChanged -= UpdateText;
    }
}