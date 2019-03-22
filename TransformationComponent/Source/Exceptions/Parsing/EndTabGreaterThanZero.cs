using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{

    /// <summary>
    /// Количество табов по окончании выше нуля
    /// <para/>
    /// Наслежует <see cref="EOSException"/>
    /// </summary>
    [Serializable]
    public class EndTabGreaterThanZero : EOSException
    {
        /// <summary>
        /// Конструктор <see cref="EndTabGreaterThanZero"/>
        /// </summary>
        public EndTabGreaterThanZero(): base(@"Количество символов /add_tab больше количества символов /del_tab")  { }

        /// <summary>
        /// Конструктор <see cref="EndTabGreaterThanZero"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        public EndTabGreaterThanZero(string message) : base(message) { }

        /// <summary>
        /// Конструктор <see cref="EndTabGreaterThanZero"/>
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутренняя ошибка</param>
        public EndTabGreaterThanZero(string message, Exception inner) : base(message, inner) { }


        /// <summary>
        /// Конструктор <see cref="EndTabGreaterThanZero"/>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected EndTabGreaterThanZero(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
