using Logic.Levels.Factory;
using Services.StaticDataService;
using UnityEngine;

namespace Logic.Levels
{
    public class DrawingSection : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Контейнер для флага")]
        [SerializeField] private Transform _container;

        private IStaticDataService _staticData;
        private IFlagFactory _flagFactory;

        public void Construct(IStaticDataService staticData, IFlagFactory flagFactory)
        {
            _staticData = staticData;
            _flagFactory = flagFactory;
        }

        public void ChangeVisibilityOfDrawingSection(bool state)
        {
            int alpha = state ? 1 : 0;
            _canvasGroup.alpha = alpha;
            _canvasGroup.interactable = state;
        }

        public void CreateFlag(int flagNumber) =>
            _flagFactory.CreateFlag(_staticData.GetLevelConfig().LevelConfig[flagNumber].Flag, _container);

        public void ShowDrawnLine() =>
            _flagFactory.GetCreatedFlag.ShowDrawnLines();

        private void OnDestroy() =>
            _flagFactory.Dispose();
    }
}