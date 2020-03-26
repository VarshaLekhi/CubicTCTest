using Moq;
using SimpleCalculator.Lib;
using SimpleCalculator.Log;
using SimpleCalculator.Log.DataModel;
using System;
using Xunit;

namespace SimpleCalculator.Tests
{
    /// <summary>
    /// Test SimpleCalculator library
    /// </summary>
    public class SimpleCalculatorTests
    {
        private Mock<ISimpleCalculatorLogger> _mock;
        private SimpleCalculatorLib _sut;

        public SimpleCalculatorTests()
        {
            _mock = new Mock<ISimpleCalculatorLogger>();
            _sut = new SimpleCalculatorLib(_mock.Object);
        }
        /// <summary>
        /// Test Add method for passed result to diagnostics 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="amount"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData(5, 2, 7)]
        [InlineData(0, 0, 0)]
        public void Add_VerifyPassedResultToDiagnostics(int start, int amount, int expected)
        {
            //Arrange
            int actual = 0;
            _mock.Setup(l => l.WriteLog(It.IsAny<SimpleCalculatorLog>()))
             .Callback<SimpleCalculatorLog>(p => { actual = p.Result; });

            //Act
            _sut.Add(start, amount);

            //Assert
            Assert.Equal<int>(expected, actual);

        }
        /// <summary>
        /// Throws Divide by zero exception
        /// </summary>
        [Fact]
        public void Divide_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => _sut.Divide(1, 0));
        }
        /// <summary>
        /// Test subtract method for passed result to diagnostics 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="amount"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData(5, 2, 3)]
        [InlineData(0, 0, 0)]
        public void Subtract_VerifyPassedResultToDiagnostics(int start, int amount, int expected)
        {
            //Arrange
            int actual = 0;
            _mock.Setup(l => l.WriteLog(It.IsAny<SimpleCalculatorLog>()))
             .Callback<SimpleCalculatorLog>(p => { actual = p.Result; });

            //Act
            _sut.Subtract(start, amount);

            //Assert
            Assert.Equal<int>(expected, actual);
        }
        /// <summary>
        /// Test divide method for passed result to diagnostics
        /// </summary>
        [Fact]
        public void Divide_VerifyPassedResultToDiagnostics()
        {
            //Arrange
            int actual = 0;
            _mock.Setup(l => l.WriteLog(It.IsAny<SimpleCalculatorLog>()))
             .Callback<SimpleCalculatorLog>(p => { actual = p.Result; });

            //Act
            _sut.Divide(100, 10);

            //Assert
            Assert.Equal<int>(10, actual);
        }
        /// <summary>
        /// Test multiply method for passed result to diagnostics
        /// </summary>
        [Fact]
        public void Multiply_VerifyPassedResultToDiagnostics()
        {
            //Arrange
            int actual = 0;
            _mock.Setup(l => l.WriteLog(It.IsAny<SimpleCalculatorLog>()))
             .Callback<SimpleCalculatorLog>(p => { actual = p.Result; });

            //Act
            _sut.Multiply(100, 10);

            //Assert
            Assert.Equal<int>(1000, actual);
        }
    }
}
