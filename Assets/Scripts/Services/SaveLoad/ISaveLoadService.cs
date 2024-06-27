using Flags.Data;

namespace Flags.Services
{
    public interface ISaveLoadService
    {
        public void SaveProgress();
        public UserProgress LoadProgress();
    }
}