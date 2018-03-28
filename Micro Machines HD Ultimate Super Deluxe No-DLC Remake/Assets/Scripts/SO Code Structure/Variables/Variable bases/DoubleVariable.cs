namespace Sjouke.CodeStructure.Variables
{
    using UnityEngine;

    [CreateAssetMenu]
    public class DoubleVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public double Value;
        public bool ResetAtAwake;
        public double DefaultValue;

        private void Awake()
        {
            if (!ResetAtAwake) return;
            Value = DefaultValue;
        }

        public void SetValue(double value)
        {
            Value = value;
        }

        public void SetValue(DoubleVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(double amount)
        {
            Value += amount;
        }

        public void ApplyChange(DoubleVariable amount)
        {
            Value += amount.Value;
        }
    }
}