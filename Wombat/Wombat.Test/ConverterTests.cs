namespace Wombat.Test
{
    using System;

    using NUnit.Framework;

    using Wombat.Core;

    [TestFixture]
    public class ConverterTests
    {
        private IWombatConverter target;

        [SetUp]
        public void SetUp()
        {
            this.target = WombatConverter.CreateInitialized();
        }

        [Test]
        public void Convert_ShouldConvertIntToString()
        {
            var expectedResult = "5";

            var actualResult = this.target.Convert<int, string>(5);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Convert_ShouldConvertDoubleToInt_IfRegistered()
        {
            var expectedResult = 5;

            this.target.Register<double, int>(doubleValue => (int)doubleValue);
            var actualResult = this.target.Convert<double, int>(5.1);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Convert_ShouldThrowsException_IfSuchConverterNotRegistered()
        {
            this.target.Register<double, int>(doubleValue => (int)doubleValue);

            Assert.Throws<ArgumentException>(() => this.target.Convert<double, string>(5.1));
        }

        [Test]
        public void Convert_ShouldThrowsException_IfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => this.target.Convert<string, string>(null));
            Assert.Throws<ArgumentNullException>(() => this.target.Convert<string, string>(null, string.Empty));
        }

        [Test]
        public void Convert_ShouldThrowsException_IfTypeNotSupported()
        {
            Assert.Throws<ArgumentException>(() => this.target.Convert(string.Empty, string.Empty));
        }

        [Test]
        public void Convert_ShouldReturnDefaultValue_IfFuncThrowException()
        {
            this.target.Register<string, string>(s => { throw new NotImplementedException(); });
            var defaultValue = string.Empty;

            var result = this.target.Convert(string.Empty, defaultValue);

            Assert.True(result == defaultValue);
        }

        [Test]
        public void Ovveride_ShouldThrowsException_IfSuchConverterNotRegistered()
        {
            Assert.Throws<ArgumentException>(() => this.target.Ovveride<byte, byte>(b => b));
        }

        [Test]
        public void Ovveride_ShouldOverrideConverter_IfRegistered()
        {
            Assert.Throws<ArgumentException>(() => this.target.Ovveride<byte, byte>(b => b));
        }

        [Test]
        public void Override_ShouldReturnExpectedValue_IfOverrided()
        {
            this.target.Register<string, string>(s => string.Empty);
            this.target.Ovveride<string, string>(s => s);
            var expected = "test";

            var actual = this.target.Convert<string, string>(expected);

            Assert.True(expected == actual);
        }

        [Test]
        public void Register_ShouldThrowsException_IfAlreadyRegistered()
        {
            this.target.Register<string, string>(s => s);

            Assert.Throws<ArgumentException>(() => this.target.Register<string, string>(s => s));
        }
    }
}
