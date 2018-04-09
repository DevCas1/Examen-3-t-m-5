using Sjouke.CodeStructure.Events;
using UnityEngine;

public class EventOnStart : MonoBehaviour 
{
    public GameEvent Event;
    private void Start() => Event.Raise();
}