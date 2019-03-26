namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BNFString : BNFSimpleElement
    {
        public string Value;

        public override bool Equals(object obj)
        {
            System.Diagnostics.Debug.WriteLine("Equals in BNFReference");
            System.Diagnostics.Debug.WriteLine("First:" + ToString());
            System.Diagnostics.Debug.WriteLine("Second:" + obj.ToString());
            if (obj is BNFString s)
                return Value.Equals(s.Value);
            return false;
        }

        public override string ToString()
        {
            return Value;
        }

        public BNFString(string val)
        {
            Value = val;
        }
    }
}