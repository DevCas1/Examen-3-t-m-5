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
        [Tooltip("Specifies whether or not the input axis will go from 0 to -1, instead of 1.")]
        public bool InvertAxis;
        [Tooltip("The Vector3Variable to reference.")]
        public Vector3Reference InputReference;
    }

    [System.Serializable]
    public sealed class SingleInputAxisObject
    {
        [Tooltip("The axis name as written inside the Unity Input System.")]
        public string AxisName;
        [Tooltip("Specifies whether or not the input axis will go from 0 to -1, instead of 1.")]
        public bool InvertAxis;
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
                if (axis.AxisName == string.Empty || axis.InputReference == null) continue;
                UpdateSingleInputAxis(axis);
            }
        }

        private void UpdateSingleInputAxis(SingleInputAxisObject axis) => axis.InputReference.Variable.Value = GetAxis(axis.AxisName, axis.InvertAxis);

        private float GetAxis(string axis, bool invert)
        {
            return !invert ? Input.GetAxis(axis) : -Input.GetAxis(axis);
        }

        private void CheckV3InputAxes()
        {
            foreach (var axis in V3AxesToUpdate)
            {
                if (axis.AxisName == string.Empty || axis.InputReference == null) continue;
                UpdateV3InputAxis(axis);
            }
        }

        private void UpdateV3InputAxis(InputAxisObject axis)
        {
            switch (axis.CorrespondingInputAxis)
            {
                case InputAxis.X:
                    axis.InputReference.Variable.Value.x = GetAxis(axis.AxisName, axis.InvertAxis);
                    return;

                case InputAxis.Y:
                    axis.InputReference.Variable.Value.y = GetAxis(axis.AxisName, axis.InvertAxis);
                    return;

                case InputAxis.Z:
                    axis.InputReference.Variable.Value.z = GetAxis(axis.AxisName, axis.InvertAxis);
                    return;
            }
        }

        private void CheckInputButtons()
        {
            foreach (var button in ButtonsToUpdate)
            {
                if (button.CorrespondingGameEvent == null) continue;
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