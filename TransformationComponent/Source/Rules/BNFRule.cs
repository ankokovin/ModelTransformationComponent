using System.Collections.Generic;

namespace ModelTransformationComponent
{
    class BNFRule : NamedRule
    {
        public BNFRule(string name) : base(name)
        {
            OrSplits = new List<BasicBNFRule>();
        }

        public List<BasicBNFRule> OrSplits;
    }
}