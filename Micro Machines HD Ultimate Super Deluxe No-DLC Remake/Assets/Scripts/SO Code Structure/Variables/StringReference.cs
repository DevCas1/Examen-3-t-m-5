namespace Sjouke.CodeStructure.Variables
{
    [System.Serializable]
    public class StringReference
    {
        public bool UseConstant = true;
        public string ConstantValue;
        public StringVariable Variable;

        public string Value { get { return NewMethod(); } }

        private string NewMethod()
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }
    }
}