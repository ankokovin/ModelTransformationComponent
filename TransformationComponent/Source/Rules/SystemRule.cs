namespace ModelTransformationComponent
{
    /// <summary>
    /// ��������� �����������
    /// <para/>
    /// ��������� <see cref="Rule"/>
    /// </summary>
    
    [System.Serializable]
    public abstract class SystemRule : Rule{
        abstract public string GetLiteral{get;}
    }
}