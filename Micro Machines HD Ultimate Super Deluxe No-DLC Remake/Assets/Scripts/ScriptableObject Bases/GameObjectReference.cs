using UnityEngine;

[CreateAssetMenu(menuName = "Custom/GameObject\tReference")]
public class GameObjectReference : ScriptableObject
{
    public GameObject Reference;
    public bool ResetAtStart = true;

    internal virtual void Awake()
    {
        if (ResetAtStart)
            Reference = null;
    }

    internal virtual void OnDisable()
    {
        if (ResetAtStart)
            Reference = null;
    }
}