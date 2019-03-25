using System.Linq;
using System.Collections.Generic;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Бор
    /// </summary>
    /// <typeparam name="T">Хранимый тип</typeparam>
    public class Trie<T>
    {
        /// <summary>
        /// Вершина бора <see cref="Trie{T}"/>
        /// </summary>
        class Node
        {
            /// <summary>
            /// Символ, по которому зашти из предка
            /// </summary>
            public char Value;
            /// <summary>
            /// Хранимое значение
            /// </summary>
            public T Result { get; set; }

            /// <summary>
            /// Потомки
            /// </summary>
            public Dictionary<char, Node> Children { get; set; }

            /// <summary>
            /// Предок
            /// </summary>
            public Node Parent { get; set; }

            /// <summary>
            /// Глубина обхода - длина строки, соответствующей данной вершине
            /// </summary>
            public int Depth { get; set; }

            /// <summary>
            /// Конструктор <see cref="Node"/>
            /// </summary>
            /// <param name="depth">Глубина</param>
            /// <param name="parent">Предок</param>
            /// <param name="value">Символ</param>
            public Node(int depth, Node parent, char value)
            {
                Children = new Dictionary<char, Node>();
                Depth = depth;
                Parent = parent;
                Value = value;
            }

            /// <summary>
            /// Является ли вершина листом
            /// </summary>
            /// <returns><see cref="System.Boolean"/></returns>
            public bool IsLeaf()
            {
                return Children.Count == 0;
            }

            /// <summary>
            /// Поиск потомка с данным символом
            /// </summary>
            /// <param name="c">Символ</param>
            /// <returns>Потомок (null если отсутствует)</returns>
            public Node FindChildNode(char c)
            {
                if (Children.ContainsKey(c))
                {
                    return Children[c];
                }
                return null;
            }

            /// <summary>
            /// Удаление потомка
            /// </summary>
            /// <param name="c">Символ</param>
            public void DeleteChildNode(char c)
            {
                Children.Remove(c);
            }
        }
        /// <summary>
        /// Корень бора
        /// </summary>
        private readonly Node _root;

        /// <summary>
        /// Конструктор <see cref="Trie{T}"/>
        /// </summary>
        public Trie()
        {
            _root = new Node(0, null, '^');
        }
        
        /// <summary>
        /// Функция поиска наибольшего префикса строки
        /// </summary>
        /// <param name="s">Строка</param>
        /// <returns>Вершина, соответствующая наибольшему префиксу</returns>
        private Node Prefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                currentNode = currentNode.FindChildNode(c);
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }

        private int maxDepth = 0;

        /// <summary>
        /// Добавление в бор
        /// </summary>
        /// <param name="Item">Хранимый элемент</param>
        /// <param name="Name">Строка элемента</param>
        public void Insert(T Item, string Name)
        {
            maxDepth = maxDepth < Name.Length ? Name.Length : maxDepth;
            var commonPrefix = Prefix(Name);
            var current = commonPrefix;

            for (var i = current.Depth; i < Name.Length; i++)
            {
                var newNode = new Node(current.Depth + 1, current, Name[i]);
                current.Children.Add(Name[i], newNode);
                current = newNode;
            }
            var endNode = new Node(current.Depth + 1, current, '$')
            {
                Result = Item
            };
            current.Children.Add('$', endNode);
        }

        /// <summary>
        /// Поиск элемента, строка которого является наибольшим префиксом данной строки
        /// </summary>
        /// <param name="s">Входная строка</param>
        /// <param name="depth">Полученная длина</param>
        /// <param name="Suggestion">Предположение</param>
        /// <returns>Найденный элемент (default если не найден)</returns>
        public T Search(string s, out int depth, out string Suggestion)
        {
            Suggestion = string.Empty;
            var prefix = Prefix(s);
            depth = prefix.Depth;
            var endChild = prefix.FindChildNode('$');
            if (endChild != null)
                return endChild.Result;
            else
            {
                if (prefix.Children.Count == 1)
                {
                    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(depth + 1, maxDepth);
                    var temp = prefix;
                    while (temp.Value != '$')
                    {
                        stringBuilder.Append(temp.Value);
                        temp = temp.Children.First().Value;
                        if (temp.Children.Count > 1)
                            return default(T);
                    }
                    temp = prefix.Parent;
                    while (temp.Depth > 0)
                    {
                        stringBuilder.Insert(0, temp.Value);
                        temp = temp.Parent;
                    }
                    temp = prefix;
                    
                    Suggestion = stringBuilder.ToString();
                }
                return default(T);
            }
        }
    }
}