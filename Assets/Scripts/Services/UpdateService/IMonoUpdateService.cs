using System;

namespace Flags.Services
{
    public interface IMonoUpdateService
    {
        public void AddToUpdate(Action action);
        public void RemoveFromUpdate(Action action);
    }
}