using UnityEngine;
using System;

namespace Flags.Data
{
    [Serializable]
    public class UserProgress
    {
        public event Action ChangedNumberOfHints;
        
        public int Progress
        {
            get => _progress;
            set => _progress = value;
        }
        
        public int RightAnswers
        {
            get => _rightAnswers;
            set => _rightAnswers = value;
        }

        public int WrongAnswers
        {
            get => _wrongAnswers;
            set => _wrongAnswers = value;
        }

        public int Hints
        {
            get => _hints;
            set => _hints = value;
        }

        public bool FreeHint
        {
            get => _freeHint;
            set => _freeHint = value;
        }

        public int HintsUsed
        {
            get => _hintsUsed;
            set => _hintsUsed = value;
        }
        
        public SettingsData SettingsData =>
            _settingsData;
        
        [SerializeField] private int _progress = 1;
        [SerializeField] private int _rightAnswers;
        [SerializeField] private int _wrongAnswers;
        [SerializeField] private int _score;

        [SerializeField] private int _hints = 3;
        [SerializeField] private bool _freeHint;
        [SerializeField] private int _hintsUsed;
        
        [SerializeField] private SettingsData _settingsData = new();
        
        public void IncreaseProgress() =>
            _progress++;

        public void ChangeNumberOfRightAndWrongAnswers(bool state)
        {
            if (state) _rightAnswers++;
            else _wrongAnswers++;
        }

        public void ChangeNumberOfHints(int value)
        {
            _hints += value;
            ChangedNumberOfHints?.Invoke();
        }
        
        public void ChangeFreeHints() =>
            _freeHint = true;
        
        public void ChangeNumberOfHintsUsed() =>
            _hintsUsed++;
        
        public void ChangeNumberOfAnswers(bool answer)
        {
            if (answer) _rightAnswers++;
            else _wrongAnswers++;
        }
        
        public int GetPlayerScore()
        {
            _score = _progress * 5 + _rightAnswers * 3 + _wrongAnswers * 2 - _hintsUsed;
            return _score;
        }
    }
}