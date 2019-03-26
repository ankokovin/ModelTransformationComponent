namespace ModelTransformationComponent
{
    class BasicBNFFactory : AbstractRuleFactory
    {

        public const char escapeChar = '~';

        public override Rule CreateRule(string text, out int charcnt)
        {
            string tempStr = string.Empty;
            int idx = 0;

            var sysRuleFact = new SystemRuleFactory();
            BasicBNFRule basicBNFRule = new BasicBNFRule();

            bool sys = false;
            bool refr = false;
            bool lit = false;

            while (idx < text.Length)
            {
                if (text[idx] == escapeChar)
                {
                    if (lit)
                    {
                        tempStr += escapeChar;
                        lit = false;
                        ++idx;
                        continue;
                    }
                    if (sys || refr)
                        throw new SyntaxError("Синтаксическая ошибка: escape символ внутри описания "+
                            (sys? "системного символа" : "ссылки"));
                    lit = true;
                    ++idx;
                    continue;
                }

                if (lit)
                {
                    tempStr += text[idx];
                    ++idx;
                    lit = false;
                    continue;
                }

                if (sys)
                {
                    if (text[idx] != ' ')
                    {
                        tempStr += text[idx];
                    }
                    else
                    {
                        sys = false;
                        var sysRule = sysRuleFact.CreateRule(tempStr, out int c);
                        var sysRuleRef = new BNFSystemRef((SystemRule)sysRule);

                        basicBNFRule.Add(sysRuleRef);
                        tempStr = string.Empty;
                    }
                    ++idx;
                    continue;
                }


                if (text[idx] == '/')
                {
                    sys = true;
                    if (!string.IsNullOrEmpty(tempStr))
                    {
                        var strRule = new BNFString(tempStr);
                        tempStr = string.Empty;
                        basicBNFRule.Add(strRule);
                    }
                    tempStr += text[idx];
                    ++idx;
                    continue;
                }

                if (text[idx] == '<')
                {
                    if (refr)
                        throw new SyntaxError("Синтаксическая ошибка: использование символа '<' внутри описания ссылки");
                    refr = true;
                    if (!string.IsNullOrEmpty(tempStr))
                    {
                        var strRule = new BNFString(tempStr);
                        tempStr = string.Empty;
                        basicBNFRule.Add(strRule);
                    }
                    ++idx;
                    continue;
                }

                if (text[idx] == '>')
                {
                    if (!refr && !lit)
                        throw new SyntaxErrorPlaced();

                    if (string.IsNullOrEmpty(tempStr))
                    {
                        throw new SyntaxErrorPlaced();
                    }
                    var refRule = new BNFReference(tempStr);
                    basicBNFRule.Add(refRule);
                    tempStr = string.Empty;
                    refr = false;
                    ++idx;
                    continue;
                }

                if (text[idx] == ' ')
                {
                    ++idx;
                    continue;
                }

                tempStr += text[idx];
                ++idx;
            }
            if (tempStr.Length > 0)
            {
                BNFSimpleElement lastRule;

                if (refr)
                {
                    throw new SyntaxError("Синтаксическая ошибка: ожидалось >");
                }

                if (!sys)
                {
                    lastRule = new BNFString( tempStr);
                   
                }
                else
                {
                    var sysRule = (SystemRule)sysRuleFact.CreateRule(tempStr, out int c);
                    lastRule = new BNFSystemRef(sysRule);
                }
                basicBNFRule.Add(lastRule);

            }
            charcnt = text.Length;
            return basicBNFRule;
        }
    }
}