using System.Collections.Generic;

namespace ModelTransformationComponent
{
    public partial class DependencyGraph
    {
        public class BNode
        {
            public BNode Parrent { get; set; }

            public List<BNode> Children { get; }
            public string Value { get; set; }

            public string RuleName { get; set; }

            public BNode(string name)
            {
                RuleName = name;
                Children = new List<BNode>();
                isRef = false;
            }

            public bool isRef { get; set; }
        }
    }
}
