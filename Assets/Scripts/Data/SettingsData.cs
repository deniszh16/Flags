using UnityEngine;
using System;

namespace Data
{
    [Serializable]
    public class SettingsData
    {
        [SerializeField] private int _locale;
        [SerializeField] private bool _sound = true;

        public int Locale { get => _locale; set => _locale = value; }
        public bool Sound { get => _sound; set => _sound = value; }
    }
}