using System.Linq;
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

        public override bool Equals(object obj)
        {
            System.Diagnostics.Debug.WriteLine("Equals in TypeRule");
            System.Diagnostics.Debug.WriteLine("First:" + ToString());
            System.Diagnostics.Debug.WriteLine("Second:" + obj.ToString());
            var r = obj as TypeRule;
            return r != null && this.SequenceEqual(r);
        }
    }
}