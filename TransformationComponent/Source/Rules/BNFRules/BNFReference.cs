namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BNFReference : BNFSimpleElement
    {
        public string Name;

        public override bool Equals(object obj)
        {
            if (obj is BNFReference r)
                return this.Name.Equals(r.Name);
            return false;
        }
    }
}