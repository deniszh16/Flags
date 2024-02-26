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

        public void CheckCountries(int progress)
        {
            _currentProgress = progress - 1;
            
            for (int i = 0; i < _countries.Count; i++)
            {
                if (i > _currentProgress) break;
                
                _countries[i].ShowCountry();
                
                if (i < _currentProgress)
                    _countries[i].ShowOpenCountry();
            }
        }

        public void MoveMapToCurrentCountry(LevelsStaticData staticData)
        {
            Vector2Int position = staticData.LevelConfig[_currentProgress].Position;
            _worldMap.localPosition = new Vector3(position.x, position.y, 0);
        }
    }
}