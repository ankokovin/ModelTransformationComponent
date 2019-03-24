using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Текущее положение генерации текстового представления модели
    /// </summary>
    class GeneratorState
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
                        text = text.Substring(0, text.Length - (value - tabCount));

                tabCount = value;
            }
        }

        /// <summary>
        /// Текущий текст
        /// </summary>
        public string Text
        {
            get => text;
            set
            {
                var str = value;
                int idx = 0;
                while(idx < str.Length)
                {
                    var nidx = str.IndexOf('\n');
                    if (nidx == -1)
                    {
                        text += str;
                    }
                    else
                    {
                        text += str.Substring(0, nidx);
                    }
                }
            }
        }
    }
}
