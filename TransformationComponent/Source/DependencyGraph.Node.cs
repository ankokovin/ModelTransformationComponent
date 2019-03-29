using System.Collections.Generic;

namespace ModelTransformationComponent
{
    public partial class DependencyGraph
    {
        /// <summary>
        /// Вершина правила в графе зависимостей <see cref="DependencyGraph"/>
        /// </summary>
        public class Node
        {
            /// <summary>
            /// Правило
            /// </summary>
            public Rule rule;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="rule"></param>
            /// <param name="isBase"></param>
            public Node(Rule rule, bool isBase)
            {
                fromBase = isBase;
                this.rule = rule;
                Children = new List<Node>();
                Parent = new List<Node>();
            }

            /// <summary>
            /// Правила, которые используют данные
            /// </summary>
            /// <value></value>
            public List<Node> Parent { get; }

            /// <summary>
            /// Правила, которые используются данными
            /// </summary>
            /// <value></value>
            public List<Node> Children { get; }

            /// <summary>
            /// 
            /// </summary>
            /// <value></value>
            public bool fromBase { get; set; }


            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
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
