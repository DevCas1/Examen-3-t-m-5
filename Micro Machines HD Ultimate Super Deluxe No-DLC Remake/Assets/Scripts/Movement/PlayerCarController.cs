namespace Sjouke.Controls.Car
{
    using UnityEngine;
    using CodeStructure.Variables;

    [System.Serializable]
    public sealed class Inputs
    {
        public Vector3Reference MovementInput;

    }

    [System.Serializable]
    public sealed class SpeedSettings
    {
        public FloatReference MaxSpeed;
        public FloatReference AccelerationFactor;
        public FloatReference DecelerationFactor;
    }

    [System.Serializable]
    public sealed class MovementSettings
    {
        public FloatReference TurnSpeed;
    }

    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerCarController : MonoBehaviour
    {
        public MovementSettings movementSettings = new MovementSettings();

        private Vector3 _velocity;

        private void Update()
        {
            UpdateVelocity();
        }

        private void UpdateVelocity()
        {

        }
    }
}