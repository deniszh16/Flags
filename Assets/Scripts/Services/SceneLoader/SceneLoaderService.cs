using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System;

namespace Services.SceneLoader
{
    public class SceneLoaderService : MonoBehaviour, ISceneLoaderService
    {
        [Header("Экран затемнения")]
        [SerializeField] private CanvasGroup _blackout;

        public async UniTask LoadSceneAsync(Scenes scene, bool screensaver, float delay)
        {
            if (delay > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
            
            if (screensaver)
            {
                _blackout.blocksRaycasts = true;
                _blackout.alpha = 1f;
            }
            
            await SceneManager.LoadSceneAsync(scene.ToString());
            
            if (screensaver)
            {
                _blackout.blocksRaycasts = false;
                _blackout.alpha = 0;
            }
        }
    }
}