using Logic.Levels.Factory;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.Levels
{
    public class DrawingSection : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Контейнер для флага")]
        [SerializeField] private Transform _container;

        private IFlagFactory _flagFactory;

        public void ChangeVisibilityOfDrawingSection(bool state)
        {
            int alpha = state ? 1 : 0;
            _canvasGroup.alpha = alpha;
            _canvasGroup.interactable = state;
            _canvasGroup.blocksRaycasts = state;
        }

        public void CreateFlag(IFlagFactory flagFactory, AssetReferenceGameObject flag)
        {
            _flagFactory = flagFactory;
            _flagFactory.CreateFlag(flag, _container);
        }

        public void ShowDrawnLine() =>
            _flagFactory.GetCreatedFlag.ShowDrawnLines();

        private void OnDestroy()
        {
            if (_flagFactory != null)
                _flagFactory.Dispose();
        }
    }
}