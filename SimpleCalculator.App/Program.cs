using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleCalculator.Lib;
using SimpleCalculator.Log;
using SimpleCalculator.Log.DataModel;
using System;
using System.Threading.Tasks;

namespace SimpleCalculator.App
{
    class Program
    {
        static IServiceProvider _serviceProvider;
        static async Task Main(string[] args)
        {
            //Initialize dependency injection IoC container and create database if doesn't exist
            await Init();

            var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            Console.WriteLine($"{appName} starts!!");

            //Call the SimpleCalculator without using IoC container
            UseSimpleCalculatorLib(false);

            //Call the SimpleCalculator and provide logger dependency via IoC
            //Currently using entity framework logger functionality 
            //and logger can be changed to console or ADO in Init method 
            UseSimpleCalculatorLib(true);

            Console.WriteLine($"{appName} ends!!");
        }
        /// <summary>
        /// Use the SimpleCalculator library with or without IoC
        /// </summary>
        /// <param name="use_IoC">Pass true for IoC</param>
        private static void UseSimpleCalculatorLib(bool use_IoC)
        {
            var start = 10;
            var amount = 5;
            var by = 2;

            ISimpleCalculator mycalc;
            if (use_IoC)
                mycalc = _serviceProvider.GetService<ISimpleCalculator>();
            else
                mycalc = new SimpleCalculatorLib();

            mycalc.Add(start, amount);
            mycalc.Subtract(start, amount);
            mycalc.Multiply(start, by);
            mycalc.Divide(start, by);
            mycalc = null;
        }

        /// <summary>
        /// Initialize IoC container and add services. Create database if not exists 
        /// </summary>        
        private static async Task Init()
        {
            //Comment the Logger service to make it non-active 
            //UnComment the logger service to make it active
            //Currently EFLogger service is active
            _serviceProvider = new ServiceCollection()
                .AddSingleton<ISimpleCalculator, SimpleCalculatorLib>()

                .AddSingleton<ISimpleCalculatorLogger, SimpleCalculatorEFLogger>()
                //.AddSingleton<ISimpleCalculatorLogger, SimpleCalculatorADOLogger>()
                //.AddSingleton<ISimpleCalculatorLogger, SimpleCalculatorConsoleLogger>()                

                .AddDbContext<SimpleCalculatorDBContext>()
                .BuildServiceProvider();

            //Create database
            using (var context = _serviceProvider.GetRequiredService<SimpleCalculatorDBContext>())
            {
                //Create SimpleCalculatorDB database using LocalMSSQLLocalDB
                bool created = await context.Database.EnsureCreatedAsync();
                //If database is created for first time then add store procedure to database
                if (created)
                {
                    //Add store-procedure to insert diagnostics info
                    string spsql = @"CREATE PROCEDURE [dbo].[InsertSimpleCalculatorLog]	
	                            @Action nvarchar(max),
	                            @Value1 int,
	                            @Value2 int,
	                            @Result int,
	                            @Source nvarchar(max)
                            AS
	                            INSERT INTO Log([Action],Value1,Value2,Result,Source)
	                            Values(@Action,@Value1,@Value2,@Result,@Source)
                            RETURN 0";
                    await context.Database.ExecuteSqlRawAsync(spsql);
                   
                }
            }
        }
    }
}
