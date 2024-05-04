using DZGames.Flags.Data;

namespace DZGames.Flags.Services
{
    public interface ISaveLoadService
    {
        public void SaveProgress();
        public UserProgress LoadProgress();
    }
}