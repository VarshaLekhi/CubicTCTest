using SimpleCalculator.Log;
using SimpleCalculator.Log.DataModel;

namespace SimpleCalculator.Lib
{
    /// <summary>
    /// Implements ISimpleCalculator interface
    /// </summary>
    public class SimpleCalculatorLib : ISimpleCalculator
    {
        private readonly ISimpleCalculatorLogger _logger;
        public SimpleCalculatorLib()
        {
            //Create the dependency directly (Avoiding SOLID principle)
            _logger = new SimpleCalculatorConsoleLogger();
        }
        /// <summary>
        /// Use IoC container to get the logger dependency
        /// </summary>
        /// <param name="logger"></param>
        public SimpleCalculatorLib(ISimpleCalculatorLogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Add two input integer parameters start and amount
        /// </summary>
        /// <param name="start">Integer parameter value</param>
        /// <param name="amount">Integer parameter value</param>
        /// <returns>result as integer value</returns>
        public int Add(int start, int amount)
        {
            //Calculate result
            var result = start + amount;

            //Create diagnostics information for this operation
            var log = new SimpleCalculatorLog("Add", start, amount, result);
            //Write diagnostics information
            _logger.WriteLog(log);

            //return the final result
            return result;
        }
        /// <summary>
        /// Subtract amount integer parameter value from start interger value
        /// </summary>
        /// <param name="start">Integer parameter value</param>
        /// <param name="amount">Integer parameter value</param>
        /// <returns>result as integer value</returns>
        public int Subtract(int start, int amount)
        {
            //Calculate result
            var result = start - amount;

            //Create diagnostics information for this operation
            var log = new SimpleCalculatorLog("Subtract", start, amount, result);
            //Write diagnostics information
            _logger.WriteLog(log);

            //return the final result
            return result;
        }
        /// <summary>
        /// Divide start integer parameter value by interger parameter "by" 
        /// </summary>
        /// <param name="start">Integer parameter value</param>
        /// <param name="by">Integer parameter value</param>
        /// <returns>result as integer value</returns>
        public int Divide(int start, int by)
        {
            //Calculate result
            var result = start / by;

            //Create diagnostics information for this operation
            var log = new SimpleCalculatorLog("Divide", start, by, result);
            //Write diagnostics information
            _logger.WriteLog(log);

            //return the final result
            return result;
        }
        /// <summary>
        /// Multiply two integer parameters
        /// </summary>
        /// <param name="start">Integer parameter value</param>
        /// <param name="by">Integer parameter value</param>
        /// <returns>result as integer value</returns>
        public int Multiply(int start, int by)
        {
            //Calculate result
            var result = start * by;

            //Create diagnostics information for this operation
            var log = new SimpleCalculatorLog("Multiply", start, by, result);
            //Write diagnostics information
            _logger.WriteLog(log);

            //return the final result
            return result;
        }

    }
}
