using System;
using System.Collections.Generic;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrixA = new Matrix();
            matrixA.AddLineList(new List<double> {1.0, 4.0});
            matrixA.AddLineList(new List<double> {2.0, 5.0});
            matrixA.AddLineList(new List<double> {3.0, 6.0});
            
            var matrixB = new Matrix();
            matrixB.AddLineList(new List<double> {1.0, 2.0, 3.0});
            matrixB.AddLineList(new List<double> {4.0, 5.0, 6.0});
            
            
            var calc = new MatrixCalculator();

            var result = calc.Multiplier(matrixA, matrixB);
            
            // calc.Multiplier1(matrixA.GetMatrix(), matrixB.GetMatrix());
            matrixA.PrintMatrix();
            matrixA.SaveMatrixInCsv("A");
            matrixB.SaveMatrixInCsv("B");
            matrixB.PrintMatrix();
            result.PrintMatrix();


        }
    }
}