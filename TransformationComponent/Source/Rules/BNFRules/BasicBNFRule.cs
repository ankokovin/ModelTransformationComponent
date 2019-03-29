using System.Collections;
using System.Collections.Generic;

namespace ModelTransformationComponent
{


    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class BasicBNFRule : Rule, IList<BNFSimpleElement>
    {
        
        private readonly List<BNFSimpleElement> elements;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Count => ((ICollection<BNFSimpleElement>)elements).Count;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsReadOnly => ((ICollection<BNFSimpleElement>)elements).IsReadOnly;


        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public BNFSimpleElement this[int index] { get => elements[index]; set => elements[index] = value; }


        /// <summary>
        /// 
        /// </summary>
        public BasicBNFRule()
        {
            elements = new List<BNFSimpleElement>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            System.Diagnostics.Debug.WriteLine("Equals in BasicBNFRule");
            System.Diagnostics.Debug.WriteLine("First:" + ToString());
            System.Diagnostics.Debug.WriteLine("Second:" + obj.ToString());
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<BNFSimpleElement> GetEnumerator()
        {
            return ((IEnumerable<BNFSimpleElement>)elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<BNFSimpleElement>)elements).GetEnumerator();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = string.Empty;

            foreach (var i in elements)
                result += i + " ";

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(BNFSimpleElement item)
        {
            ((ICollection<BNFSimpleElement>)elements).Add(item);
        }


        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            ((ICollection<BNFSimpleElement>)elements).Clear();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(BNFSimpleElement item)
        {
            return ((ICollection<BNFSimpleElement>)elements).Contains(item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(BNFSimpleElement[] array, int arrayIndex)
        {
            ((ICollection<BNFSimpleElement>)elements).CopyTo(array, arrayIndex);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(BNFSimpleElement item)
        {
            return ((ICollection<BNFSimpleElement>)elements).Remove(item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(BNFSimpleElement item)
        {
            return elements.IndexOf(item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, BNFSimpleElement item)
        {
            elements.Insert(index, item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            elements.RemoveAt(index);
        }
    }
}