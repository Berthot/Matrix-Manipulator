using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Matrix
{
    
    // m numero de linhas primeira matriz, k coluna primeira matriz, n colunas segunda matriz
    // 
    class Program
    {
        static void Main(string[] args)
        {
            var matrixA = new Matrix();
            var matrixB = new Matrix();
            const int m = 2000;
            const int k = 4000;
            const int n = 2000;
            matrixA.CreateCompleteMatrix(m, k);
            matrixB.CreateCompleteMatrix(k, n);
            var calc = new MatrixCalculator();
            var dimension = $"m={m.ToString()}k={k.ToString()}n={n.ToString()}";
            
            var matrixResult = new Matrix();
            var stopwatch = new Stopwatch();
            
            matrixResult.CreateCompleteMatrix(m, n);
            
            stopwatch.Start();
            var matrix2 = calc.Multiplier(matrixA, matrixB, matrixResult);
            stopwatch.Stop();
            
            matrixResult.CreateCompleteMatrix(m, n);

            matrix2.SaveMatrixInCsvWithTime("normal3", stopwatch.Elapsed, dimension);
            Console.WriteLine($"normal   : {stopwatch.Elapsed.ToString()}");
            
            stopwatch.Reset();
                        
            stopwatch.Start();
            var matrix1 = calc.MultiplierParallel(matrixA, matrixB, matrixResult);
            stopwatch.Stop();
            matrix1.SaveMatrixInCsvWithTime("paralela3", stopwatch.Elapsed, dimension);
            Console.WriteLine($"paralela : {stopwatch.Elapsed.ToString()}");
            
            // IEnumerable<int> numbers = Enumerable.Range(0, 10);
            // var evens = from num in numbers where num % 2 == 0 select num;


        }
        
        // var matrixA = new Matrix();
        // // matrixA.ReadMatrixByCsv("A");
        // matrixA.CreateCompleteMatrix(1000);
        // matrixA.SaveMatrixInCsv("test");
        // Process.GetCurrentProcess().Kill();
        //     
        // var matrixB = new Matrix();
        // matrixB.ReadMatrixByCsv("B");
        //     
        // var calc = new MatrixCalculator();
        // var result = calc.Multiplier(matrixA, matrixB);
        //     
        // // matrixA.PrintMatrix();
        // // matrixB.PrintMatrix();
        // // result.PrintMatrix();
        // var directThreadsCount = Process.GetCurrentProcess().Threads.Count;
        // Console.WriteLine(directThreadsCount);
    }
}