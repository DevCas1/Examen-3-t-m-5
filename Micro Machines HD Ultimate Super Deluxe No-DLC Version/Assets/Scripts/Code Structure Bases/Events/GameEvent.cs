namespace Sjouke.CodeStructure.Events
{
    using UnityEngine;
    using System.Collections.Generic;

    [CreateAssetMenu]
    public sealed class GameEvent : ScriptableObject
    {
        /// <summary>The list of listeners that this event will notify if it is raised.</summary>
        private readonly List<GameEventListener> _eventListeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int index = _eventListeners.Count - 1; index >= 0; index--)
                _eventListeners[index].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}