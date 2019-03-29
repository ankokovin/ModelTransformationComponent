using System.Collections.Generic;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Граф зависимостей правил
    /// </summary>
    public partial class DependencyGraph
    {

        /// <summary>
        /// Вершина дерева парсинга
        /// </summary>
        public class BNode
        {
            /// <summary>
            /// Родитель вершины
            /// </summary>
            public BNode Parent { get; set; }


            /// <summary>
            /// Дети вершины
            /// </summary>
            public List<BNode> Children { get; }

            /// <summary>
            /// Параметр вершиины
            /// </summary>
            public string Value { get; set; }

            /// <summary>
            /// Название правила
            /// </summary>
            public string RuleName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="name">Название</param>
            public BNode(string name)
            {
                RuleName = name;
                Children = new List<BNode>();
                isRef = false;
            }

            /// <summary>
            /// Является ли ссылкой
            /// </summary>
            public bool isRef { get; set; }
        }
    }
}
