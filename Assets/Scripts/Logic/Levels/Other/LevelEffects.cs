using Logic.Sound;
using Services.Sound;
using UnityEngine;

namespace Logic.Levels.Other
{
    public class LevelEffects : MonoBehaviour
    {
        [Header("Звуки эффектов")]
        [SerializeField] private PlayingSound _playingSound;

        [Header("Эффекты уровня")]
        [SerializeField] private ParticleSystem _finishingDrawing;
        [SerializeField] private ParticleSystem _flagColorized;
        [SerializeField] private ParticleSystem _wrongColoring;
        [SerializeField] private ParticleSystem[] _confetti;
        
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
            PlayEffectSound(Sounds.LevelСompleted);
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