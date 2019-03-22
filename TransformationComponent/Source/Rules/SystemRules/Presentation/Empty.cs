namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция пустой символ
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Empty : SystemRule{

        /// <summary>
        /// Литерал конструкции пустой символ
        /// </summary>
        public override string GetLiteral => "/empty";
    }
}