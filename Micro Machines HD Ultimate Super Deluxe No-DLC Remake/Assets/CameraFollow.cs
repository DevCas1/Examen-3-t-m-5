namespace Sjouke.Camera
{
    using UnityEngine;
    using CodeStructure.Variables;

    [System.Serializable]
    public sealed class FollowSettings
    {
        public Vector3 FollowOffset;
        public BoolReference SmoothFollow;
        [Range(0f, 100f)] public float FollowSpeed;
    }

    public class CameraFollow : MonoBehaviour
    {
        public Transform transformToFollow;
        public FollowSettings followSettings = new FollowSettings();
        
        void Update()
        {
            if (followSettings.SmoothFollow.Value)
                PerformSmoothFollow();
            else 
                PerformInstantFollow();
        }

        private void PerformSmoothFollow()
        {
            return;
        }

        private void PerformInstantFollow()
        {
            return;
        }
    }
}