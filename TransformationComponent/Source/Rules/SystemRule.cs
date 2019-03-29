namespace ModelTransformationComponent
{
    /// <summary>
    /// Cистемная конструкция
    /// <para/>
    /// Наследует <see cref="Rule"/>
    /// </summary>
    
    [System.Serializable]
    public abstract class SystemRule : Rule{

        /// <summary>
        /// Литерал конструкции
        /// </summary>
        abstract public string Literal{get;}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "SystemRule: "+Literal;
        }
    }
}