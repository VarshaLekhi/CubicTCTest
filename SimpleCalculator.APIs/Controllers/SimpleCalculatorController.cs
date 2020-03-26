using Microsoft.AspNetCore.Mvc;
using SimpleCalculator.Lib;
using System.ComponentModel.DataAnnotations;

namespace SimpleCalculator.APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimpleCalculatorController : ControllerBase
    {
        private ISimpleCalculator _calculator;
        /// <summary>
        /// Get SimpleCalculator from IoC container
        /// </summary>
        /// <param name="simplecalculator"></param>
        public SimpleCalculatorController(ISimpleCalculator simplecalculator)
        {
            _calculator = simplecalculator;
        }
        /// <summary>
        /// Add two integer numbers
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Result as integer</returns>
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody]SimpleCalculatorData input)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(_calculator.Add(input.value1, input.value2));
        }
        /// <summary>
        /// Subtract two integer numbers value1 - value2
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Result as integer</returns>
        [HttpPost]
        [Route("Subtract")]
        public IActionResult Subtract([FromBody]SimpleCalculatorData input)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(_calculator.Subtract(input.value1, input.value2));
        }
        /// <summary>
        /// Multiply two integer numbers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Multiply")]
        public IActionResult Multiply([FromBody]SimpleCalculatorData input)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(_calculator.Multiply(input.value1, input.value2));
        }
        /// <summary>
        /// Divide 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Divide")]
        public IActionResult Divide([FromBody]SimpleCalculatorData input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                return Ok(_calculator.Divide(input.value1, input.value2));
            }
            catch (System.DivideByZeroException)
            {
                return BadRequest();
            }
        }
    }
    /// <summary>
    /// Input data structure to pass information to API in json format
    /// </summary>
    public class SimpleCalculatorData
    {
        [Required]
        public int value1 { get; set; }
        [Required]
        public int value2 { get; set; }
    }
}
