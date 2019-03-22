using System;
using System.Runtime.Serialization;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Синтаксическая ошибка с указанием номера строки и номера символа
    /// <para/>
    /// Наследует <see cref="SyntaxError"/>
    /// </summary>
    [Serializable]
    public class SyntaxErrorPlaced : SyntaxError
    {
        /// <summary>
        /// Строка формата расположения ошибки
        /// </summary>
        /// <value>
        /// "[{0},{1}]"
        /// </value>
        static protected string PlaceFormatString = "[{0},{1}]"; 

        /// <summary>
        /// Строка формата сообщения ошибки по-умолчанию
        /// </summary>
        /// <value>
        /// [{0},{1}] Синтаксическая ошибка: ожидалось \"{2}\", а получили \"{3}\"
        /// </value>
        static protected string FormatString = PlaceFormatString + " Синтаксическая ошибка: ожидалось \"{2}\", а получили \"{3}\"";
        /// <summary>
        /// Сообщение об ошибке без указания номера строки с символа
        /// </summary>
        public string TrimedMsg;

        /// <summary>
        /// Конструктор <see cref="SyntaxErrorPlaced"/>
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
        /// Конструктор <see cref="SyntaxErrorPlaced"/>
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
        /// Конструктор <see cref="SyntaxErrorPlaced"/>
        /// </summary>
        /// <param name="got">Полученная строка</param>
        /// <param name="expected">Ожидаемая строка</param>
        public SyntaxErrorPlaced(string got, string expected) : this(0, 0, got, expected) { }

        /// <summary>
        /// Конструктор <see cref="SyntaxErrorPlaced"/>
        /// </summary>
        public SyntaxErrorPlaced() { }

        /// <summary>
        /// Конструктор <see cref="SyntaxErrorPlaced"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public SyntaxErrorPlaced(string message) : base(message) { }

        /// <summary>
        /// Конструктор <see cref="SyntaxErrorPlaced"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        public SyntaxErrorPlaced(string message, Exception inner) : base(message, inner) { }


        /// <summary>
        /// Конструктор <see cref="SyntaxErrorPlaced"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        /// <param name="line"></param>
        /// <param name="symbol"></param>
        public SyntaxErrorPlaced(string message, Exception inner, int line, int symbol) 
            : this(message, inner)
        {
            Line = line;
            Symbol = symbol;
            TrimedMsg = message;
        }


        /// <summary>
        /// Конструктор <see cref="SyntaxErrorPlaced"/>
        /// </summary>
        /// <param name="inner">Внутренняя ошибка</param>
        /// <param name="line"></param>
        /// <param name="symbol"></param>
        public SyntaxErrorPlaced(int line, int symbol, Exception inner) : this(
            string.Format(PlaceFormatString + "Синтаксическая ошибка.", line, symbol), inner, line, symbol)
        {
            TrimedMsg = Message.Substring(Message.IndexOf(']') + 1);
        }

        /// <summary>
        /// Конструктор <see cref="SyntaxErrorPlaced"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SyntaxErrorPlaced(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}