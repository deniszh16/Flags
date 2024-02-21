using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;

namespace StaticData
{
    [Serializable]
    public class LevelConfig
    {
        [Header("Название страны")]
        public LocalizedString LocalizedText;

        [Header("Позиция на карте")]
        public Vector2Int Position;

        [Header("Префаб флага")]
        public AssetReferenceGameObject Flag;

        [Header("Цвета для флага")]
        public Color[] Colors;
    }
}