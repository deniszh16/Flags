using UnityEngine;

namespace Logic.Levels.Other
{
    public class LevelEffects : MonoBehaviour
    {
        [Header("Завершение рисования")]
        [SerializeField] private ParticleSystem _finishingDrawing;

        [Header("Завершение фрагмента")]
        [SerializeField] private ParticleSystem _flagColorized;

        [Header("Ошибочкая раскраска")]
        [SerializeField] private ParticleSystem _wrongColoring;

        [Header("Победное конфетти")]
        [SerializeField] private ParticleSystem[] _confetti;
        
        public void ShowDrawingFinishEffect()
        {
            _finishingDrawing.gameObject.SetActive(true);
            _finishingDrawing.Play();
        }

        public void ShowColorizedFlagEffect()
        {
            _flagColorized.gameObject.SetActive(true);
            _flagColorized.Play();
        }
        
        public void ShowEffectIncorrectColoring()
        {
            _wrongColoring.gameObject.SetActive(true);
            _wrongColoring.Play();
        }
        
        public void ShowEffectQuizResult(bool state)
        {
            if (state) ShowColorizedFlagEffect();
            else ShowEffectIncorrectColoring();
        }
        
        public void ShowConfettiEffect()
        {
            foreach (ParticleSystem effect in _confetti)
            {
                effect.gameObject.SetActive(true);
                effect.Play();
            }
        }
    }
}