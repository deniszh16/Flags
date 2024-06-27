using UnityEngine;
using System;

namespace Flags.Data
{
    [Serializable]
    public class SettingsData
    {
        public int Locale
        {
            get => _locale;
            set => _locale = value;
        }

        public bool Sound
        {
            get => _sound;
            set => _sound = value;
        }
        
        [SerializeField] private int _locale;
        [SerializeField] private bool _sound = true;

        public SettingsData() =>
            _locale = Application.systemLanguage == SystemLanguage.Russian ? 0 : 1;
    }
}