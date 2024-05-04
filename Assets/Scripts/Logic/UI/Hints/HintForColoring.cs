using DZGames.Flags.Services;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using VContainer;
using TMPro;

namespace DZGames.Flags.Logic
{
    public class HintForColoring : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _hintButton;
        [SerializeField] private TextMeshProUGUI _numberOfHints;
        [SerializeField] private Button _closeWindowButton;

        [Header("Эффект получения")]
        [SerializeField] private ParticleSystem _effect;

        [Header("Панель получения подсказок")]
        [SerializeField] private GameObject _gettingHints;
        
        private const float AnimationDuration = 0.3f;

        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IAdsService _adsService;
        
        private ArrangementOfColors _arrangementOfColors;
        private ColoringFlag _coloringFlag;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IAdsService adsService, ArrangementOfColors arrangementOfColors, ColoringFlag coloringFlag)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _adsService = adsService;
            _arrangementOfColors = arrangementOfColors;
            _coloringFlag = coloringFlag;
        }
        
        private void OnEnable()
        {
            _hintButton.onClick.AddListener(UseHint);
            _closeWindowButton.onClick.AddListener(CloseHintWindow);
            _progressService.GetUserProgress.ChangedNumberOfHints += ShowNumberOfHints;
            _adsService.RewardedAdViewed += GiveRewardForViewingAds;
            _adsService.RewardedAdViewed += ShowHintEffect;
        }
        
        private void OnDisable()
        {
            _hintButton.onClick.RemoveListener(UseHint);
            _closeWindowButton.onClick.RemoveListener(CloseHintWindow);
            _progressService.GetUserProgress.ChangedNumberOfHints -= ShowNumberOfHints;
            _adsService.RewardedAdViewed -= GiveRewardForViewingAds;
            _adsService.RewardedAdViewed -= ShowHintEffect;
        }

        public void ChangeActivityOfHintButton(bool state) =>
            _hintButton.interactable = state;

        public void ShowNumberOfHints() =>
            _numberOfHints.text = _progressService.GetUserProgress.Hints.ToString();
        
        public void ViewRewardedAds()
        {
            _adsService.ShowRewardedAd();
            CloseHintWindow();
        }
        
        public void CloseHintWindow() =>
            _gettingHints.SetActive(false);
        
        private void UseHint()
        {
            if (_progressService.GetUserProgress.Hints > 0)
            {
                var fragmentAndColor = _arrangementOfColors.FindFragmentAndColorForHint();
                _coloringFlag.ColorInFragmentWithHint(fragmentAndColor.Item1, fragmentAndColor.Item2);
                _progressService.GetUserProgress.ChangeNumberOfHintsUsed();
                _progressService.GetUserProgress.ChangeNumberOfHints(-1);
                _saveLoadService.SaveProgress();
                ShowHintEffect();
            }
            else
            {
                _gettingHints.SetActive(true);
                _gettingHints.transform.localScale = Vector3.zero;
                _gettingHints.transform.DOScale(Vector3.one, AnimationDuration);
            }
        }
        
        private void GiveRewardForViewingAds()
        {
            _progressService.GetUserProgress.ChangeNumberOfHints(3);
            _saveLoadService.SaveProgress();
        }
        
        private void ShowHintEffect()
        {
            _effect.gameObject.SetActive(true);
            _effect.Play();
        }
    }
}