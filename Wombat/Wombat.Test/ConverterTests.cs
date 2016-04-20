namespace Wombat.Test
{
    using System;

    using NUnit.Framework;

    using Wombat.Core;

    [TestFixture]
    public class ConverterTests
    {
        [Test]
        public void Convert_ShouldConvertIntToString()
        {
            WombatConverter target = new WombatConverter();

            var result = target.Convert<int, string>(5);

            var expectedResult = "5";

            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void Convert_ShouldConvertDoubleToInt_IfRegistered()
        {
            WombatConverter target = new WombatConverter();

            target.Register<double, int>(doubleValue => (int)doubleValue);
            var result = target.Convert<double, int>(5.1);

            var expectedResult = 5;

            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void Convert_ShouldThrowsException_IfSuchConverterNotRegistered()
        {
            WombatConverter target = new WombatConverter();

            target.Register<double, int>(doubleValue => (int)doubleValue);

            Assert.Throws<ArgumentException>(() => target.Convert<double, string>(5.1));
        }
    }
}
