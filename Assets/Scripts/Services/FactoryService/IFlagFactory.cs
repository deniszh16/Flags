using Flags.Logic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UniRx;

namespace Flags.Services
{
    public interface IFlagFactory
    {
        public Flag GetCreatedFlag { get; }
        
        public ReactiveCommand<EdgeCollider2D[]> FlagCreated { get; }
        
        public void CreateFlag(AssetReferenceGameObject flagAssetReference, Transform container);
        public void RemovePreviousFlag();
    }
}