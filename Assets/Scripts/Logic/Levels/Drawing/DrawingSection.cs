using Flags.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Flags.Logic
{
    public class DrawingSection : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Контейнер для флага")]
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _loadingIcon;

        private IFlagFactory _flagFactory;
        
        private void OnDestroy() =>
            _flagFactory?.RemovePreviousFlag();

        public void ChangeVisibilityOfDrawingSection(bool state)
        {
            int alpha = state ? 1 : 0;
            _canvasGroup.alpha = alpha;
            _canvasGroup.interactable = state;
            _canvasGroup.blocksRaycasts = state;
        }

        public void ChangeVisibilityOfLoadingIcon(bool state) =>
            _loadingIcon.SetActive(state);

        public void CreateFlag(IFlagFactory flagFactory, AssetReferenceGameObject flag)
        {
            _flagFactory = flagFactory;
            _flagFactory.CreateFlag(flag, _container);
        }
    }
}