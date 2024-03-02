using UnityEngine;
using UnityEngine.AddressableAssets;
using UniRx;

namespace Logic.Levels.Factory
{
    public interface IFlagFactory
    {
        public Flag GetCreatedFlag { get; }
        
        public ReactiveCommand<EdgeCollider2D[]> FlagCreated { get; }
        
        public void CreateFlag(AssetReferenceGameObject flagAssetReference, Transform container);
        public void RemovePreviousFlag();
    }
}