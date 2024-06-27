using System;
using UnityEngine;
using VContainer;

namespace Flags.Services
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        public bool SoundActivity { get; private set; }
        
        public event Action SoundChanged;
        
        [Header("Фоновая музыка")]
        [SerializeField] private AudioSource _audioSourceBackgroundMusic;
        [SerializeField] private AudioClip[] _audioClips;
        
        [Header("Игровые звуки")]
        [SerializeField] private AudioSource _audioSourceSounds;
        [SerializeField] private AudioClip[] _uiSounds;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        
        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void ChangeSoundActivity(bool state) =>
            SoundActivity = state;

        public void SwitchSound()
        {
            bool activity = _progressService.GetUserProgress.SettingsData.Sound;
            SoundActivity = !activity;

            _progressService.GetUserProgress.SettingsData.Sound = SoundActivity;
            _saveLoadService.SaveProgress();
            
            SoundChanged?.Invoke();
            SettingBackgroundMusic();
        }

        public void SettingBackgroundMusic()
        {
            if (SoundActivity)
            {
                if (_audioSourceBackgroundMusic.isPlaying == false)
                {
                    int randomMusic = UnityEngine.Random.Range(0, _audioClips.Length);
                    _audioSourceBackgroundMusic.clip = _audioClips[randomMusic];
                    _audioSourceBackgroundMusic.Play();
                }
            }
            else
            {
                _audioSourceBackgroundMusic.Stop();
            }
        }

        public void PlaySound(Sounds sound)
        {
            if (SoundActivity)
            {
                _audioSourceSounds.clip = _uiSounds[(int)sound];
                _audioSourceSounds.Play();
            }
        }
    }
}