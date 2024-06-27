using Flags.Data;

namespace Flags.Services
{
    public interface IPersistentProgressService
    {
        public UserProgress GetUserProgress { get; }
        public void SetUserProgress(UserProgress progress);
    }
}