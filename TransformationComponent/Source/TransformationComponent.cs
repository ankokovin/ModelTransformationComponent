using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ModelTransformationComponent
{
    /// <summary>
    /// Компонент трасформации моделей
    /// </summary>
    public class TransformationComponent : ITransformationComponent
    {
        /// <summary>
        /// Функция трансформации моделей
        /// </summary>
        /// <param name="text">Текстовое представление исходной модели</param>
        /// <param name="rules">Текстовое представление правил трансформации</param>
        /// <param name="sourceLang">Название исходного языка</param>
        /// <param name="targetLang">Название целевого языка</param>
        /// <returns>Результат трансформации - новое текстовое представление</returns>
        /// <exception cref="TransformationComponent.NoLanguageRulesFound">
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
        public AllRules TransformToRules(string rules){
            Debug.WriteLine("Inside GetAllRules");
            Debug.WriteLine("---------Rules--------");
            Debug.WriteLine(rules);
            Debug.WriteLine("----------------------");
            var result = new AllRules();
            var startIdx = rules.IndexOf(new Start().GetLiteral);
            Debug.WriteLine("Index for /start: "+startIdx);
            var endIdx = rules.IndexOf(new End().GetLiteral);
            Debug.WriteLine("Index for /end: "+endIdx);
            var text = rules.Substring(startIdx,endIdx-startIdx+new End().GetLiteral.Length);
            Debug.WriteLine("---------Inside text----------");
            Debug.WriteLine(text);
            Debug.WriteLine("-----------------------");
            var firstlang = text.IndexOf(new Language_start().GetLiteral);
            Debug.WriteLine("First idx for /language_start: "+firstlang);
            var baseLang = GetBaseDescription(text.Substring(0,firstlang));
            result.AddBaseRules(baseLang);

            var languages = text.Substring(firstlang).Split(
                new [] { new Language_end().GetLiteral }, StringSplitOptions.None);
            
            Debug.WriteLine("Number of languages: "+languages.Length);
            foreach (var lang in languages){
                var res = GetLangDescription(lang,baseLang,out string langName);
                result.AddLanguageRules(langName, res);
                Debug.WriteLine("Got language: "+langName);
            }
            return result; 
        }

        /// <summary>
        /// Получить базовые структуры 
        /// </summary>
        /// <param name="text">Текстовое описание базовых структур</param>
        /// <returns>Базовые структуры</returns>
        private Dictionary<string, Rule> GetBaseDescription(string text){
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить структуры языка 
        /// </summary>
        /// <param name="text">Текстовое описание структур языка</param>
        /// <param name="baseDescription">Базовые структуры</param>
        /// <param name="LangName">Название языка</param>
        /// <returns>Структуры данного языка</returns>
        private Dictionary<string, Rule> GetLangDescription(string text, Dictionary<string,Rule> baseDescription, out string LangName){
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить текстовое представление модели в целевом языке
        /// </summary>
        /// <param name="text">Текстовое представление модели в исходном языке</param>
        /// <param name="rules">Все правила языков</param>
        /// <param name="sourceLang">Название исходного языка</param>
        /// <param name="targetLang">Название целевого языка</param>
        /// <returns>Текстовое представление модели в целевом языке</returns>
        /// <exception cref="TransformationComponent.NoLanguageRulesFound">
        /// Вызывается при отсутствии определения языка
        /// </exception>
        public string Transform(string text, AllRules rules, string sourceLang, string targetLang){
            if(!rules.HasLanguage(sourceLang)) throw new NoLanguageRulesFound(sourceLang);
            if(!rules.HasLanguage(targetLang)) throw new NoLanguageRulesFound(targetLang);
            throw new NotImplementedException();
        }
    }
}
