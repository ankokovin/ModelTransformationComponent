﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Текущее положение парсера
    /// </summary>
    public class ParserState
    {
        private int tabCount;
        public int TabCount
        {
            get => tabCount;
            set
            {
                if (value < 0)
                {
                    throw new DelTabOverFlow();
                }

                tabCount = value;
            }
        }
    }
}
