using System.Collections;
using System.Collections.Generic;

namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BasicBNFRule : Rule, IEnumerable<BNFSimpleElement>
    {
        public List<BNFSimpleElement> elements;

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
    }
}