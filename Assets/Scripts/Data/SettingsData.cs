using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class SettingsData
    {
        [SerializeField] private int _locale;
        [SerializeField] private bool _sound;

        public int Locale { get => _locale; set => _locale = value; }
    }
}