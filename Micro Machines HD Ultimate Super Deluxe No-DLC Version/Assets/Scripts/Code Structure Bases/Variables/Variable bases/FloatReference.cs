namespace Sjouke.CodeStructure.Variables
{
    using System;
    
    [Serializable]
    public sealed class FloatReference
    {
        public bool UseConstant = true;
        public float ConstantValue;
        public FloatVariable Variable;

        public float Value => UseConstant ? ConstantValue : Variable.Value;
    }
}