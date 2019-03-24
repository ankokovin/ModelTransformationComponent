using System.Collections.Generic;

namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BNFRule : NamedRule
    {
        public BNFRule(string name) : base(name)
        {
            OrSplits = new List<BasicBNFRule>();
        }

        public List<BasicBNFRule> OrSplits;
    }
}