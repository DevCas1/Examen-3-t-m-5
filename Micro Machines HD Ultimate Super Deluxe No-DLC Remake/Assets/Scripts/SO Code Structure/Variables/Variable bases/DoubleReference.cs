namespace Sjouke.CodeStructure.Variables
{
    using System;
    
    [Serializable]
    public class DoubleReference
    {
        public bool UseConstant = true;
        public double ConstantValue;
        public DoubleVariable Variable;

        public double Value { get { return UseConstant ? ConstantValue : Variable.Value; } }
    }
}