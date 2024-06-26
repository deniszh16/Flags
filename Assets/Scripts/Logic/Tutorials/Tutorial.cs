﻿using Cysharp.Threading.Tasks;
using UnityEngine;
using System;

namespace Flags.Logic
{
    public class Tutorial : MonoBehaviour
    {
        [Header("Иконка нажатия")]
        [SerializeField] private GameObject _icon;

        public async UniTask ShowTutorialAsync(float delay, Vector2 position)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            _icon.transform.localPosition = position;
            ChangeVisibilityOfTutorial(state: true);
        }
        
        public void ChangeVisibilityOfTutorial(bool state) =>
            _icon.SetActive(state);
    }
}