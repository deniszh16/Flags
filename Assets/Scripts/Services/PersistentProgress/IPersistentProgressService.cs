using DZGames.Flags.Data;

namespace DZGames.Flags.Services
{
    public interface IPersistentProgressService
    {
        public UserProgress GetUserProgress { get; }
        public void SetUserProgress(UserProgress progress);
    }
}