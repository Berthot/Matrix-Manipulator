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
        private readonly List<List<double>> _matrix;
        private const double Tolerance = 0.01;

        public Matrix()
        {
            _matrix = new List<List<double>>();
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

        public void AddLineList(List<double> line)
        {
            if (line.Any(value => value.GetType() != typeof(double)))
            {
                throw new Exception("Somente valores 'Double' são permetidos");
            }

            _matrix.Add(line);
        }

        public void CreateCompleteMatrix(int lineLenght, int columnLenght)
        {
            for (var i = 0; i < columnLenght; i++)
            {
                AddLineRandomValues(lineLenght);
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

                var saveLine = string.Format($"{newLine.TrimEnd(',')}\n", Environment.NewLine);
                csv.Append(saveLine);
            }

            var currentPath = Directory.GetCurrentDirectory().Replace("/bin/Debug/netcoreapp3.1", "");
            var path =
                $"{currentPath}/MatrixFiles/{csvName}.csv";
            File.WriteAllText(path, csv.ToString());
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
            FillEmptyRow(row);
            FillEmptyColumn(column);
            _matrix[row][column] = value;
        }

        private void FillEmptyColumn(int column)
        {
            while (GetColumnLenght() < column && GetColumnLenght() == 0)
            {
                for (var i = 0; i < GetRowLenght(); i++)
                {
                    _matrix[i].Add(-1.0);
                }
            }
        }

        private void FillEmptyRow(int row)
        {
            while (GetRowLenght() < row || GetRowLenght() == 0)
            {
                _matrix.Add(new List<double>(){-1.0});
            }
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