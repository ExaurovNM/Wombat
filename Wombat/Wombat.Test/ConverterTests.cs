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
            var expectedResult = "5";

            var actualResult = target.Convert<int, string>(5);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Convert_ShouldConvertDoubleToInt_IfRegistered()
        {
            WombatConverter target = new WombatConverter();
            var expectedResult = 5;

            target.Register<double, int>(doubleValue => (int)doubleValue);
            var actualResult = target.Convert<double, int>(5.1);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Convert_ShouldThrowsException_IfSuchConverterNotRegistered()
        {
            WombatConverter target = new WombatConverter();

            target.Register<double, int>(doubleValue => (int)doubleValue);

            Assert.Throws<ArgumentException>(() => target.Convert<double, string>(5.1));
        }

        [Test]
        public void Ovveride_ShouldThrowsException_IfSuchConverterRegistered()
        {
            WombatConverter target = new WombatConverter();

            Assert.Throws<ArgumentException>(() => target.Convert<byte, byte>(byte.MaxValue));
        }
    }
}
