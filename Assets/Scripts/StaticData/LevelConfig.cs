using System;
using UnityEngine;
using UnityEngine.Localization;

namespace StaticData
{
    [Serializable]
    public class LevelConfig
    {
        [Header("Номер уровня")]
        public int Number;
        
        [Header("Название страны")]
        public LocalizedString LocalizedText;

        [Header("Позиция на карте")]
        public Vector2Int Position;

        [Header("Цвета для флага")]
        public Color[] Colors;
    }
}