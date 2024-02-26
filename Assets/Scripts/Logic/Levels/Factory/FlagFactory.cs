using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Logic.Levels.Factory
{
    public class FlagFactory : IFlagFactory, IDisposable
    {
        private AssetReferenceGameObject _flag;
        private Flag _createdFlag;

        public Flag GetCreatedFlag =>
            _createdFlag;
        
        public event Action<EdgeCollider2D[]> FlagCreated;
        
        public void CreateFlag(AssetReferenceGameObject flag, Transform container)
        {
            RemovePreviousFlag();
            
            _flag = flag;
            _flag.InstantiateAsync(container).Completed += OnFlagInstantiated;
        }
        
        private void OnFlagInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _createdFlag = handle.Result.GetComponent<Flag>();
                FlagCreated?.Invoke(_createdFlag.Colliders);
            }
        }

        public void RemovePreviousFlag()
        {
            if (_flag != null)
                _flag.ReleaseInstance(_createdFlag.gameObject);
        }

        public void Dispose() =>
            FlagCreated = null;
    }
}