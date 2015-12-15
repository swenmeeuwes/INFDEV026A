using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    class AlgorithmAnalyser
    {
        private Stopwatch stopwatch;
        public AlgorithmAnalyser()
        {
            stopwatch = new Stopwatch();
        }

        private void BeginEmpiricalAnalysis()
        {
            Console.WriteLine("Starting Empirical analysis ...");
            stopwatch.Restart();
        }
        private long EndEmpiricalAnalysis()
        {
            stopwatch.Stop();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Empirical analysis");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Elapsed milliseconds: {0}.", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("DONE, End of Empirical analysis ...\n");
            return stopwatch.ElapsedMilliseconds;
        }
        public T EmpiricalAnalysis<T>(object obj, string methodName, params object[] arguments)
        {
            Console.WriteLine("\n\nExecuting algorithm \"{0}\".", methodName);
            BeginEmpiricalAnalysis();
            Type type = obj.GetType();
            MethodInfo methodInfo = type.GetMethod(methodName);
            T result = (T)methodInfo.Invoke(obj, arguments);
            EndEmpiricalAnalysis();
            return result;
        }
    }
}