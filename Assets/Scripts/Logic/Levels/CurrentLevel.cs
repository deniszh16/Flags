using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

namespace Logic.Levels
{
    public class CurrentLevel : MonoBehaviour
    {
        [Header("Элементы уровня")]
        [SerializeField] private GameObject _drawingSection;
        
        [Header("Контейнер флага")]
        [SerializeField] private Transform _container;
        
        [Header("Список цветов")]
        [SerializeField] private ColorForColoring[] _colorsForColoring;
        
        private AssetReferenceGameObject _flag;
        private DrawingRoute _drawingRoute;
        private Flag _createdFlag;

        public event Action FlagCreated;

        public void ShowDrawingSection() =>
            _drawingSection.SetActive(true);

        public void CreateFlag(AssetReferenceGameObject flag, DrawingRoute drawingRoute)
        {
            if (_flag != null)
                _flag.ReleaseInstance(_createdFlag.gameObject);
            
            _flag = flag;
            _flag.InstantiateAsync(_container).Completed += OnFlagInstantiated;
            _drawingRoute = drawingRoute;
        }

        private void OnFlagInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _createdFlag = handle.Result.GetComponent<Flag>();
                _drawingRoute.PrepareRouteForPencil(_createdFlag.Colliders);
                FlagCreated?.Invoke();
            }
        }

        public void ArrangeColors(Color[] colors)
        {
            List<Color> listOfColors = new List<Color>();
            listOfColors.AddRange(colors);

            int numberOfColors = listOfColors.Count;
            for (int i = 0; i < numberOfColors; i++)
            {
                int number = Random.Range(0, listOfColors.Count - 1);
                _colorsForColoring[i].gameObject.SetActive(true);
                _colorsForColoring[i].SetColor(listOfColors[number]);
                listOfColors.RemoveAt(number);
            }
        }

        private void OnDestroy() =>
            FlagCreated = null;
    }
}