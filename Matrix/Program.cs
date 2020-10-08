using System;
using System.Diagnostics;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrixA = new Matrix();
            var matrixB = new Matrix();
            matrixA.CreateCompleteMatrix(130, 130);
            matrixB.CreateCompleteMatrix(130, 130);
            var result = new Matrix();
            var calc = new MatrixCalculator();

            var stopwatch = new Stopwatch();
            

            
            
            stopwatch.Start();
            var matrix2 = calc.Multiplier(matrixA, matrixB);
            stopwatch.Stop();
            matrix2.SaveMatrixInCsvWithTime("normal", stopwatch.Elapsed);
            Console.WriteLine($"normal : {stopwatch.Elapsed.ToString()}");
            
            stopwatch.Restart();
                        
            stopwatch.Start();
            var matrix1 = calc.MultiplierParallel(matrixA, matrixB);
            stopwatch.Stop();
            matrix1.SaveMatrixInCsvWithTime("paralela", stopwatch.Elapsed);
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