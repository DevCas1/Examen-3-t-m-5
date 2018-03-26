namespace Sjouke.Input
{
    using UnityEngine;
    using CodeStructure.Variables;
    using CodeStructure.Events;
    using InputAxis = InputAxisObject.InputAxis;
    using ButtonState = InputButtonObject.InputState;

    [System.Serializable]
    public class InputAxisObject
    {
        [Tooltip("The axis name as written inside the Unity Input System.")]
        public string AxisName;
        public enum InputAxis { X, Y, Z }
        [Tooltip("The specific axis within the Vector3 input.")]
        public InputAxis CorrespondingInputAxis;
        [Tooltip("The Vector3Variable to reference.")]
        public Vector3Reference InputReference;
    }

    [System.Serializable]
    public class InputButtonObject
    {
        [Tooltip("The function of the button")]
        public string ButtonName;
        [Tooltip("The specific button to listen to.")]
        public KeyCode Button;
        public enum InputState { Pressed, Hold, Released }
        [Tooltip("The specific button state to listen to.")]
        public InputState RequiredButtonState;
        [Tooltip("The GameEvent to call after action is true.")]
        public GameEvent CorrespondingGameEvent;
    }

    public class InputUpdater : MonoBehaviour
    {
        public InputAxisObject[] AxisToUpdate;
        public InputButtonObject[] ButtonsToUpdate;

        private void Update()
        {
            CheckInputAxises();
            CheckInputButtons();
        }

        private void CheckInputAxises()
        {
            foreach (var axis in AxisToUpdate)
            {
                UpdateInputAxis(axis);
            }
        }

        private void UpdateInputAxis(InputAxisObject axis)
        {
            switch (axis.CorrespondingInputAxis)
            {
                case InputAxis.X:
                    axis.InputReference.Variable.Value.x = Input.GetAxis(axis.AxisName);
                    return;

                case InputAxis.Y:
                    axis.InputReference.Variable.Value.y = Input.GetAxis(axis.AxisName);
                    return;

                case InputAxis.Z:
                    axis.InputReference.Variable.Value.z = Input.GetAxis(axis.AxisName);
                    return;
            }
        }

        private void CheckInputButtons()
        {
            foreach (var button in ButtonsToUpdate)
            {
                UpdateInputButton(button);
            }
        }

        private void UpdateInputButton(InputButtonObject button)
        {
            switch (button.RequiredButtonState)
            {
                case ButtonState.Pressed:
                    if (Input.GetKeyDown(button.Button)) button.CorrespondingGameEvent.Raise();
                    return;

                case ButtonState.Hold:
                    if (Input.GetKey(button.Button)) button.CorrespondingGameEvent.Raise();
                    return;

                case ButtonState.Released:
                    if (Input.GetKeyUp(button.Button)) button.CorrespondingGameEvent.Raise();
                    return;
            }
        }
    }
}