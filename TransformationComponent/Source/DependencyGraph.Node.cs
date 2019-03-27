using System.Collections.Generic;

namespace ModelTransformationComponent
{
    public partial class DependencyGraph
    {
        public class Node
        {
            public Rule rule;
            public Node(Rule rule, bool isBase)
            {
                fromBase = isBase;
                this.rule = rule;
                Children = new List<Node>();
                Parent = new List<Node>();
            }


            public List<Node> Parent { get; }
            public List<Node> Children { get; }


            public bool fromBase { get; set; }

            public override string ToString()
            {
                var result = ((NamedRule)rule).Name;
                result += "\nParents: ";
                foreach (var item in Parent)
                {
                    result += ((NamedRule)item.rule).Name + " ";
                }
                result += "\nChildren: ";
                foreach (var item in Children)
                {
                    result += ((NamedRule)item.rule).Name + " ";
                }
                return result;
            }
        }
    }
}
