using Cysharp.Threading.Tasks;

namespace Services.SceneLoader
{
    public interface ISceneLoaderService
    {
        public UniTask LoadSceneAsync(Scenes scene, bool screensaver, float delay);
    }
}