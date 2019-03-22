namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция создания конструкции типа
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Type : SystemRule{
        /// <summary>
        /// Литерал конструкции создания конструкции типа
        /// </summary>
        public override string GetLiteral => "/type";
    }
}