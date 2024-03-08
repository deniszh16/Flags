using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using StaticData;

namespace Logic.WorldMap
{
    public class Countries : MonoBehaviour
    {
        [Header("Позиция карты")]
        [SerializeField] private RectTransform _worldMap;

        [Header("Список стран")]
        [SerializeField] private List<Country> _countries;

        private int _currentProgress;
        private const float AnimationDuration = 0.6f;

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

        public void MoveMapToCurrentCountry(LevelsStaticData staticData, bool motionAnimation)
        {
            Vector2Int position = staticData.LevelConfig[_currentProgress].Position;
            
            if (motionAnimation) _worldMap.localPosition = new Vector3(position.x, position.y, 0);
            else _worldMap.DOAnchorPos(position, AnimationDuration);
        }
    }
}