namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BNFSystemRef : BNFSimpleElement
    {
        public SystemRule rule;

        public override bool Equals(object obj)
        {
            System.Diagnostics.Debug.WriteLine("Equals in BNFReference");
            System.Diagnostics.Debug.WriteLine("First:" + ToString());
            System.Diagnostics.Debug.WriteLine("Second:" + obj.ToString());
            if (obj is BNFSystemRef s)
                return rule.Literal.Equals(s.rule.Literal);
            return false;
        }

        public override string ToString()
        {
            return rule.Literal;
        }


        public BNFSystemRef(SystemRule systemRule)
        {
            rule = systemRule;
        }
    }
}