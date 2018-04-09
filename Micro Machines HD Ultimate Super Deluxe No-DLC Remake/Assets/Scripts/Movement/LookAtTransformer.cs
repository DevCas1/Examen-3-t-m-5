namespace Sjouke.Simple
{
    using UnityEngine;

    public sealed class LookAtTransformer : MonoBehaviour
    {
        public Transform TargetTransform;

        private void Update()
        {
            transform.LookAt(TargetTransform.position);
        }
    }
}