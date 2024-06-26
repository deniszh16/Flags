﻿using Flags.Services;
using UnityEngine;
using VContainer;

namespace Flags.Logic
{
    public class BackgroundMusic : MonoBehaviour
    {
        private ISoundService _soundService;

        [Inject]
        private void Construct(ISoundService soundService) =>
            _soundService = soundService;

        private void Start() =>
            _soundService.SettingBackgroundMusic();
    }
}