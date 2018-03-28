namespace Sjouke.CodeStructure.Variables
{
    using UnityEngine;

    [CreateAssetMenu]
    public sealed class Vector3Variable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Vector3 Value;
        public bool ResetAtAwake;
        public Vector3 DefaultValue;

        private void Awake()
        {
            if (!ResetAtAwake) return;
            Value = DefaultValue;
        }

        public void SetValue(Vector3 value)
        {
            Value = value;
        }

        public void SetValue(Vector3Variable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(Vector3 amount)
        {
            Value += amount;
        }

        public void ApplyChange(Vector3Variable amount)
        {
            Value += amount.Value;
        }
    }
}