namespace Sjouke.CodeStructure.Variables
{
    using System;

    [Serializable]
    public sealed class StringReference
    {
        public bool UseConstant = true;
        public string ConstantValue;
        public StringVariable Variable;

        public string Value => UseConstant ? ConstantValue : Variable.Value;
    }
}