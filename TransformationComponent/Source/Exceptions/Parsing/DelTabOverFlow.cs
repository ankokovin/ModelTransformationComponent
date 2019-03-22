using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{

    /// <summary>
    /// Количество табов стало отрицательным
    /// <para/>
    /// Наслежует <see cref="SyntaxErrorPlaced"/>
    /// </summary>
    [Serializable]
    public class DelTabOverFlow : SyntaxErrorPlaced
    {
        /// <summary>
        /// Строка формата сообщения ошибки по-умолчанию
        /// </summary>
        /// <value>
        /// "[{0},{1}] Синтаксическая ошибка: количество табов стало отрицательным"
        /// </value>
        new protected static string FormatString = "[{0},{1}] Синтаксическая ошибка: количество табов стало отрицательным";

        /// <summary>
        /// Конструктор <see cref="DelTabOverFlow"/>
        /// </summary>
        public DelTabOverFlow() { }

        /// <summary>
        /// Конструктор <see cref="DelTabOverFlow"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public DelTabOverFlow(string message) : base(message) { }

        /// <summary>
        /// Конструктор <see cref="DelTabOverFlow"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        public DelTabOverFlow(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Конструктор <see cref="DelTabOverFlow"/>
        /// </summary>
        /// <param name="line"></param>
        /// <param name="symbol"></param>
        public DelTabOverFlow(int line, int symbol) 
            : base(line, symbol, string.Empty, string.Empty, FormatString)
        {
        }

        /// <summary>
        /// Конструктор <see cref="DelTabOverFlow"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DelTabOverFlow(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
