namespace ModelTransformationComponent
{
    /// <summary>
    /// Системная конструкция
    /// <para/>
    /// Наследует <see cref="Rule"/>
    /// </summary>
    abstract class SystemRule : Rule{
        abstract public string GetLiteral{get;}
    }
}