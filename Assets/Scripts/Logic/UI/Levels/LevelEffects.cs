using System.Collections.Generic;
using DZGames.Flags.Services;
using UnityEngine;

namespace DZGames.Flags.Logic
{
    public class LevelEffects : MonoBehaviour
    {
        [Header("Звук эффектов")]
        [SerializeField] private PlayingSound _playingSound;

        [Header("Эффекты уровня")]
        [SerializeField] private ParticleSystem _finishingDrawing;
        [SerializeField] private ParticleSystem _flagColorized;
        [SerializeField] private ParticleSystem _wrongColoring;
        [SerializeField] private List<ParticleSystem> _confetti;
        
        public void ShowDrawingFinishEffect()
        {
            _finishingDrawing.gameObject.SetActive(true);
            _finishingDrawing.Play();
            PlayEffectSound(Sounds.FlagCreated);
        }

        public void ShowColorizedFlagEffect()
        {
            _flagColorized.gameObject.SetActive(true);
            _flagColorized.Play();
            PlayEffectSound(Sounds.FlagColored);
        }
        
        public void ShowEffectIncorrectColoring()
        {
            _wrongColoring.gameObject.SetActive(true);
            _wrongColoring.Play();
            PlayEffectSound(Sounds.IncorrectAnswer);
        }
        
        public void ShowEffectQuizResult(bool state)
        {
            if (state)
            {
                ShowColorizedFlagEffect();
                PlayEffectSound(Sounds.CorrectAnswer);
            }
            else
            {
                ShowEffectIncorrectColoring();
                PlayEffectSound(Sounds.IncorrectAnswer);
            }
        }
        
        public void ShowConfettiEffect()
        {
            PlayEffectSound(Sounds.LevelCompleted);
            foreach (ParticleSystem effect in _confetti)
            {
                effect.gameObject.SetActive(true);
                effect.Play();
            }
        }
        
        private void PlayEffectSound(Sounds sound)
        {
            _playingSound.ChangeSound(sound);
            _playingSound.PlaySound();
        }
    }
}