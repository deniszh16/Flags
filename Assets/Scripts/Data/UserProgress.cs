using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        [SerializeField]
        private int _progress = 1;

        public int Progress =>
            _progress;

        [SerializeField]
        private int _hints = 3;

        public int Hints
        {
            get => _hints;
            set
            {
                _hints += value;
                if (_hints < 0) _hints = 0;
            }
        }
        
        [SerializeField]
        private int _locale;

        public int Locale
        {
            get => _locale;
            set => _locale = value;
        }
        
        private bool _sound;

        public void IncreaseProgress() =>
            _progress++;
    }
}