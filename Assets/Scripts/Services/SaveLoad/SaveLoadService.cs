﻿using Flags.Data;
using UnityEngine;

namespace Flags.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "MyProgress";
        
        private readonly IPersistentProgressService _persistentProgress;
        
        public SaveLoadService(IPersistentProgressService progressService) =>
            _persistentProgress = progressService;
        
        public void SaveProgress() =>
            PlayerPrefs.SetString(ProgressKey, _persistentProgress.GetUserProgress.ToJson());

        public UserProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<UserProgress>();
    }
}