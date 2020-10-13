using System;
using System.Diagnostics;

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
            const int m = 120;
            const int n = 120;
            const int k = 120;
            matrixA.CreateCompleteMatrix(m, n);
            matrixB.CreateCompleteMatrix(n, k);
            var calc = new MatrixCalculator();
            var dimension = $"Tentativas: {m.ToString()}x{k.ToString()}x{n.ToString()}";
            
            var matrixResult = new Matrix();
            var stopwatch = new Stopwatch();
            
            matrixResult.CreateCompleteMatrix(m, n);
            
            stopwatch.Start();
            var matrix2 = calc.Multiplier(matrixA, matrixB, matrixResult);
            stopwatch.Stop();
            
            matrixResult.CreateCompleteMatrix(m, n);

            matrix2.SaveMatrixInCsvWithTime("normal", stopwatch.Elapsed, dimension);
            Console.WriteLine($"normal   : {stopwatch.Elapsed.ToString()}");
            
            stopwatch.Reset();
                        
            stopwatch.Start();
            var matrix1 = calc.MultiplierParallel(matrixA, matrixB, matrixResult);
            stopwatch.Stop();
            
            matrix1.SaveMatrixInCsvWithTime("paralela", stopwatch.Elapsed, dimension);
            Console.WriteLine($"paralela : {stopwatch.Elapsed.ToString()}");









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