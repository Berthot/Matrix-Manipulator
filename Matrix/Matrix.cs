using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable StringLiteralTypo
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable 659


namespace Matrix
{
    public class Matrix
    {
        private List<List<double>> _matrix;
        private const double Tolerance = 0.01;
        private static char _csvSplit;

        public Matrix(char csvSplit = ',')
        {
            _matrix = new List<List<double>>();
            _csvSplit = csvSplit;

        }

        public void AddLineRandomValues(int lenght, int maxGenerateNumber = 10)
        {
            var line = new List<double>();
            for (var i = 0; i < lenght; i++)
            {
                line.Add(RandomValue(maxGenerateNumber));
            }

            _matrix.Add(line);
        }
        
        public void AddLineRandomValuesEmpty(int lenght)
        {
            var line = new List<double>();
            for (var i = 0; i < lenght; i++)
            {
                line.Add(-1.0);
            }

            _matrix.Add(line);
        }

        public void AddLineList(List<double> line)
        {
            if (line.Any(value => value.GetType() != typeof(double)))
            {
                throw new Exception("Somente valores 'Double' são permetidos");
            }

            _matrix.Add(line);
        }

        public void CreateCompleteMatrix(int rows, int column)
        {
            for (var i = 0; i < rows; i++)
            {
                AddLineRandomValues(column);
            }
        }
        
        public void CreateCompleteMatrixEmpty(int rows, int column)
        {
            for (var i = 0; i < rows; i++)
            {
                AddLineRandomValuesEmpty(column);
            }
        }
        
        public void CreateMatrixColumnLine(int column, int line)
        {
            for (var i = 0; i < line; i++)
            {
                AddLineRandomValues(column);
            }
        }

        public List<List<double>> GetMatrix()
        {
            return new List<List<double>>(_matrix);
        }

        public int GetColumnLenght()
        {
            return _matrix.Count == 0 ? 0 : _matrix[0].Count;
        }

        public int GetRowLenght()
        {
            return _matrix.Count == 0 ? 0 : _matrix.Count;
        }

        public void SaveMatrixInCsv(string csvName)
        {
            var csv = new StringBuilder();
            foreach (var line in _matrix)
            {
                var newLine =
                    line.Aggregate("", (current, value) =>
                        current + $"{value.ToString(CultureInfo.InvariantCulture)},");

                var saveLine = string.Format($"{newLine.TrimEnd(_csvSplit)}\n", Environment.NewLine);
                csv.Append(saveLine);
            }

            File.WriteAllText(GetPath(csvName), csv.ToString());
        }
        
        public void SaveMatrixInCsvWithTime(string csvName, TimeSpan time, string dimension)
        {
            var csv = new StringBuilder();
            csv.Append($"tempo de execução{_csvSplit.ToString()}{time.ToString()} sec,{dimension}\n\n");
            foreach (var line in _matrix)
            {
                var newLine =
                    line.Aggregate("", (current, value) =>
                        $"{current}{value.ToString(CultureInfo.InvariantCulture)},");

                var saveLine = string.Format($"{newLine.TrimEnd(_csvSplit)}\n", Environment.NewLine);
                csv.Append(saveLine);
            }

            File.WriteAllText(GetPath(csvName), csv.ToString());
        }


        public void ReadMatrixByCsv(string csvName)
        {
            try
            {
                using var reader = new StreamReader(GetPath(csvName));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if(line == "") continue;
                    if (line == null) throw new NullReferenceException();
                    var matrixLine =
                        line.Split(_csvSplit).Select(double.Parse).ToList();
                    _matrix.Add(matrixLine);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Falha na conversão dos valores para double");
                ClearMatrix();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Matrix com valores nulos");
                ClearMatrix();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ClearMatrix();
                Console.WriteLine("Erro ao captar a matriz do csv, corrija e tente novamente");
            }
        }

        public void ClearMatrix()
        {
            _matrix = new List<List<double>>();
        }

        private static string GetPath(string csvName)
        {
            var currentPath = Directory.GetCurrentDirectory().Replace("/bin/Debug/netcoreapp3.1", "");
            return $"{currentPath}/MatrixFiles/{csvName}.csv";
        }

        public void PrintMatrix()
        {
            if (IsEmpty())
            {
                Console.WriteLine("A matriz está vazia");
                return;
            }

            Console.WriteLine("==================================================");
            foreach (var line in _matrix)
            {
                foreach (var column in line)
                {
                    Console.Write($" | {column.ToString(CultureInfo.InvariantCulture)}");
                }

                Console.Write(" | ");
                Console.WriteLine();
            }
        }

        public bool IsValid()
        {
            foreach (var line in _matrix)
            {
                if (line.Contains(-1.0) || line.Contains(0.0))
                    return false;
            }

            return true;
        }

        public bool IsEmpty()
        {
            return GetColumnLenght() == 0 && GetRowLenght() == 0;
        }

        public void AddValueOnIndex(double value, int row, int column)
        {
            _matrix[row][column] = value;
        }


        public override bool Equals(object other)
        {
            if (other == null || other.GetType() != typeof(Matrix))
                return false;
            if (other.GetHashCode() == GetHashCode())
                return true;
            var matrix = (Matrix) other;
            if (GetColumnLenght() != matrix.GetColumnLenght() || GetRowLenght() != matrix.GetRowLenght())
                return false;
            for (var i = 0; i < GetRowLenght(); i++)
            {
                for (var j = 0; j < GetColumnLenght(); j++)
                {
                    if (Math.Abs(_matrix[i][j] - matrix.GetMatrix()[i][j]) > Tolerance)
                        return false;
                }
            }

            return true;
        }

        private static int RandomValue(int maxGenerateNumber)
        {
            return new Random().Next(1, maxGenerateNumber);
        }
    }
}