namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция задания представления
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Presentation : SystemRule{
        /// <summary>
        /// Литерал конструкции задания представления
        /// </summary>
        public override string GetLiteral=> "::=";
    }
}