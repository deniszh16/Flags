using System;

namespace Flags.Services
{
    public interface ISoundService
    {
        public event Action SoundChanged;
        
        public bool SoundActivity { get; }
        public void ChangeSoundActivity(bool state);
        
        public void SwitchSound();
        public void SettingBackgroundMusic();
        public void PlaySound(Sounds sound);
    }
}