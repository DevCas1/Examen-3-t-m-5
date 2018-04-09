namespace Sjouke.Input
{
    using UnityEngine;
    using CodeStructure.Variables;

    [CreateAssetMenu(menuName = "Player Settings/Input\tSet")]
    public sealed class PlayerInputContainer : ScriptableObject
    {
        public FloatVariable Steering;
        public FloatVariable Acceleration;
        public FloatVariable Braking;

        private void Reset()
        {
            Steering = null;
            Acceleration = null;
            Braking = null;
        }

        public void AssignSteering(FloatVariable input) => Steering = input;

        public void AssignAcceleration(FloatVariable input) => Acceleration = input;

        public void AssignBraking(FloatVariable input) => Braking = input;
    }

}