using System;
using TransformationComponent;
namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            var transf = new TransformationComponent.TransformationComponent();
            Console.WriteLine(transf.Transform(args[0],args[1]));
        }
    }
}
