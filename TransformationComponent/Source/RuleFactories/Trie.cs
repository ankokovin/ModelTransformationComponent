using System.Linq;
using System.Collections.Generic;

namespace ModelTransformationComponent
{
    public class Trie<T>
    {
        class Node
        {
            public char Value { get; set; }

            public T Result { get; set; }
            public List<Node> Children { get; set; }
            public Node Parent { get; set; }
            public int Depth { get; set; }

            public Node(char value, int depth, Node parent)
            {
                Value = value;
                Children = new List<Node>();
                Depth = depth;
                Parent = parent;
            }

            public bool IsLeaf()
            {
                return Children.Count == 0;
            }

            public Node FindChildNode(char c)
            {
                foreach (var child in Children)
                    if (child.Value == c)
                        return child;

                return null;
            }

            public void DeleteChildNode(char c)
            {
                for (var i = 0; i < Children.Count; i++)
                    if (Children[i].Value == c)
                        Children.RemoveAt(i);
            }
        }
        private readonly Node _root;

        public Trie()
        {
            _root = new Node('^', 0, null);
        }
        
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

        public void Insert(T Item, string Name)
        {
            var commonPrefix = Prefix(Name);
            var current = commonPrefix;

            for (var i = current.Depth; i < Name.Length; i++)
            {
                var newNode = new Node(Name[i], current.Depth + 1, current);
                current.Children.Add(newNode);
                current = newNode;
            }
            var endNode = new Node('$', current.Depth + 1, current)
            {
                Result = Item
            };
            current.Children.Add(endNode);
        }

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
                if (prefix.Children.Count == 0)
                {
                    var temp = prefix.Parent;
                    while (temp.Depth > 0)
                    {
                        Suggestion += temp.Value;
                        temp = temp.Parent;
                    }
                    char[] charArray = Suggestion.ToCharArray();
                    System.Array.Reverse(charArray);
                    Suggestion = new string(charArray);

                    temp = prefix;
                    while (prefix.Value != '$')
                    {
                        Suggestion += temp.Value;
                        temp = temp.Children.First();
                    }
                }
                return default(T);
            }
        }
    }
}