namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BasicBNFRule : Rule
    {
        public System.Collections.Generic.List<BNFSimpleElement> elements;

        public BasicBNFRule()
        {
            elements = new System.Collections.Generic.List<BNFSimpleElement>();
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
    }
}