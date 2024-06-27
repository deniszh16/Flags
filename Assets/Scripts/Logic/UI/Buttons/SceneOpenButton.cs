using System.Threading;
using Cysharp.Threading.Tasks;
using Flags.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Flags.Logic
{
    public class SceneOpenButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        
        [Header("Сцена для загрузки")]
        [SerializeField] private Scenes _scene;

        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoaderService = sceneLoaderService;

        private void OnEnable() =>
            _button.onClick.AddListener(GoToScene);
        
        private void OnDisable() =>
            _button.onClick.RemoveListener(GoToScene);

        private void GoToScene() =>
            _sceneLoaderService.LoadSceneAsync(_scene, screensaver: true, delay: 0f, CancellationToken.None).Forget();
    }
}