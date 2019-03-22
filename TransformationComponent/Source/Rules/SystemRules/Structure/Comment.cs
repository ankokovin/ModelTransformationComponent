namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция однострочного комментария
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Comment : SystemRule
    {
        
        
        /// <summary>
        /// Литерал конструкции однострочного комментария
        /// </summary>
        public override string GetLiteral=> "//";
    }
}