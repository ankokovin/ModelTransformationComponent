namespace ModelTransformationComponent
{

    /// <summary>
    /// Ссылка на системное правило
    /// <para>Наследует <see cref="BNFSimpleElement"/></para>
    /// </summary>
    [System.Serializable]
    public class BNFSystemRef : BNFSimpleElement
    {

        /// <summary>
        /// Системное правило, на которое ссылается данный оюъект
        /// </summary>
        public SystemRule rule;


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
            if (obj is BNFSystemRef s)
                return rule.Literal.Equals(s.rule.Literal);
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return rule.Literal;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemRule"></param>
        public BNFSystemRef(SystemRule systemRule)
        {
            rule = systemRule;
        }
    }
}