namespace Sjouke.CodeStructure.Variables
{
    using System;
    
    [Serializable]
    public sealed class IntReference
    {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public int Value => UseConstant ? ConstantValue : Variable.Value;
    }
}