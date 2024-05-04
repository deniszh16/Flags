using DZGames.Flags.Data;

namespace DZGames.Flags.Services
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public UserProgress GetUserProgress { get; private set; }

        public void SetUserProgress(UserProgress progress) =>
            GetUserProgress = progress;
    }
}