namespace Sjouke.Controls.Car
{
    using UnityEngine;
    using CodeStructure.Variables;

    [System.Serializable]
    public sealed class Inputs
    {
        //public Vector3Reference MovementInput;
        public FloatReference SteeringInput;
        public FloatReference AccelerationInput;
        public FloatReference BrakeInput;
        public Transform RaycastTransform;
        public FloatReference RaycastDistance;
        public LayerMask GroundLayer;
    }

    [System.Serializable]
    public sealed class SpeedSettings
    {
        public FloatReference MaxSpeed;
        public FloatReference AccelerationFactor;
        public FloatReference DecelerationFactor;
        public FloatReference BrakeFactor;
    }

    [System.Serializable]
    public sealed class SteeringSettings
    {
        public FloatReference TurnSpeed;
        [Tooltip("How much traction does this car have?\n(1 for max, 0.1 for nearly none)"), Range(0.1f, 1)]
        public float DriftTraction = 0.75f;
    }

    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerCarController : MonoBehaviour
    {
        public Inputs Input = new Inputs();
        public SpeedSettings SpeedSettings = new SpeedSettings();
        public SteeringSettings SteeringSettings = new SteeringSettings();

        private Rigidbody _rigidbody;
        private Vector3 _velocity;
        private float _forwardVelocity;
        private readonly RaycastHit[] _hit = new RaycastHit[1];
        private bool _isGrounded;

        private void Reset()
        {
            ResetValues();
        }

        private void ResetValues()
        {
            // //////     INPUT     \\\\\\
            Input.SteeringInput.UseConstant = false;
            Input.AccelerationInput.UseConstant = false;
            Input.BrakeInput.UseConstant = false;

            // ////// SPEED SETTINGS \\\\\\ 
            if (SpeedSettings.MaxSpeed.UseConstant) SpeedSettings.MaxSpeed.ConstantValue = 1;
            if (SpeedSettings.AccelerationFactor.UseConstant) SpeedSettings.AccelerationFactor.ConstantValue = 1;
            if (SpeedSettings.DecelerationFactor.UseConstant) SpeedSettings.DecelerationFactor.ConstantValue = 1;

            // ////// MOVEMENT SETTINGS \\\\\\
            if (SteeringSettings.TurnSpeed.UseConstant) SteeringSettings.TurnSpeed.ConstantValue = 1;
        }

        private void Start()
        {
            GetPrivateReferences();
            ResetPrivateValues();
        }

        private void GetPrivateReferences()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void ResetPrivateValues()
        {
            _velocity = Vector3.zero;
            _forwardVelocity = 0;
        }

        private void Update()
        {
            _isGrounded = CheckForGround();

            if (!_isGrounded)
            {
                ApplyDownForce();
                return;
            }

            CalculateNewVelocity();
            UpdateVelocity();
            RotateCarObject();
            MoveCarObject();
        }

        private void CalculateNewVelocity()
        {
            float input = Input.AccelerationInput.Value - Input.BrakeInput.Value;
            if (Mathf.Abs(input) < float.Epsilon && Mathf.Abs(_forwardVelocity) > float.Epsilon)
            {
                if (_forwardVelocity > float.Epsilon)
                    _forwardVelocity -= SpeedSettings.DecelerationFactor.Value;
                else if (_forwardVelocity < float.Epsilon)
                    _forwardVelocity += SpeedSettings.DecelerationFactor.Value;
            }
            if (Mathf.Abs(input) < float.Epsilon && _forwardVelocity > float.Epsilon)
                _forwardVelocity -= -input * Input.BrakeInput.Value;
            else if (input > float.Epsilon)
                _forwardVelocity += input * Input.AccelerationInput.Value;
            else if (input < -float.Epsilon)
                _forwardVelocity -= -input * Input.BrakeInput.Value;
        }

        private void UpdateVelocity()
        {
            _forwardVelocity = Mathf.Clamp(_forwardVelocity, -SpeedSettings.MaxSpeed.Value, SpeedSettings.MaxSpeed.Value);
            _velocity = transform.forward * _forwardVelocity;
        }

        private void ApplyDownForce()
        {
            _velocity += Vector3.down * 0.02f;
        }

        private void RotateCarObject()
        {
            transform.Rotate(0, 
                            (_forwardVelocity > 0 ? Input.SteeringInput.Value 
                                                  : -Input.SteeringInput.Value) * SteeringSettings.TurnSpeed.Value * (_velocity.magnitude / SpeedSettings.MaxSpeed.Value) * Time.deltaTime, 
                             0);
        }

        private void MoveCarObject()
        {
            _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.deltaTime);
        }

        private bool CheckForGround()
        {
            return Physics.RaycastNonAlloc(Input.RaycastTransform.position, -transform.up, _hit, Input.RaycastDistance.Value, Input.GroundLayer) > 0;
        }

        //public void ApplyDriftTraction(bool enable)
        //{
        //    if (!_isGrounded) return;
        //}
    }
}