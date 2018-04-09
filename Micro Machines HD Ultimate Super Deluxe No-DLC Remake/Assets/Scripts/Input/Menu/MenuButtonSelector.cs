using UnityEngine;
using Sjouke.CodeStructure.Button;

public sealed class MenuButtonSelector : MonoBehaviour
{
    public LayerMask UILayer;
    public float RaycastDistance = 30;

    private RaycastHit _hit;
    private MenuButton _button;

    private void FixedUpdate() => CheckForMenuButton();

    private void CheckForMenuButton()
    {
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, RaycastDistance, UILayer))
        {
            if (_button != null)
                _button.OnButtonLeave();
            _button = null;
            return;
        }

        if (_button != null)
            if (_hit.transform == _button.transform) return;

        _button = _hit.transform.GetComponent<MenuButton>();
        _button.OnButtonHover();
    }

    public void OnMouseClick()
    {
        if (_button == null) return;
        _button.OnButtonPressed();
    }
}