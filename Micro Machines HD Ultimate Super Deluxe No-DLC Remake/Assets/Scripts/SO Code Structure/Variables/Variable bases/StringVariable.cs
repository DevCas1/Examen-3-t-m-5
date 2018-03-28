﻿namespace Sjouke.CodeStructure.Variables
{
    using UnityEngine;

    [CreateAssetMenu]
    public class StringVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public string Value;
        public bool ResetAtAwake;
        public string DefaultValue;

        private void Awake()
        {
            if (!ResetAtAwake) return;
            Value = DefaultValue;
        }

        public void SetValue(string value)
        {
            Value = value;
        }

        public void SetValue(StringVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(string amount)
        {
            Value += amount;
        }

        public void ApplyChange(StringVariable amount)
        {
            Value += amount.Value;
        }
    }
}