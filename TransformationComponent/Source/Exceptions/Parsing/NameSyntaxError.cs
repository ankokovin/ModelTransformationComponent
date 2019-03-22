using System;
using System.Runtime.Serialization;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Ошибка в названии структуры
    /// <para>
    /// Подвид синтаксической ошибки <see cref="ModelTransformationComponent.SyntaxError">
    /// </para>
    /// </summary>
    [Serializable]
    public class NameSyntaxError : SyntaxErrorPlaced
    {
        /// <summary>
        /// Строка формата сообщения ошибки по-умолчанию
        /// </summary>
        /// <value>
        /// "[{0},{1}] Синтаксическая ошибка: ожидалось валидное имя, а получили \"{2}\""
        /// </value>
        new protected static string FormatString = "[{0},{1}] Синтаксическая ошибка: ожидалось валидное имя, а получили \"{2}\"";
        
        /// <summary>
        /// Ошибка в названии структуры
        /// </summary>
        /// <param name="line">Номер строки</param>
        /// <param name="symbol">Номер символа</param>
        /// <param name="got">Полученная строка</param>
        /// <param name="formatString">Строка формата сообщения об ошибке</param>
        public NameSyntaxError(int line, int symbol, string got, string formatString) 
        : base(line,symbol,got,string.Empty,formatString.Replace("{2}","{3}"))
        {}

        /// <summary>
        /// Ошибка в названии структуры
        /// </summary>
        /// <param name="line">Номер строки</param>
        /// <param name="symbol">Номер символа</param>
        /// <param name="got">Полученная строка</param>
        public NameSyntaxError(int line, int symbol, string got) 
        : this(line, symbol, got,FormatString)
        {}

        /// <summary>
        /// Ошибка в названии структуры
        /// </summary>
        /// <param name="got">Полученная строка</param>
        public NameSyntaxError(string got) 
        : this(0,0,got)
        {}

        protected NameSyntaxError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NameSyntaxError(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NameSyntaxError()
        {
        }
    }
}