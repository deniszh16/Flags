using Flags.Services;
using UnityEngine;
using VContainer;

namespace Flags.Logic
{
    public class PlayingSound : MonoBehaviour
    {
        [Header("Автовоспроизведение")]
        [SerializeField] private bool _autoplay;
        
        [Header("Звук")]
        [SerializeField] private Sounds _sound;
        
        private ISoundService _soundService;

        [Inject]
        private void Construct(ISoundService soundService) =>
            _soundService = soundService;
        
        private void Start()
        {
            if (_autoplay)
                PlaySound();
        }

        public void PlaySound() =>
            _soundService.PlaySound(sound: _sound);
        
        public void ChangeSound(Sounds sound) =>
            _sound = sound;
    }
}