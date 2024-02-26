using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Logic.Levels.Factory
{
    public interface IFlagFactory : IDisposable
    {
        public Flag GetCreatedFlag { get; }
        public event Action<EdgeCollider2D[]> FlagCreated;
        
        public void CreateFlag(AssetReferenceGameObject flag, Transform container);
        public void RemovePreviousFlag();
    }
}