using DZGames.Flags.Logic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UniRx;

namespace DZGames.Flags.Services
{
    public class FlagFactory : IFlagFactory
    {
        public Flag GetCreatedFlag { get; private set; } 
        
        public ReactiveCommand<EdgeCollider2D[]> FlagCreated { get; } = new();
        
        private AssetReferenceGameObject _flag;

        public void CreateFlag(AssetReferenceGameObject flagAssetReference, Transform container)
        {
            _flag = flagAssetReference;
            _flag.InstantiateAsync(container).Completed += OnFlagInstantiated;
        }
        
        public void RemovePreviousFlag()
        {
            if (GetCreatedFlag != null)
                _flag?.ReleaseInstance(GetCreatedFlag.gameObject);
        }
        
        private void OnFlagInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GetCreatedFlag = handle.Result.GetComponent<Flag>();
                FlagCreated.Execute(GetCreatedFlag.Colliders);
            }
        }
    }
}