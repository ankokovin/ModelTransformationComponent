namespace ModelTransformationComponent
{

    /// <summary>
    /// Строка в БНФ
    /// <para/>
    /// Наследует <see cref="BNFSimpleElement"/>
    /// </summary>
    [System.Serializable]
    public class BNFString : BNFSimpleElement
    {
        /// <summary>
        /// Строковое значение
        /// </summary>
        public string Value;


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
            if (obj is BNFString s)
                return Value.Equals(s.Value);
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public BNFString(string val)
        {
            Value = val;
        }
    }
}