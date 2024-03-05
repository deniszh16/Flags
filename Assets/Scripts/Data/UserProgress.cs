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
        private int _hints = 0;

        public int Hints => _hints;
        
        public event Action ChangedNumberOfHints;

        [SerializeField]
        private bool _endlessHints;

        public bool EndlessHints
        {
            get => _endlessHints;
            set => _endlessHints = value;
        }

        [SerializeField]
        private bool _freeHint;
        
        public bool FreeHint
        {
            get => _freeHint;
            set => _freeHint = value;
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
        
        public void ChangeNumberOfHints(int value)
        {
            _hints += value;
            ChangedNumberOfHints?.Invoke();
        }
    }
}