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
    public sealed class SingleInputAxisObject
    {
        [Tooltip("The axis name as written inside the Unity Input System.")]
        public string AxisName;
        [Tooltip("The Vector3Variable to reference.")]
        public FloatReference InputReference;
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
        public ButtonState RequiredButtonState;
        [Tooltip("The GameEvent to call after action is true.")]
        public GameEvent CorrespondingGameEvent;
    }

    public class InputUpdater : MonoBehaviour
    {
        [Tooltip("Put all single axis values to update here. (E.g. controller triggers)")]
        public SingleInputAxisObject[] AxesToUpdate;
        [Tooltip("Put all Vector3 axis to update here (e.g. Horizontal and Vertical Movement, mouse position etc.)")]
        public InputAxisObject[] V3AxesToUpdate;
        [Tooltip("Put all buttons to update here.")]
        public InputButtonObject[] ButtonsToUpdate;

        private void Update()
        {
            CheckSingleInputAxes();
            CheckV3InputAxes();
            CheckInputButtons();
        }

        private void CheckSingleInputAxes()
        {
            foreach (var axis in AxesToUpdate)
            {
                UpdateSingleInputAxis(axis);
            }
        }

        private void UpdateSingleInputAxis(SingleInputAxisObject axis)
        {
            axis.InputReference.Variable.Value = Input.GetAxis(axis.AxisName);
        }

        private void CheckV3InputAxes()
        {
            foreach (var axis in V3AxesToUpdate)
            {
                UpdateV3InputAxis(axis);
            }
        }

        private void UpdateV3InputAxis(InputAxisObject axis)
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