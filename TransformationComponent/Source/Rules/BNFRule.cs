using ModelTransformationComponent.SystemRules;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ModelTransformationComponent
{
    [System.Serializable]
    public class BNFRule : NamedRule, IList<BasicBNFRule>
    {
        public BNFRule(string name) : base(name)
        {
            OrSplits = new List<BasicBNFRule>();
        }

        protected readonly List<BasicBNFRule> OrSplits;

        public int Count => OrSplits.Count;

        public bool IsReadOnly => ((ICollection<BasicBNFRule>)OrSplits).IsReadOnly;



        public BasicBNFRule this[int index] { get => OrSplits[index]; set => OrSplits[index] = value; }

        public IEnumerator<BasicBNFRule> GetEnumerator()
        {
            return ((IEnumerable<BasicBNFRule>)OrSplits).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<BasicBNFRule>)OrSplits).GetEnumerator();
        }

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

        public override bool Equals(object obj)
        {
            return (obj is BNFRule r) && (!(obj is TypeRule)) && (Name.Equals(r.Name)) &&
                OrSplits.SequenceEqual(r.OrSplits);
        }

        public void Add(BasicBNFRule item)
        {
            OrSplits.Add(item);
        }

        public void Clear()
        {
            OrSplits.Clear();
        }

        public bool Contains(BasicBNFRule item)
        {
            return OrSplits.Contains(item);
        }

        public void CopyTo(BasicBNFRule[] array, int arrayIndex)
        {
            OrSplits.CopyTo(array, arrayIndex);
        }

        public bool Remove(BasicBNFRule item)
        {
            return OrSplits.Remove(item);
        }

        public int IndexOf(BasicBNFRule item)
        {
            return OrSplits.IndexOf(item);
        }

        public void Insert(int index, BasicBNFRule item)
        {
            OrSplits.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            OrSplits.RemoveAt(index);
        }
    }
}