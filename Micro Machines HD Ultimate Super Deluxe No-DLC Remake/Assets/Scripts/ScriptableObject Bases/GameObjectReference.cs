using UnityEngine;

[CreateAssetMenu(menuName = "Custom/GameObject\tReference")]
public sealed class GameObjectReference : ScriptableObject 
{
    public GameObject Reference;
    public bool ResetAtStart = true;

    private void Awake() => Reference = ResetAtStart ? null : Reference;
}