using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            AllRules allRules = TransformToRules(rules, true);
            
            return Transform(text,allRules,sourceLang, targetLang);
        }

        /// <summary>
        /// Получение всех структур языков
        /// </summary>
        /// <param name="rules">Текстовое представление структур</param>
        /// <param name="Minimize"></param>
        /// <returns>Все структуры языков</returns>
        /// <exception cref="InputIsEmpty">
        /// Вызывается при отсутствии входного текста
        /// </exception>
        public AllRules TransformToRules(string rules, bool Minimize = false){
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
                    var baseLang = GetBaseDescription(text.Substring(0, firstlang), out int line);
                    result.AddBaseRules(baseLang);

                    var languages = (new System.Text.RegularExpressions.Regex("/language_start(.|[\n])*?/language_end")).Matches(text)
                        .Cast<System.Text.RegularExpressions.Match>()
                        .Select(m => m.Value)
                        .ToArray(); ;

                    Debug.WriteLine("Number of languages: " + languages.Length);
                    foreach (var lang in languages)
                    {
                        string langName = string.Empty;

                        var s = lang.Split('\n')[0].Split().Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        if (s.Length != 2)
                            throw new SyntaxErrorPlaced(line,0,null);
                        langName = s[1];


                        var res = GetLangDescription(lang.Substring(lang.IndexOf('\n')+1), baseLang, langName, line, out int linec);
                        line += linec;
                        result.AddLanguageRules(langName, res);
                        Debug.WriteLine("Got language: " + langName);
                    }
                }
                else
                {
                    Debug.WriteLine("No other languages");
                    var baseLang = GetBaseDescription(text, out int line_end);
                    result.AddBaseRules(baseLang);
                }


                if (System.Diagnostics.Debugger.IsAttached)
                {
                    Debug.WriteLine("TranformToRules result:");
                    Debug.WriteLine("-------------------Base------------------");
                    foreach (var item in result.GetBaseRules)
                    {
                        Debug.WriteLine(item.Value);
                    }
                    foreach (var item in result.Languages)
                    {
                        Debug.WriteLine("---------------------------");
                        Debug.WriteLine("------------------"+item+"---------------------------");
                        foreach (var item1 in result.GetRulesForLanguage(item))
                        {
                            Debug.WriteLine(item1);
                        }
                    }
                }

                if (Minimize)
                {
                    
                    result = BackTrackTypes(result);

                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        Debug.WriteLine("TranformToRules result:");
                        Debug.WriteLine("-------------------Base------------------");
                        foreach (var item in result.GetBaseRules)
                        {
                            Debug.WriteLine(item.Value);
                        }
                        foreach (var item in result.Languages)
                        {
                            Debug.WriteLine("---------------------------");
                            Debug.WriteLine("------------------" + item + "---------------------------");
                            foreach (var item1 in result.GetRulesForLanguage(item))
                            {
                                Debug.WriteLine(item1);
                            }
                        }
                    }
                }
                return result;
            }catch(Exception e)
            {
                throw new RuleParseException("Ошибка при парсинге правил",e);
            }
        }

        private AllRules BackTrackTypes(in AllRules allRules)
        {
            AllRules res = new AllRules();
            var baseR = allRules.GetBaseRules;
            Dictionary<string, Rule> temp = new Dictionary<string, Rule>();
            foreach (var item in baseR)
            {
                if (item.Value is TypeRule t)
                {
                    BNFRule bNF = new BNFRule(item.Key);
                    foreach (var refr in t.RefList)
                    {
                        bNF.Add(new BasicBNFRule { refr });
                    }
                    temp[item.Key] = bNF;
                }
                else
                {
                    temp[item.Key] = item.Value;
                }
            }
            res.AddBaseRules(temp);
            foreach (var lang in allRules.Languages)
            {
                temp = new Dictionary<string, Rule>();
                var langR = allRules.GetRulesForLanguage(lang);
                foreach (var item in langR)
                {
                    if (item.Value is TypeRule t)
                    {
                        BNFRule bNF = new BNFRule(item.Key);
                        foreach (var refr in t.RefList)
                        {
                            bNF.Add(new BasicBNFRule { refr });
                        }
                        temp[item.Key] = bNF;
                    }
                    else
                    {
                        temp[item.Key] = item.Value;
                    }
                }
                res.AddLanguageRules(lang, temp);
            }

            return res;
        }

        private Dictionary<string, Rule> ParseRulesDescription(string text, 
            out int line_end,
            bool Base = true, 
            int startline = 0,
            Dictionary<string, Rule> baseDescription = null
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
            line_end = lines.Length;
            string typen = string.Empty;
            bool isTypeEx = false;
            bool paramStart = false;
            bool TranslateRulesStarted = false;
            try
            {
                while (idx < lines.Length)
                {
                    if (string.IsNullOrWhiteSpace(lines[idx]))
                    {
                        ++idx;
                        continue;
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
                            
                            if (res is Comment)
                            {
                                ++idx;
                                ok = true;
                                break;
                            }

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
                            if (res is Translate_rules_start)
                            {
                                if (isTypeEx || paramStart || prevRule is TypeDef || prevRule is Reg)
                                    throw new SyntaxError("Синтаксическая ошибка: неожиданный символ /translate_rules_start");

                                TranslateRulesStarted = true;
                                break;
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
                            if (isTypeEx && !(res is TypeDef))
                            {
                                HandleTypeEx(ref result, typen, res);
                                isTypeEx = false;
                            }
                            ok = true;
                            break;

                        }
                    }
                    if (TranslateRulesStarted) break;
                    if (!ok) throw new SyntaxError();

                }
                if (TranslateRulesStarted)
                {
                    
                    prevRule = null;
                    bool translate_rules_end = false;
                    while (idx < lines.Length)
                    {
                        if (string.IsNullOrWhiteSpace(lines[idx]))
                        {
                            ++idx;
                            continue;

                        }

                        foreach (var pred in RuleTypePredicateList)
                        {
                            if (pred.Value(lines[idx], prevRule))
                            {
                                if (pred.Key != typeof(BNFRuleFactory) && pred.Key != typeof(SystemRuleFactory))
                                {
                                    throw new SyntaxError("Синтаксическая ошибка: ожидалась БНФ конструкция");
                                }

                                var res = ((AbstractRuleFactory)System.Activator.CreateInstance(pred.Key)).CreateRule(lines[idx], out int charcnt);

                                Debug.WriteLine("Result:\n" + res);

                                if (res is Comment)
                                {
                                    ++idx;
                                    break;
                                }

                                if (charcnt != lines[idx].Length)
                                {
                                    lines[idx] = lines[idx].Substring(charcnt + 1);
                                    skipChars += charcnt;
                                }
                                else
                                {
                                    ++idx;
                                    skipChars = 0;
                                }

                                if (res is BNFRule b)
                                {
                                    if (result.ContainsKey(b.Name))
                                    {
                                        b.Name = "T+" + b.Name;
                                        result.Add(b.Name, b);
                                        break;
                                    }
                                    else
                                    {
                                        throw new SyntaxError("Синтаксическая ошибка: Не может быть определено новое правило внутри /translate_rules");
                                    }
                                }
                                if (res is Translate_rules_end)
                                {
                                    translate_rules_end = true;
                                    break;
                                }
                            }
                        }
                        if (translate_rules_end)
                            break;
                    }

                    if (idx == lines.Length - 1)
                    {
                        if (RuleTypePredicateList[typeof(SystemRuleFactory)](lines[idx], prevRule)
                            && (new SystemRuleFactory()).CreateRule(lines[idx], out int charcnt) is End
                            && (charcnt == lines[idx].Length || string.IsNullOrWhiteSpace(lines[idx].Substring(charcnt+1))))
                            {

                        }
                        else
                        {
                            throw new SyntaxError("Синтаксическая ошибка: неожиданный символ:" + lines[idx]);
                        }
                    }

                }

                if (!Base)
                {
                    foreach (Rule r in baseDescription.Values)
                    {
                        if (r is BNFRule bnf && bnf.Count == 0 && !result.ContainsKey(bnf.Name))
                            throw new EOSException("Неожиданный конец описания языка. Ожидался символ " + bnf.Name);

                    }
                }
                    
                
                return result;
            }
            catch (Exception e)
            {
                throw new SyntaxErrorPlaced(startline + idx + 1, skipChars + 1, e);
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
            var bnfRule = (BNFRule)prevRule;

            foreach(BasicBNFRule basicr in bnfRule)
            {
                foreach (BNFSimpleElement item in basicr)
                {
                    if (item is BNFReference refr &&  refr.Name == r.Name)
                        refr.Name = nName;
                }
            }
            

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
            BNFRule bNFRule;
            if (res is TypeRule)
                bNFRule = new TypeRule(bnfR.Name);
            else
                bNFRule = new BNFRule(bnfR.Name);
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
                                nBasic.Add(new BNFString( prev.Value + newS.Value ));
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
                            nBasic.Add(new BNFString(str.Value + str2.Value));
                        }
                        else
                        nBasic.Add(element);
                    }
                }
                

                bNFRule.Add(nBasic);
            }
            baseType.RefList.Add(new BNFReference(bnfR.Name ));
            result[bNFRule.Name] = bNFRule;
            Debug.WriteLine("Result:\n"+ bNFRule);
        }

        /// <summary>
        /// Получить базовые структуры 
        /// </summary>
        /// <param name="text">Текстовое описание базовых структур</param>
        /// <param name="line_end">Номер последней строки</param>
        /// <returns>Базовые структуры</returns>
        private Dictionary<string, Rule> GetBaseDescription(string text, out int line_end){
            try
            {

                return ParseRulesDescription(text, out line_end);            
            }catch(Exception e)
            {
                throw new BaseRuleParseException(e); 
            }
        }

        /// <summary>
        /// Получить структуры языка 
        /// </summary>
        /// <param name="text">Текстовое описание структур языка</param>
        /// <param name="baseDescription">Базовые структуры</param>
        /// <param name="LangName">Название языка</param>
        /// <param name="line"></param>
        /// <param name="line_end"></param>
        /// <returns>Структуры данного языка</returns>
        private Dictionary<string, Rule> GetLangDescription(string text, Dictionary<string,Rule> baseDescription, string LangName, int line, out int line_end){
            Debug.WriteLine("getting language description");
            Debug.WriteLine("text:");
            Debug.WriteLine(text);
            try
            {
                return ParseRulesDescription(text,out line_end, false, line, baseDescription);
            }
            catch(Exception e)
            {
                
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
                var a = rules.GetBaseRules;
                if (!rules.HasLanguage(sourceLang)) throw new NoLanguageRulesFound(sourceLang);
                if (!rules.HasLanguage(targetLang)) throw new NoLanguageRulesFound(targetLang);

                DependencyGraph dependencyGraph = new DependencyGraph(in rules, sourceLang, targetLang);
                return dependencyGraph.TransformText(text);
                
            }
            catch (Exception e)
            {
                throw new ModelParseException("Ошибка при парсинге текстовой модели", e);
            }
        }
    }
}
