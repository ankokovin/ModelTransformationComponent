namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкиция пробел
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Space : SystemRule{

        /// <summary>
        /// Литерал конструкции пробел
        /// </summary>
        public override string GetLiteral => "/space";
    }
}