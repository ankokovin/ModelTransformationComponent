using System;
using System.Collections.Generic;
using System.Diagnostics;
using ModelTransformationComponent.SystemRules;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Компонент трасформации моделей
    /// </summary>
    public class TransformationComponent : ITransformationComponent
    {
        private readonly Dictionary<System.Type, Func<string, Rule, bool>> RuleTypePredicateList;

        /// <summary>
        /// Конструктор <see cref="TransformationComponent"/>
        /// </summary>
        public TransformationComponent()
        {
            RuleTypePredicateList = new Dictionary<System.Type, Func<string, Rule, bool>>
            {
                [typeof(RegRuleFactory)] =
                delegate (string s, Rule rule)
                {
                    return rule is Reg;
                },
                [typeof(SystemRuleFactory)] = 
                delegate (string s, Rule rule)
                {
                    return s.Length>0 && s[0] == '/' && !(rule is Reg) &&/*(!rule is BNF or rule is ended or system is fine)*/ true;
                },
                [typeof(BNFRuleFactory)] =
                delegate (string s, Rule rule)
                {
                    return s.Length > 0 && s[0] != '/' && !(rule is Reg) && !(rule is TypeDef);
                },
                [typeof(TypeRuleFactory)] =
                delegate (string s, Rule rule)
                {
                    return rule is TypeDef;
                }
            };

        }
        



        /// <summary>
        /// Функция трансформации моделей
        /// </summary>
        /// <param name="text">Текстовое представление исходной модели</param>
        /// <param name="rules">Текстовое представление правил трансформации</param>
        /// <param name="sourceLang">Название исходного языка</param>
        /// <param name="targetLang">Название целевого языка</param>
        /// <returns>Результат трансформации - новое текстовое представление</returns>
        /// <exception cref="NoLanguageRulesFound">
        /// Вызывается при отсутствии определения языка
        /// </exception>
        public string Transform(string text, string rules, string sourceLang, string targetLang){
            return Transform(text,TransformToRules(rules),sourceLang, targetLang);
        }

        /// <summary>
        /// Получение всех структур языков
        /// </summary>
        /// <param name="rules">Текстовое представление структур</param>
        /// <returns>Все структуры языков</returns>
        /// <exception cref="InputIsEmpty">
        /// Вызывается при отсутствии входного текста
        /// </exception>
        public AllRules TransformToRules(string rules){
            try
            {
                if (rules.Length == 0)
                    throw new InputIsEmpty();

                Debug.WriteLine("Inside GetAllRules");
                Debug.WriteLine("---------Rules--------");
                Debug.WriteLine(rules);
                Debug.WriteLine("----------------------");

                var result = new AllRules();

                var startIdx = rules.IndexOf(new Start().Literal);
                Debug.WriteLine("Index for /start: " + startIdx);

                if (startIdx == -1)
                    throw new NoStartDetected();


                var endIdx = rules.IndexOf(new End().Literal);
                Debug.WriteLine("Index for /end: " + endIdx);

                if (endIdx == -1)
                    throw new NoEndDetected();


                var text = rules.Substring(startIdx, endIdx - startIdx + new End().Literal.Length);

                Debug.WriteLine("---------Inside text----------");
                Debug.WriteLine(text);
                Debug.WriteLine("-----------------------");


                var firstlang = text.IndexOf(new Language_start().Literal);


                Debug.WriteLine("First idx for /language_start: " + firstlang);
                if (firstlang != -1)
                {
                    var baseLang = GetBaseDescription(text.Substring(0, firstlang));
                    result.AddBaseRules(baseLang);

                    var languages = text.Substring(firstlang).Split(
                        new[] { new Language_end().Literal }, StringSplitOptions.None);

                    Debug.WriteLine("Number of languages: " + languages.Length);
                    foreach (var lang in languages)
                    {
                        var res = GetLangDescription(lang, baseLang, out string langName);
                        result.AddLanguageRules(langName, res);
                        Debug.WriteLine("Got language: " + langName);
                    }
                }
                else
                {
                    Debug.WriteLine("No other languages");
                    var baseLang = GetBaseDescription(text);
                    result.AddBaseRules(baseLang);
                }
                return result;
            }catch(Exception e)
            {
                throw new RuleParseException("Ошибка при парсинге правил",e);
            }
        }


        private Dictionary<string, Rule> ParseRulesDescription(string text, 
            bool Base = true, 
            int startline = 0
            )
        {
            Debug.WriteLine("getting base description");
            Debug.WriteLine("text:");
            Debug.WriteLine(text);
            Rule prevRule = null;
            Dictionary<string, Rule> result = new Dictionary<string, Rule>();
            int idx = 0;
            int skipChars = 0;
            var lines = text.Split('\n');
            string typen = string.Empty;
            bool isTypeEx = false;
            bool paramStart = false;
            try
            {
                while (idx < lines.Length)
                {
                    if (string.IsNullOrWhiteSpace(lines[idx]))
                    {
                        ++idx;
                        break;
                    }

                    bool ok = false;

                    

                    if (prevRule is TypeEx)
                    {
                        HandleTypeExSetUp(in result, idx, ref lines, out typen, out isTypeEx);
                    }

                    foreach (var pred in RuleTypePredicateList)
                    {
                        if (pred.Value(lines[idx], prevRule))
                        {
                            Debug.WriteLine("Line " + idx + ": creaing factory:" + pred.Key);
                            Debug.WriteLine("Input line:\n" + lines[idx]);
                            var factory = (AbstractRuleFactory)Activator.CreateInstance(pred.Key);
                            var res = factory.CreateRule(lines[idx], out int charcnt);
                            Debug.WriteLine("Result:\n" + res);
                            if (charcnt != lines[idx].Length)
                            {
                                lines[idx] = lines[idx].Substring(charcnt+1);
                                skipChars += charcnt;
                            }
                            else
                            {
                                ++idx;
                                skipChars = 0;
                            }
                            if (!paramStart)
                            {
                                if (res is Params_start)
                                {
                                    if (prevRule is BNFRule || prevRule is TypeDef)
                                    {
                                        paramStart = true;
                                    }
                                    else
                                        throw new SyntaxError("Синтаксическая ошибка: получили /params_start после не БНФ или типовой структуры");
                                }
                                else
                                {
                                    prevRule = res;
                                    if (prevRule is NamedRule rule)
                                    {
                                        if (result.ContainsKey(rule.Name))
                                        {
                                            throw new ConstructAlreadyDefined();
                                        }
                                        result[rule.Name] = rule;
                                    }
                                }
                                
                            }
                            else
                            {
                                if (res is Params_end)
                                {
                                    paramStart = false;
                                    prevRule = null;

                                }
                                else if (res is BNFRule r)
                                {
                                    HandleParam(prevRule, ref result, r);
                                }
                                else
                                {
                                    throw new SyntaxError("Синтаксическая ошибка: получили не БНФ конструкцию внутри описания параметров");
                                }
                            }
                            if (isTypeEx)
                            {
                                HandleTypeEx(ref result, typen, res);
                                isTypeEx = false;
                            }
                            ok = true;
                            break;

                        }
                    }
                    if (!ok) throw new SyntaxError();

                }
                //throw new NotImplementedException();
                return result;
            }
            catch (SyntaxErrorPlaced e)
            {
                throw new BaseRuleParseException(e);
            }
            catch (Exception e)
            {
                throw new BaseRuleParseException(
                    new SyntaxErrorPlaced(startline + idx + 1, skipChars + 1, e)
                    );
            }

        }

        private static void HandleTypeExSetUp(in Dictionary<string, Rule> result, 
            int idx, 
            ref string[] lines, 
            out string typen, 
            out bool isTypeEx)
        {
            Debug.WriteLine("HandleTypeExSetUp called.");
            typen = lines[idx].Split()[0];
            Debug.WriteLine("Name of type: " + typen);
            if (!result.ContainsKey(typen))
            {
                throw new SyntaxError(
                    string.Format(
                        "Синтаксическая ошибка: " +
                        "Было встречено определение экземпляра типа {0}, " +
                        "который ещё не определён",
                        typen)
                        );
            }


            if (!(result[typen] is TypeRule))
            {
                throw new SyntaxError(
                    string.Format(
                        "Синтаксическая ошибка: " +
                        "{0} не является названием типа",
                        typen
                        )
                        );
            }
            Debug.WriteLine("Line was\n"+ lines[idx]);
            lines[idx] = lines[idx].Substring(typen.Length+1);
            Debug.WriteLine("Line now\n"+ lines[idx]);
            isTypeEx = true;

        }

        private static void HandleParam(Rule prevRule, ref Dictionary<string, Rule> result, BNFRule r)
        {
            Debug.WriteLine("HandleParam called");
            if (r.Count > 1)
            {
                throw new SyntaxError("Синтаксическая ошибка: оператор | во время описания параметра");
            }

            if (r.Count > 0 && r[0].Count > 1)
            {
                throw new SyntaxError("Синтаксическая ошибка: описание параметра может иметь только 1 элемент - ссылку");
            }

            if (r.Count > 0 && !(r[0][0] is BNFReference))
            {
                throw new SyntaxError("Синтаксическая ошибка: описание параметра может иметь только 1 элемент - ссылку");
            }

            string nName = ((NamedRule)prevRule).Name + "." + r.Name;
            if (result.ContainsKey(nName))
            {
                throw new ConstructAlreadyDefined();
            }
            Debug.WriteLine("Name was:" + r.Name);
            r.Name = nName;
            Debug.WriteLine("Name now:"+ r.Name);
            result[nName] = r;
        }

        private static void HandleTypeEx(ref Dictionary<string, Rule> result, string typen, Rule res)
        {
            Debug.WriteLine("HandleTypeEx called");
            TypeRule baseType = result[typen] as TypeRule;

            if (!(res is NamedRule nres))
                throw new TransformComponentException();

            BNFRule bnfR = res as BNFRule;

            if (bnfR == null) throw new TransformComponentException();

            BNFRule bNFRule = new BNFRule(bnfR.Name);
            foreach (BasicBNFRule basicBNFRule in bnfR)
            {
                BasicBNFRule nBasic = new BasicBNFRule();
                
                foreach (BNFSimpleElement element in baseType[0])
                {
                    bool checkStr = false;
                    if (element is BNFSystemRef sref && sref.rule is Child)
                    {
                        checkStr = true;
                        bool first = true;
                        foreach (var e in basicBNFRule)
                        {
                            if (first && e is BNFString newS && nBasic[nBasic.Count-1] is BNFString prev)
                            {
                                nBasic.RemoveAt(nBasic.Count - 1);
                                nBasic.Add(new BNFString() { Value = prev.Value + newS.Value });
                            }else
                                nBasic.Add(e);
                            first = false;
                        }

                    }
                    else
                    {
                        if (checkStr && element is BNFString str && nBasic.Count> 0 && nBasic[nBasic.Count-1] is BNFString str2)
                        {
                            checkStr = false;
                            nBasic.RemoveAt(nBasic.Count - 1);
                            nBasic.Add(new BNFString() { Value = str.Value + str2.Value });
                        }
                        else
                        nBasic.Add(element);
                    }
                }
                

                bNFRule.Add(nBasic);
            }
            baseType.RefList.Add(new BNFReference() { Name = bnfR.Name });
            result[bNFRule.Name] = bNFRule;
            Debug.WriteLine("Result:\n"+ bNFRule);
        }

        /// <summary>
        /// Получить базовые структуры 
        /// </summary>
        /// <param name="text">Текстовое описание базовых структур</param>
        /// <returns>Базовые структуры</returns>
        private Dictionary<string, Rule> GetBaseDescription(string text){
            return ParseRulesDescription(text);            
        }

        /// <summary>
        /// Получить структуры языка 
        /// </summary>
        /// <param name="text">Текстовое описание структур языка</param>
        /// <param name="baseDescription">Базовые структуры</param>
        /// <param name="LangName">Название языка</param>
        /// <returns>Структуры данного языка</returns>
        private Dictionary<string, Rule> GetLangDescription(string text, Dictionary<string,Rule> baseDescription, out string LangName){
            LangName = null;
            Debug.WriteLine("getting language description");
            Debug.WriteLine("text:");
            Debug.WriteLine(text);
            try
            {
                throw new NotImplementedException();
            }catch(Exception e)
            {
                if (LangName == null)
                    throw new LangRuleParseException(e);
                else
                    throw new LangRuleParseException(LangName, e);
            }
        }

        /// <summary>
        /// Получить текстовое представление модели в целевом языке
        /// </summary>
        /// <param name="text">Текстовое представление модели в исходном языке</param>
        /// <param name="rules">Все правила языков</param>
        /// <param name="sourceLang">Название исходного языка</param>
        /// <param name="targetLang">Название целевого языка</param>
        /// <returns>Текстовое представление модели в целевом языке</returns>
        /// <exception cref="NoLanguageRulesFound">
        /// Вызывается при отсутствии определения языка
        /// </exception>
        /// <exception cref="InputIsEmpty">
        /// Вызывается при отсутствии входного текста
        /// </exception>
        public string Transform(string text, AllRules rules, string sourceLang, string targetLang){
            try
            {
                if (text.Length == 0)
                    throw new InputIsEmpty();
                Debug.WriteLine("text:");
                Debug.WriteLine(text);
                Debug.WriteLineIf(rules == null, "rules was null");
                Debug.WriteLine("sourceLang:" + sourceLang);
                Debug.WriteLine("targetLang:" + targetLang);
                try { var t = rules.GetBaseRules; }
                catch(Exception e)
                {
                    throw new NoBaseRulesFound(e);
                }
                if (!rules.HasLanguage(sourceLang)) throw new NoLanguageRulesFound(sourceLang);
                if (!rules.HasLanguage(targetLang)) throw new NoLanguageRulesFound(targetLang);
                throw new NotImplementedException();
            }catch(Exception e)
            {
                throw new ModelParseException("Ошибка при парсинге текстовой модели", e);
            }
        }
    }
}
