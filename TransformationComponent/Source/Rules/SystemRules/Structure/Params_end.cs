namespace ModelTransformationComponent{
    /// <summary>
    /// Системная конструкция окончания описания параметров
    /// <para/>
    /// Наследует <see cref="SystemRule"/>
    /// </summary>
    class Params_end : SystemRule{
        /// <summary>
        /// Литерал конструкции окончания описания параметров
        /// </summary>
        public override string GetLiteral => "/params_end"; 
    }
}