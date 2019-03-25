namespace ModelTransformationComponent
{

    [System.Serializable]
    public class BNFReference : BNFSimpleElement
    {
        public string Name;

        public override bool Equals(object obj)
        {
            System.Diagnostics.Debug.WriteLine("Equals in BNFReference");
            System.Diagnostics.Debug.WriteLine("First:" + ToString());
            System.Diagnostics.Debug.WriteLine("Second:" + obj.ToString());
            if (obj is BNFReference r)
                return Name.Equals(r.Name);
            return false;
        }

        public override string ToString()
        {
            return "<"+Name+">";
        }
    }
}