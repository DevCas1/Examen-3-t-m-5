using UnityEngine;
using Sjouke.CodeStructure.Button;
using Sjouke.CodeStructure.Events;

[RequireComponent(typeof(MenuButton))]
public sealed class MenuButtonSelector : MonoBehaviour
{
    public LayerMask UILayer;
    public float RaycastDistance = 30;
    [Space(5)]
    public GameObjectReference SelectedMenuButton;
    public GameEvent OnMenuButtonChange;
    private RaycastHit _hit;

    void Update()
    {
        CheckForMenuButton();
    }

    private void CheckForMenuButton()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * RaycastDistance, Color.green);
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, RaycastDistance, UILayer)) return;
        Debug.Log(_hit.transform.name);
        var button = _hit.transform.GetComponent<MenuButton>();
        if (button != null && button.gameObject != SelectedMenuButton.Reference)
            OnMenuButtonChange.Raise();
        SelectedMenuButton.Reference = button != null ? button.gameObject : null;
    }
}