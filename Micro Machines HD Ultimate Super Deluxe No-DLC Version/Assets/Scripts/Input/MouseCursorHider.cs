namespace Sjouke.Input
{
    using UnityEngine;
    using CodeStructure.Variables;

    public class MouseCursorHider : MonoBehaviour 
    {
        public BoolReference HideOnStart;

        private void Start()
        {
            StartChecks();
        }

        private void StartChecks()
        {
            if (HideOnStart.Value)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void SwitchCursorLockMode()
        {
            Cursor.lockState = Cursor.visible ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !Cursor.visible;
        }
    }
}