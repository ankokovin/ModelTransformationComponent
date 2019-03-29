namespace ModelTransformationComponent
{

    /// <summary>
    /// Ссылка на правило
    /// <para/>
    /// Наследует <see cref="BNFSimpleElement"/>
    /// </summary>
    [System.Serializable]
    public class BNFReference : BNFSimpleElement
    {
        /// <summary>
        /// Название правила, на которое ссылаются
        /// </summary>
        public string Name;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public BNFReference(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            System.Diagnostics.Debug.WriteLine("Equals in BNFReference");
            System.Diagnostics.Debug.WriteLine("First:" + ToString());
            System.Diagnostics.Debug.WriteLine("Second:" + obj.ToString());
            if (obj is BNFReference r)
                return Name.Equals(r.Name);
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "<"+Name+">";
        }
    }
}