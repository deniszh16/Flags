using UnityEngine;
using System;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        [SerializeField] private int _progress = 1;
        [SerializeField] private int _rightAnswers;
        [SerializeField] private int _wrongAnswers;
        
        [SerializeField] private int _hints = 3;
        [SerializeField] private bool _endlessHints;
        [SerializeField] private bool _freeHint;
        [SerializeField] private int _hintsUsed;

        public int Progress => _progress;
        public int RightAnswers => _rightAnswers;
        public int WrongAnswers => _wrongAnswers;
        
        public event Action ChangedNumberOfHints;

        public int Hints => _hints;
        public bool EndlessHints => _endlessHints;
        public bool FreeHint => _freeHint;
        public int HintsUsed => _hintsUsed;

        public SettingsData SettingsData { get; } = new();

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

        public void ChangeEndlessHints() =>
            _endlessHints = true;
        
        public void ChangeFreeHints() =>
            _freeHint = true;
        
        public void ChangeNumberOfHintsUsed() =>
            _hintsUsed++;
        
        public void ChangeNumberOfAnswers(bool answer)
        {
            if (answer) _rightAnswers++;
            else _wrongAnswers++;
        }
    }
}