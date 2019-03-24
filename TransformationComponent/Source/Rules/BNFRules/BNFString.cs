namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BNFString : BNFSimpleElement
    {
        public string Value;

        public override bool Equals(object obj)
        {
            if (obj is BNFString s)
                return Value.Equals(s.Value);
            return false;
        }
    }
}