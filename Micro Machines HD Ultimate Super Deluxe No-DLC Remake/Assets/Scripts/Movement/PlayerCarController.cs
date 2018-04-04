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
        }

        private void GetPrivateReferences()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _isGrounded = CheckForGround();
            UpdateVelocity();
            if (!_isGrounded)
                ApplyDownForce();
            RotateCarObject();
            MoveCarObject();
        }

        private void UpdateVelocity()
        {
            if (!_isGrounded) return;
            _velocity = transform.forward * Input.AccelerationInput.Value;
            _velocity = Vector3.ClampMagnitude(_velocity, SpeedSettings.MaxSpeed.Value);

            //float forwardVelocity = Input.AccelerationInput.Value - Input.BrakeInput.Value;
            //Vector3 tempVelocity = transform.TransformDirection(new Vector3(0, 0, forwardVelocity * (Input.BrakeInput.Value > 0.1 ? -SpeedSettings.BrakeFactor.Value
            //                                                                                                                      : (Input.AccelerationInput.Value > 0.1 ? SpeedSettings.AccelerationFactor.Value
            //                                                                                                                                                             : -SpeedSettings.DecelerationFactor.Value * _velocity.magnitude))));

            //_velocity = Vector3.ClampMagnitude(Vector3.Lerp(_velocity,
            //                                                tempVelocity,
            //                                                SteeringSettings.Traction * Time.deltaTime),
            //                                   SpeedSettings.MaxSpeed.Value);
        }

        private void ApplyDownForce()
        {
            _velocity += Vector3.down * 0.02f/** Physics.gravity.magnitude*/;
        }

        private void RotateCarObject()
        {
            if (!_isGrounded) return;
            transform.Rotate(0, (Input.AccelerationInput.Value > 0 ? Input.SteeringInput.Value : -Input.SteeringInput.Value) * SteeringSettings.TurnSpeed.Value * (_velocity.magnitude / SpeedSettings.MaxSpeed.Value), 0);
        }

        private void MoveCarObject()
        {
            //if (!_isGrounded) return;
            _rigidbody.MovePosition(_rigidbody.position + _velocity/*, ForceMode.VelocityChange*/);
        }

        private bool CheckForGround()
        {
            return Physics.RaycastNonAlloc(Input.RaycastTransform.position, -transform.up, _hit, Input.RaycastDistance.Value, Input.GroundLayer) > 0;
        }

        public void ApplyDriftTraction(bool enable)
        {
            if (!_isGrounded) return;
        }
    }
}