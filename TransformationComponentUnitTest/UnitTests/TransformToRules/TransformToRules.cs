using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;
using System.Reflection;
using System.Resources;

namespace TransformationComponentUnitTest
{
    public partial class TransformUnitTest
    {
        [TestClass]
        public partial class TransformToRules
        {

            [TestMethod]
            public void PascalCSharp()
            {
                //arrange
                string text = TransformationComponentUnitTest.Resource1.CSharpPascalRules;

                var component = new TransformationComponent();

                //act
                var actual = component.TransformToRules(text);

            }
        }
    }
}
