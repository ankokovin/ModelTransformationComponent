using System.Collections;
using System.Collections.Generic;

namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BasicBNFRule : Rule, IList<BNFSimpleElement>
    {
        private readonly List<BNFSimpleElement> elements;

        public int Count => ((ICollection<BNFSimpleElement>)elements).Count;

        public bool IsReadOnly => ((ICollection<BNFSimpleElement>)elements).IsReadOnly;

        public BNFSimpleElement this[int index] { get => elements[index]; set => elements[index] = value; }

        public BasicBNFRule()
        {
            elements = new List<BNFSimpleElement>();
        }

        public override bool Equals(object obj)
        {
            if (obj is BasicBNFRule b)
            {
                if (elements.Count != b.elements.Count) return false;
                for (int i = 0; i < elements.Count; i++)
                {
                    if (!elements[i].Equals(b.elements[i]))
                        return false;
                }
                return true;
            }
            return false;
        }

        public IEnumerator<BNFSimpleElement> GetEnumerator()
        {
            return ((IEnumerable<BNFSimpleElement>)elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<BNFSimpleElement>)elements).GetEnumerator();
        }

        public override string ToString()
        {
            var result = string.Empty;

            foreach (var i in elements)
                result += i + " ";

            return result;
        }

        public void Add(BNFSimpleElement item)
        {
            ((ICollection<BNFSimpleElement>)elements).Add(item);
        }

        public void Clear()
        {
            ((ICollection<BNFSimpleElement>)elements).Clear();
        }

        public bool Contains(BNFSimpleElement item)
        {
            return ((ICollection<BNFSimpleElement>)elements).Contains(item);
        }

        public void CopyTo(BNFSimpleElement[] array, int arrayIndex)
        {
            ((ICollection<BNFSimpleElement>)elements).CopyTo(array, arrayIndex);
        }

        public bool Remove(BNFSimpleElement item)
        {
            return ((ICollection<BNFSimpleElement>)elements).Remove(item);
        }

        public int IndexOf(BNFSimpleElement item)
        {
            return elements.IndexOf(item);
        }

        public void Insert(int index, BNFSimpleElement item)
        {
            elements.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            elements.RemoveAt(index);
        }
    }
}