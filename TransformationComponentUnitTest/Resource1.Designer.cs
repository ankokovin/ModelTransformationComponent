﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransformationComponentUnitTest {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource1 {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource1() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TransformationComponentUnitTest.Resource1", typeof(Resource1).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на /start
        /////Предыдущий символ показывает, что началость описание трансформаций
        /////Сначала описывается совместная структура языков
        /////Поддержка регулярных выражений
        /////Структура &quot;ноль или больше пробельных символов&quot;
        ////reg ws_zero ::= \s*
        ///
        /////Структура &quot;один или больше пробельных символов&quot;
        ////reg ws_one ::=  \s+
        ///
        ///
        /////Для пробела и пустоты есть системные символы
        /////Структура &quot;начало блока&quot;
        ///start_block ::= /empty
        ///
        /////Структура &quot;конец блока&quot;
        ///end_block ::= /empty
        ///
        /////Некоторые конструкции могут быть просто оп [остаток строки не уместился]&quot;;.
        /// </summary>
        public static string CSharpPascalRules {
            get {
                return ResourceManager.GetString("CSharpPascalRules", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на namespace test{
        ///	class test{
        ///		int main(){
        ///			int i;
        ///			for(i:=1;i&lt;=10;++i){
        ///				System.Console.WriteLine(i);
        ///			};
        ///		}
        ///	}
        ///}.
        /// </summary>
        public static string CSharpSource {
            get {
                return ResourceManager.GetString("CSharpSource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на program test;
        ///var i:Integer;
        ///begin
        ///for i:=1 to 10 do begin
        ///writeln(i);
        ///end;
        ///end..
        /// </summary>
        public static string PascalSource {
            get {
                return ResourceManager.GetString("PascalSource", resourceCulture);
            }
        }
    }
}
