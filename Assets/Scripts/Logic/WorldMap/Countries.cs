using Services.PersistentProgress;
using Services.StaticDataService;
using UnityEngine;
using StaticData;
using Zenject;

namespace Logic.WorldMap
{
    public class Countries : MonoBehaviour
    {
        [Header("Позиция карты")]
        [SerializeField] private Transform _worldMap;
        
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticData;

        [Inject]
        private void Construct(IPersistentProgressService progressService, IStaticDataService staticData)
        {
            _progressService = progressService;
            _staticData = staticData;
        }

        private void Start() =>
            MoveMapToCurrentCountry();

        private void MoveMapToCurrentCountry()
        {
            int progress = GetCurrentCountry();
            Vector2Int position = GetCountryData(progress - 1).Position;
            _worldMap.localPosition = new Vector3(position.x, position.y, 0);
        }

        public int GetCurrentCountry() =>
            _progressService.GetUserProgress.Progress;

        private LevelConfig GetCountryData(int number) =>
            _staticData.GetLevelConfig().LevelConfig[number];
    }
}