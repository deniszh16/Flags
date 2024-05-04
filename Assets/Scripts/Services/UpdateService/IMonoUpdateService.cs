using System;

namespace DZGames.Flags.Services
{
    public interface IMonoUpdateService
    {
        public void AddToUpdate(Action action);
        public void RemoveFromUpdate(Action action);
    }
}