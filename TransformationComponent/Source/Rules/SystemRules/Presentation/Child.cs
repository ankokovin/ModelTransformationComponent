namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция потомка. Используется в <see cref="Type"/>
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Child : SystemRule{
        
        /// <summary>
        /// Литерал конструкции потомка
        /// </summary>
        public override string GetLiteral => "/child";
    }
}