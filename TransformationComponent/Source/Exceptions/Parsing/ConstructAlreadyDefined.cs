using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{

    /// <summary>
    /// Конструкция с данным именем уже существует
    /// <para/>
    /// Наследует <see cref="SyntaxError"/>
    /// </summary>
    [Serializable]
    public class ConstructAlreadyDefined : SyntaxError
    {
       
        /// <summary>
        /// 
        /// </summary>
        public ConstructAlreadyDefined() : base("Синтаксическая ошибка: конструкция с данным именем уже существует")
        {
        }
    }
}