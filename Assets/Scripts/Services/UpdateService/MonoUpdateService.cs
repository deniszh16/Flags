using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.UpdateService
{
    public class MonoUpdateService : MonoBehaviour, IMonoUpdateService
    {
        private List<Action> _updateActions = new();
        
        public void AddToUpdate(Action action) =>
            _updateActions.Add(action);
        
        public void RemoveFromUpdate(Action action) =>
            _updateActions.Remove(action);
        
        private void Update()
        {
            for (int i = 0; i < _updateActions.Count; i++)
                _updateActions[i]?.Invoke();
        }
    }
}