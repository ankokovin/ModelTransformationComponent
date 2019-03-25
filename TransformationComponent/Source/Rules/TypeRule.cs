using ModelTransformationComponent.SystemRules;

namespace ModelTransformationComponent
{
    [System.Serializable]
    public class TypeRule : BNFRule
    {

        public System.Collections.Generic.List<BNFReference> RefList;

        public TypeRule(string name) : base(name)
        {
            RefList = new System.Collections.Generic.List<BNFReference>();
        }

        public override string ToString()
        {
            return "Type " + base.ToString();
        }
    }
}