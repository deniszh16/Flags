using System.Collections.Generic;
using UnityEngine;
using StaticData;

namespace Logic.WorldMap
{
    public class Countries : MonoBehaviour
    {
        [Header("Позиция карты")]
        [SerializeField] private Transform _worldMap;

        [Header("Список стран")]
        [SerializeField] private List<Country> _countries;

        private int _currentProgress;
        private LevelsStaticData _staticData;

        public void Construct(int progress, LevelsStaticData staticData)
        {
            _currentProgress = progress - 1;
            _staticData = staticData;
        }

        public void CheckCountries()
        {
            for (int i = 0; i < _countries.Count; i++)
            {
                if (i > _currentProgress) break;
                
                _countries[i].ShowCountry();
                
                if (i < _currentProgress)
                    _countries[i].ShowOpenCountry();
            }
        }

        public void MoveMapToCurrentCountry()
        {
            Vector2Int position = _staticData.LevelConfig[_currentProgress].Position;
            _worldMap.localPosition = new Vector3(position.x, position.y, 0);
        }
    }
}