namespace ModelTransformationComponent
{
    /// <summary>
    /// Конструкция с названием
    /// <para/>
    /// Наследует <see cref="Rule"/>
    /// </summary>
    
    [System.Serializable]
    public abstract class NamedRule : Rule{
        /// <summary>
        /// Название конструкции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструкция с названием
        /// </summary>
        /// <param name="name">Название конструкции</param>
        public NamedRule(string name) : base(){
            Name = name;
        }
    }
}