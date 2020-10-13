using System;
using System.Threading.Tasks;

// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBeMadeStatic.Local

namespace Matrix
{
    public class MatrixCalculator
    {
        public Matrix MultiplierParallel(Matrix matrixA, Matrix matrixB, Matrix result)
        {
            // if (ValidateMultiplier(matrixA, matrixB))
            //     throw new Exception("Matrizes não validas");
            var matrixARow = matrixA.GetRowLenght();
            var matrixBColumn = matrixB.GetColumnLenght();
            var matrixAColumn = matrixA.GetColumnLenght();
            
            var matrixAClone = matrixA.GetMatrix();
            var matrixBClone = matrixB.GetMatrix();
            
            Parallel.For(0, matrixARow, body: lineMatrixA =>
            {
                for (var columnMatrixB = 0; columnMatrixB < matrixBColumn; columnMatrixB++)
                {
                    double acc = 0;
                    for (var i = 0; i < matrixAColumn; i++)
                    {
                        var a = matrixAClone[lineMatrixA][i];
                        var b = matrixBClone[i][columnMatrixB];
                        acc += a * b;
                    }
                    result.AddValueOnIndex(acc, lineMatrixA, columnMatrixB);
                }

            });

            return result;
        }

        public Matrix Multiplier(Matrix matrixA, Matrix matrixB, Matrix result)
        {
            // if (ValidateMultiplier(matrixA, matrixB))
            //     throw new Exception("Matrizes não validas");
            var matrixARow = matrixA.GetRowLenght();
            var matrixBColumn = matrixB.GetColumnLenght();
            var matrixAColumn = matrixA.GetColumnLenght();
            
            var matrixAClone = matrixA.GetMatrix();
            var matrixBClone = matrixB.GetMatrix();

            for (var lineMatrixA = 0; lineMatrixA < matrixARow; lineMatrixA++)
            {
                for (var columnMatrixB = 0; columnMatrixB < matrixBColumn; columnMatrixB++)
                {
                    var acc = 0.0;
                    for (var i = 0; i < matrixAColumn; i++)
                    {
                        var a = matrixAClone[lineMatrixA][i];
                        var b = matrixBClone[i][columnMatrixB];
                        acc += a * b;
                    }
                    result.AddValueOnIndex(acc, lineMatrixA, columnMatrixB);
                }
            }

            return result;
        }

        public Matrix MultiplierFail(Matrix matrixA, Matrix matrixB)
        {
            if (ValidateMultiplier(matrixA, matrixB))
                throw new Exception("Matrizes não validas");
            var matrixAxb = new Matrix();
            matrixAxb.CreateCompleteMatrix(matrixA.GetRowLenght(), matrixB.GetColumnLenght());
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
                    matrixAxb.AddValueOnIndex(acc, lineMatrixA, columnMatrixB);

                }

                // matrixAxb.AddValueOnIndex(acc, lineMatrixA, matrixB.GetColumnLenght());
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