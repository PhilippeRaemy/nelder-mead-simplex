using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using NumUtils.NelderMeadSimplex;

namespace NumUtilsTests
{
    public class NumUtilsTests
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting first simplex test...");
            _simplexTest1();
            _simplexTest2();
            Console.WriteLine("Hit any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Test to see if we can fit a parabola
        /// </summary>
        static void _simplexTest1()
        {
            Console.WriteLine("Starting SimplexTest1");
            var constants = new[] { new SimplexConstant(3, 1), new SimplexConstant(5, 1) };
            const double tolerance = 1e-6;
            const int maxEvals = 1000;
            var objFunction = new ObjectiveFunctionDelegate(_objFunction1);
            var result = NelderMeadSimplex.Regress(constants, tolerance, maxEvals, objFunction);
            _printResult(result);
        }

        static double _objFunction1(double[] constants)
        {
            double a = 5;
            double b = 10;

            Console.Write("Called with a={0} b={1}", constants[0], constants[1]);

            // evaluate it for some selected points, with a bit of noise
            double ssq = 0;
            var r = new Random();
            for (double x = -10; x < 10; x += .1)
            {
                var yTrue = a * x * x + b * x + r.NextDouble();
                var yRegress = constants[0] * x * x + constants[1] * x;
                ssq += Math.Pow((yTrue - yRegress), 2);
            }
            Console.WriteLine("  SSQ={0}", ssq);
            return ssq;
        }

        /// <summary>
        /// Test on the Rosenbrock function
        /// </summary>
        static void _simplexTest2()
        {
            Console.WriteLine("\n\nStarting SimplexTest2");

            // we are regressing for frequency, amplitude, and phase offset
            var constants = new[] { new SimplexConstant(-1.2, .1), new SimplexConstant(1, .1)};
            var tolerance = 1e-10;
            var maxEvals = 1000;
            var objFunction = new ObjectiveFunctionDelegate(_objFunction2);
            var result = NelderMeadSimplex.Regress(constants, tolerance, maxEvals, objFunction);
            _printResult(result);
        }

        static double _objFunction2(double[] constants)
        {
            Console.Write("Called with x1={0} x2={1} ", constants[0], constants[1]);
            var err = 100 * Math.Pow((constants[1] - constants[0] * constants[0]), 2) + Math.Pow((1 - constants[0]), 2);
            Console.WriteLine("  err={0}", err);

            return err;
        }

        static void _printResult(RegressionResult result)
        {
            // a bit of reflection fun, why not...
            var properties = result.GetType().GetProperties();
            foreach (var p in properties)
            {
                Console.WriteLine(p.Name + ": " + p.GetValue(result, null).ToString());
            }
        }
    }
}
