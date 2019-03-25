using ModelTransformationComponent.SystemRules;
using System.Collections;
using System.Collections.Generic;

namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BNFRule : NamedRule, IEnumerable<BasicBNFRule>
    {
        public BNFRule(string name) : base(name)
        {
            OrSplits = new List<BasicBNFRule>();
        }

        public readonly List<BasicBNFRule> OrSplits;

        public IEnumerator<BasicBNFRule> GetEnumerator()
        {
            return ((IEnumerable<BasicBNFRule>)OrSplits).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<BasicBNFRule>)OrSplits).GetEnumerator();
        }

        public override string ToString()
        {
            var result = "BNFRule " + Name + "\n";
            foreach (var i in OrSplits)
                result += i.ToString();
            return result;
        }

        public override bool Equals(object obj)
        {
            return (obj is BNFRule r)&& (!(obj is TypeRule)) &&(Name.Equals(r.Name))&&
                /* OrSplits.Equals(r.OrSplits)*/ ToString().Equals(r.ToString());
        }
    }
}