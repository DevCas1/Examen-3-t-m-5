using System.Collections;
using Sjouke.CodeStructure.Events;
using UnityEngine;

public class EventOnDelayedStart : MonoBehaviour 
{
    public GameEvent Event;
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        Event.Raise();
        yield return null;
    }
}