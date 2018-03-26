namespace Sjouke.CodeStructure.Variables
{
    using System;
    
    [Serializable]
    public sealed class DoubleReference
    {
        public bool UseConstant = true;
        public double ConstantValue;
        public DoubleVariable Variable;

        public double Value => UseConstant ? ConstantValue : Variable.Value;
    }
}