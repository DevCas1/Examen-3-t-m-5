namespace Sjouke.CodeStructure.Variables
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Variable/String\tVariable")]
    public class StringVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline(4)]
        public string DeveloperDescription = "";
#endif
        public string Value;
        public bool ResetAtAwake;
        public string DefaultValue;

        private void Awake()
        {
            if (ResetAtAwake) Value = DefaultValue;
        }

        public void SetValue(string value) => Value = value;

        public void SetValue(StringVariable value) => Value = value.Value;

        public void ApplyChange(string amount) => Value += amount;

        public void ApplyChange(StringVariable amount) => Value += amount.Value;
    }
}