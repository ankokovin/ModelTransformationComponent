using System;
using System.Runtime.Serialization;

namespace ModelTransformationComponent{

    /// <summary>
    /// Синтаксическая ошибка
    /// </summary>
    [Serializable]
    class SyntaxError : System.Exception{

        /// <summary>
        /// Строка формата сообщения ошибки по-умолчанию
        /// </summary>
        /// <value>
        /// [{0},{1}] Синтаксическая ошибка: ожидалось \"{2}\", а получили \"{3}\"
        /// </value>
        static protected string FormatString = "[{0},{1}] Синтаксическая ошибка: ожидалось \"{2}\", а получили \"{3}\""; 
        
        /// <summary>
        /// Синтаксическая ошибка
        /// </summary>
        /// <param name="line">Номер строки</param>
        /// <param name="symbol">Номер символа</param>
        /// <param name="got">Полученная строка</param>
        /// <param name="expected">Ожидаемая строка</param>
        /// <param name="formatString">Строка формата сообщения ошибки</param>
        public SyntaxError(int line, int symbol, string got, string expected, string formatString)
        :base(string.Format(formatString,line,symbol,expected,got)){
            Line = line;
            Symbol = symbol;
            Got = got;
            Expected = expected;
        }

        /// <summary>
        /// Синтаксическая ошибка
        /// </summary>
        /// <param name="line">Номер строки</param>
        /// <param name="symbol">Номер символа</param>
        /// <param name="got">Полученная строка</param>
        /// <param name="expected">Ожидаемая строка</param>
        public SyntaxError(int line, int symbol, string got, string expected): 
        this(line, symbol, got, expected, FormatString){}

        public int Line;
        public int Symbol;

        public string Got;

        public string Expected;

        /// <summary>
        /// Синтаксическая ошибка
        /// </summary>
        /// <param name="got">Полученная строка</param>
        /// <param name="expected">Ожидаемая строка</param>
        public SyntaxError(string got, string expected): this(0,0,got,expected){}

        protected SyntaxError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SyntaxError(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SyntaxError()
        {
        }

        public SyntaxError(string message) : base(message)
        {
        }
    }
    /// <summary>
    /// Ошибка в названии структуры
    /// <para>
    /// Подвид синтаксической ошибки <see cref="ModelTransformationComponent.SyntaxError">
    /// </para>
    /// </summary>
    [Serializable]
    class NameSyntaxError : SyntaxError
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