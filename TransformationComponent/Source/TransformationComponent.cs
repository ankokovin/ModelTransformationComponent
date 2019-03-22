﻿using System;
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

                var startIdx = rules.IndexOf(new Start().GetLiteral);
                Debug.WriteLine("Index for /start: " + startIdx);

                if (startIdx == -1)
                    throw new NoStartDetected();


                var endIdx = rules.IndexOf(new End().GetLiteral);
                Debug.WriteLine("Index for /end: " + endIdx);

                if (endIdx == -1)
                    throw new NoEndDetected();


                var text = rules.Substring(startIdx, endIdx - startIdx + new End().GetLiteral.Length);

                Debug.WriteLine("---------Inside text----------");
                Debug.WriteLine(text);
                Debug.WriteLine("-----------------------");


                var firstlang = text.IndexOf(new Language_start().GetLiteral);


                Debug.WriteLine("First idx for /language_start: " + firstlang);
                if (firstlang != -1)
                {
                    var baseLang = GetBaseDescription(text.Substring(0, firstlang));
                    result.AddBaseRules(baseLang);

                    var languages = text.Substring(firstlang).Split(
                        new[] { new Language_end().GetLiteral }, StringSplitOptions.None);

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

        /// <summary>
        /// Получить базовые структуры 
        /// </summary>
        /// <param name="text">Текстовое описание базовых структур</param>
        /// <returns>Базовые структуры</returns>
        private Dictionary<string, Rule> GetBaseDescription(string text){
            Debug.WriteLine("getting base description");
            Debug.WriteLine("text:");
            Debug.WriteLine(text);
            try {
                throw new NotImplementedException();
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