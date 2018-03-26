namespace Sjouke.CodeStructure.Variables
{
    using System;

    /// <summary>A specified float variable.</summary>
    [Serializable]
    public sealed class BoolReference
    {
        public bool UseConstant = true;
        public bool ConstantValue;
        public BoolVariable Variable;

        public bool Value => UseConstant ? ConstantValue : Variable.Value;
    }
}