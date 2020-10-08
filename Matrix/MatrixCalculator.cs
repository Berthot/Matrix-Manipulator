using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBeMadeStatic.Local

namespace Matrix
{
    public class MatrixCalculator
    {
        public Matrix MultiplierParallel(Matrix matrixA, Matrix matrixB)
        {
            if (ValidateMultiplier(matrixA, matrixB))
                throw new Exception("Matrizes não validas");
            var matrixAxb = new Matrix();

            Parallel.For(0, 
                matrixA.GetRowLenght(), 
                new ParallelOptions {MaxDegreeOfParallelism = 5}, 
                lineMatrixA =>
            {
                var newLine = new List<double>();
                for (var columnMatrixB = 0; columnMatrixB < matrixB.GetColumnLenght(); columnMatrixB++)
                {
                    var acc = 0.0;
                    for (var i = 0; i < matrixA.GetColumnLenght(); i++)
                    {
                        var a = matrixA.GetMatrix()[lineMatrixA][i];
                        var b = matrixB.GetMatrix()[i][columnMatrixB];
                        acc += a * b;
                    }

                    newLine.Add(acc);
                }

                matrixAxb.AddLineList(newLine);
            });

            return matrixAxb;
        }

        public Matrix Multiplier(Matrix matrixA, Matrix matrixB)
        {
            if (ValidateMultiplier(matrixA, matrixB))
                throw new Exception("Matrizes não validas");
            var matrixAxb = new Matrix();
            for (var lineMatrixA = 0; lineMatrixA < matrixA.GetRowLenght(); lineMatrixA++)
            {
                var newLine = new List<double>();
                for (var columnMatrixB = 0; columnMatrixB < matrixB.GetColumnLenght(); columnMatrixB++)
                {
                    var acc = 0.0;
                    for (var i = 0; i < matrixA.GetColumnLenght(); i++)
                    {
                        var a = matrixA.GetMatrix()[lineMatrixA][i];
                        var b = matrixB.GetMatrix()[i][columnMatrixB];
                        acc += a * b;
                    }

                    newLine.Add(acc);
                }

                matrixAxb.AddLineList(newLine);
            }

            return matrixAxb;
        }

        public Matrix MultiplierFail(Matrix matrixA, Matrix matrixB)
        {
            if (ValidateMultiplier(matrixA, matrixB))
                throw new Exception("Matrizes não validas");
            var matrixAxb = new Matrix();
            for (var lineMatrixA = 0; lineMatrixA < matrixA.GetRowLenght() - 1; lineMatrixA++)
            {
                var acc = 0.0;
                for (var columnMatrixB = 0; columnMatrixB < matrixB.GetColumnLenght() - 1; columnMatrixB++)
                {
                    for (var i = 0; i < matrixA.GetColumnLenght() - 1; i++)
                    {
                        var a = matrixA.GetMatrix()[lineMatrixA][i];
                        var b = matrixB.GetMatrix()[i][columnMatrixB];
                        acc += a * b;
                    }
                }

                matrixAxb.AddValueOnIndex(acc, lineMatrixA, matrixB.GetColumnLenght());
            }

            return matrixAxb;
        }

        public int Sum(Matrix matrixA, Matrix matrixB)
        {
            return 0;
        }

        private bool ValidateMultiplier(Matrix matrixA, Matrix matrixB)
        {
            if (!matrixA.IsEmpty() || !matrixA.IsValid() || !matrixB.IsEmpty() || !matrixB.IsValid())
                return false;
            return matrixA.GetColumnLenght() == matrixB.GetRowLenght();
        }

        // public List<List<double>> Multiplier1(List<List<double>> matrixA, List<List<double>> matrixB)
        // {
        //     var matrixAxb = new List<List<double>>();
        //     var matrix = new Matrix();
        //     foreach (var lineMatrixA in matrixA)
        //     {
        //         var newLine = new List<double>();
        //         for (var columnMatrixB = 0; columnMatrixB < matrixA.Count; columnMatrixB++)
        //         {
        //             var acc = 0.0;
        //             for (var i = 0; i < lineMatrixA.Count; i++)
        //             {
        //                 var a = lineMatrixA[i];
        //                 var b = matrixB[i][columnMatrixB];
        //                 acc += a * b;
        //             }
        //
        //             newLine.Add(acc);
        //         }
        //         matrix.AddLineList(newLine);
        //         matrixAxb.Add(newLine);
        //     }
        //     matrix.PrintMatrix();
        //     return matrixAxb;
        // }
        //
        
        
    }
}