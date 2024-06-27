using System.Threading;
using Cysharp.Threading.Tasks;

namespace Flags.Services
{
    public interface ISceneLoaderService
    {
        public UniTask LoadSceneAsync(Scenes scene, bool screensaver, float delay, CancellationToken token);
    }
}