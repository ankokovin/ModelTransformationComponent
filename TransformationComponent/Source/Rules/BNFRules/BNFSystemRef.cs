namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BNFSystemRef : BNFSimpleElement
    {
        public SystemRule rule;

        public override bool Equals(object obj)
        {
            if (obj is BNFSystemRef s)
                return rule.Literal.Equals(s.rule.Literal);
            return false;
        }

        public override string ToString()
        {
            return rule.Literal;
        }
    }
}