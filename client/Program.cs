using System;
using System.IO;
using ModelTransformationComponent;
using System.Diagnostics;
namespace client
{
    class Program
    {
        /// <summary>
        /// Входная точка программы
        /// </summary>
        /// <param name="args">
        /// Ожидается 4 аргумента
        /// <list type="number">
        ///     <listheader>
        ///         <description>Список аргументов</description>
        ///     </listheader>
        ///     <item>
        ///         <description>Расположение файла с моделью</description>
        ///     </item>
        ///     <item>
        ///         <description>Расположение файла с описанием трансформаций</description>
        ///     </item>
        ///     <item>
        ///         <description>Название исходного языка</description>
        ///     </item>
        ///     <item>
        ///         <description>Название целевого языка</description>
        ///     </item>
        /// </list>
        /// </param>
        static void Main(string[] args)
        {
            var transf = new ModelTransformationComponent.TransformationComponent();
            var text = File.ReadAllText(args[0]);
            var rules = File.ReadAllText(args[1]);
            Console.WriteLine(transf.Transform(text,rules, args[2], args[3]));
        }
    }
}
