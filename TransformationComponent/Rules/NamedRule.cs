namespace ModelTransformationComponent
{
    /// <summary>
    /// Конструкция с названием
    /// </summary>
    class NamedRule : Rule{
        /// <summary>
        /// Название конструкции
        /// </summary>
        private string Name;

        /// <summary>
        /// Название конструкции
        /// </summary>
        public string GetName {get=>Name;}

        /// <summary>
        /// Конструкция с названием
        /// </summary>
        /// <param name="name">Название конструкции</param>
        public NamedRule(string name) : base(){
            Name = name;
        }
    }
}