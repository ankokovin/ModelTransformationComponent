using ModelTransformationComponent.SystemRules;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ModelTransformationComponent
{

    /// <summary>
    /// БНФ правило
    /// </summary>
    [System.Serializable]
    public class BNFRule : NamedRule, IList<BasicBNFRule>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BNFRule(string name) : base(name)
        {
            OrSplits = new List<BasicBNFRule>();
        }


        /// <summary>
        /// 
        /// </summary>
        protected readonly List<BasicBNFRule> OrSplits;


        /// <summary>
        /// 
        /// </summary>
        public int Count => OrSplits.Count;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsReadOnly => ((ICollection<BasicBNFRule>)OrSplits).IsReadOnly;


        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public BasicBNFRule this[int index] { get => OrSplits[index]; set => OrSplits[index] = value; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<BasicBNFRule> GetEnumerator()
        {
            return ((IEnumerable<BasicBNFRule>)OrSplits).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() 
        {
            return ((IEnumerable<BasicBNFRule>)OrSplits).GetEnumerator();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = "BNFRule " + Name + "\n";
            bool first = true;
            foreach (var i in OrSplits)
                if (first)
                {
                    first = false;
                    result += i.ToString();
                }
                else
                    result += " | " + i.ToString();
            return result;
        }


        //TO DO: could be a bit optimized
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public bool SelfReferenced
        {
            get
            {
                foreach (var item in this)
                {
                    foreach (var item2 in item)
                    {
                        if (item2 is BNFReference refr && refr.Name == Name)
                            return true;
                    }
                }
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is BNFRule r) && (!(obj is TypeRule)) && (Name.Equals(r.Name)) &&
                OrSplits.SequenceEqual(r.OrSplits);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(BasicBNFRule item)
        {
            OrSplits.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            OrSplits.Clear();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(BasicBNFRule item)
        {
            return OrSplits.Contains(item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(BasicBNFRule[] array, int arrayIndex)
        {
            OrSplits.CopyTo(array, arrayIndex);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(BasicBNFRule item)
        {
            return OrSplits.Remove(item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(BasicBNFRule item)
        {
            return OrSplits.IndexOf(item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, BasicBNFRule item)
        {
            OrSplits.Insert(index, item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            OrSplits.RemoveAt(index);
        }
    }
}