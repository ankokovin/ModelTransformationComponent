using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Текущее положение генерации текстового представления модели
    /// </summary>
    public class GeneratorState
    {
        private string text;
        private int tabCount;

        /// <summary>
        /// Счётчик табов
        /// </summary>
        public int TabCount
        {
            get => tabCount;
            set
            {
                if(value < 0)
                {
                    throw new DelTabOverFlow();
                }

                if (text.Length > 0 && text[text.Length - 1] == '\t')
                    if (tabCount < value)
                        text += new string('\t', value - tabCount);
                    else
                        text = text.Substring(0, text.Length - (tabCount - value));

                tabCount = value;
            }
        }

        /// <summary>
        /// Текущий текст
        /// </summary>
        public string Text
        {
            get => text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void AppendText(string input)
        {

            var sp = input.Split('\n');
            if ((sp.Length >= 1) && input.Length == 1 && input[0]=='\n')
            {
                text += '\n' + new string('\t', tabCount);
            }
            text += sp[0];
            foreach (var item in sp.Skip(1))
            {
                text += '\n' + new string('\t', tabCount) + item;
            }
        }
    }
}
