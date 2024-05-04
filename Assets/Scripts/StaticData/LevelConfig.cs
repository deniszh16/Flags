using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;

namespace DZGames.Flags.StaticData
{
    [Serializable]
    public class LevelConfig
    {
        [Header("Название страны")]
        public LocalizedString LocalizedText;
        
        [Header("Варианты столицы")]
        public LocalizedString[] Capitals;

        [Header("Правильный вариант")]
        public int CorrectVariant;

        [Header("Позиция на карте")]
        public Vector2Int Position;

        [Header("Префаб флага")]
        public AssetReferenceGameObject Flag;

        [Header("Цвета для флага")]
        public Color[] Colors;
    }
}