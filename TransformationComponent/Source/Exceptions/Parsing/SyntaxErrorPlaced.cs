using System;
using System.Runtime.Serialization;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Синтаксическая ошибка с указанием номера строки и номера символа
    /// </summary>
    [Serializable]
    public class SyntaxErrorPlaced : SyntaxError
    {


        /// <summary>
        /// Строка формата сообщения ошибки по-умолчанию
        /// </summary>
        /// <value>
        /// [{0},{1}] Синтаксическая ошибка: ожидалось \"{2}\", а получили \"{3}\"
        /// </value>
        static protected string FormatString = "[{0},{1}] Синтаксическая ошибка: ожидалось \"{2}\", а получили \"{3}\"";

        /// <summary>
        /// Сообщение об ошибке без указания номера строки с символа
        /// </summary>
        public string TrimedMsg;

        /// <summary>
        /// Синтаксическая ошибка
        /// </summary>
        /// <param name="line">Номер строки</param>
        /// <param name="symbol">Номер символа</param>
        /// <param name="got">Полученная строка</param>
        /// <param name="expected">Ожидаемая строка</param>
        /// <param name="formatString">Строка формата сообщения ошибки</param>
        public SyntaxErrorPlaced(int line, int symbol, string got, string expected, string formatString)
        : base(string.Format(formatString, line, symbol, expected, got))
        {
            Line = line;
            Symbol = symbol;
            Got = got;
            Expected = expected;
            TrimedMsg = Message.Substring(Message.IndexOf(']') + 1);
        }

        /// <summary>
        /// Синтаксическая ошибка
        /// </summary>
        /// <param name="line">Номер строки</param>
        /// <param name="symbol">Номер символа</param>
        /// <param name="got">Полученная строка</param>
        /// <param name="expected">Ожидаемая строка</param>
        public SyntaxErrorPlaced(int line, int symbol, string got, string expected) :
        this(line, symbol, got, expected, FormatString)
        { }

        /// <summary>
        /// Номер строки
        /// </summary>
        public int Line;

        /// <summary>
        /// Номер символа
        /// </summary>
        public int Symbol;


        /// <summary>
        /// Синтаксическая ошибка
        /// </summary>
        /// <param name="got">Полученная строка</param>
        /// <param name="expected">Ожидаемая строка</param>
        public SyntaxErrorPlaced(string got, string expected) : this(0, 0, got, expected) { }
        public SyntaxErrorPlaced() { }
        public SyntaxErrorPlaced(string message) : base(message) { }
        public SyntaxErrorPlaced(string message, Exception inner) : base(message, inner) { }
        protected SyntaxErrorPlaced(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}