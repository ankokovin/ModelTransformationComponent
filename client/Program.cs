using System;
using System.IO;
using ModelTransformationComponent;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
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
            try{
            int ar = 0;
            
            var transf = new ModelTransformationComponent.TransformationComponent();
            switch(args[0]){
                case "-h":
                case "--h":
                    Help();
                    return;

                case "-r":
                    var r_rules_txt = File.ReadAllText(args[1]);
                    var r_rules = transf.TransformToRules(r_rules_txt);
                    var formatter = new BinaryFormatter();
                    using (FileStream fs = new FileStream(args[2], FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, r_rules);
                        Console.WriteLine("Done");
                    }
                    return;

                case "-fr":
                    formatter = new BinaryFormatter();
                    AllRules fr_rules;
                    using (FileStream fs = new FileStream(args[2], FileMode.OpenOrCreate))
                    {
                        fr_rules = (AllRules)formatter.Deserialize(fs);
                    }
                    var fr_text = File.ReadAllText(args[1]);
                    File.WriteAllText(args[5],transf.Transform(fr_text,fr_rules, args[3], args[4]));       
                    return;

                case "-f":
                    ar++;
                    break;
            
            }
            var text = File.ReadAllText(args[ar]);
            var rules = File.ReadAllText(args[ar+1]);
            var result = transf.Transform(text,rules, args[ar+2], args[ar+3]);
            if (args.Length == ar+5)
                File.WriteAllText(args[ar+4],result);
            else
                Console.WriteLine(result);
            }
            catch(TransformComponentException tr){
                System.Exception ex = tr;
                do{
                    Console.WriteLine(ex.Message);
                    ex = ex.InnerException;
                }while(ex!=null);
            }
            catch(System.Exception){
                Console.WriteLine("Ошибка в аргументах вызова");
                Help();
            }
        }

        static void Help(){
            Console.WriteLine("Консольный клиент компонента трансформации");
            Console.WriteLine("client [-h|--h|-r|-fr|-f] <args>");
            Console.WriteLine("-h|--h : info");
            Console.WriteLine("-r : parse rules, <args> ::= <rules txt source><rules serialize target>");
            Console.WriteLine("-fr: parse model with already serialized rules, <args> ::= <model txt><rules serialized source><source language><target language><output txt>");
            Console.WriteLine("-f | none : parse both rules and model, <args> ::= <model txt><rules txt><source language><target language><output txt>");
                    
        }
    }
}
