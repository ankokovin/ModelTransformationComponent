using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTransformationComponent;
namespace TransformationComponentUnitTest
{
    public static class TestUtil
    {
        
        public static void AssertReg(RegexRule rule, string expectedName, string expectedPattren)
        {
            Assert.IsNotNull(rule);
            Assert.AreEqual(expectedName, rule.Name);
            Assert.AreEqual(expectedPattren, rule.Pattern);
        }


        public static void AssertBNF(BNFRule rule, string expectedName, params BasicBNFRule[] expetedOrs)
        {
            Assert.IsNotNull(rule);
            Assert.AreEqual(expectedName, rule.Name);
            Assert.AreEqual(expetedOrs.Length, rule.OrSplits.Count);

            for (int i = 0; i < expetedOrs.Length; i++)
            {
                Assert.AreEqual(expetedOrs[i], rule.OrSplits[i]);
            }

        }
        
    }
}
