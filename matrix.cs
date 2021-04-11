using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Matrix1
{
    class Program
    {
        public class RMatrix
        {
            private int nRows;
            private int nCols;
            private double[,] matrix;

            public RMatrix(int nRows, int nCols)
            {
                this.nRows = nRows;
                this.nCols = nCols;
                this.matrix = new double[nRows, nCols];
                for (int i = 0; i < nRows; i++)
                {
                    for (int j = 0; j < nCols; j++)
                    {
                        matrix[i, j] = 0.0;
                    }
                }
            }
            public RMatrix(int nRows)
            {
                this.nRows = nRows;
                this.nCols = nRows;
                this.matrix = new double[nRows, nRows];
                Random rnd = new Random();
                for (int i = 0; i < nRows; i++)
                {
                    for (int j = 0; j < nRows; j++)
                    {
                        matrix[i, j] = rnd.Next(1, 10);
                    }
                }
            }

            public RMatrix DeepCopy()
            {
                RMatrix clone = (RMatrix)this.MemberwiseClone();
                clone.nCols = this.nCols;
                clone.nRows = this.nRows;
                clone.matrix = this.matrix;
                return clone;
            }

            public RMatrix(double[,] matrix)
            {
                this.nRows = matrix.GetLength(0);
                this.nCols = matrix.GetLength(1);
                this.matrix = matrix;
            }


            public RMatrix(RMatrix m)
            {
                nRows = m.GetnRows;
                nCols = m.GetnCols;
                matrix = m.matrix;
            }

            public double this[int m, int n]
            {
                get
                {
                    if (m < 0 || m > nRows)
                    {
                        throw new Exception("м-й ряд вне зоны досягаемости!");
                    }
                    if (n < 0 || n > nCols)
                    {
                        throw new Exception("м-й cтрока вне зоны досягаемости!");
                    }
                    return matrix[m, n];
                }
                set { matrix[m, n] = value; }
            }

            public int GetnRows
            {
                get { return nRows; }
            }

            public int GetnCols
            {
                get { return nCols; }
            }

            public override string ToString()
            {
                string strMatrix = "(";
                for (int i = 0; i < nRows; i++)
                {
                    string str = "";
                    for (int j = 0; j < nCols - 1; j++)
                    {
                        str += matrix[i, j].ToString() + ", ";
                    }
                    str += matrix[i, nCols - 1].ToString();
                    if (i != nRows - 1 && i == 0)
                        strMatrix += str + "\n";
                    else if (i != nRows - 1 && i != 0)
                        strMatrix += " " + str + "\n";
                    else
                        strMatrix += " " + str + ")";
                }
                return strMatrix;
            }

            public override bool Equals(object obj)
            {
                return (obj is RMatrix) && this.Equals((RMatrix)obj);
            }

            public bool Equals(RMatrix m)
            {
                return matrix == m.matrix;
            }

            public override int GetHashCode()
            {
                return matrix.GetHashCode();
            }

            public static RMatrix operator +(RMatrix m1, RMatrix m2)
            {
                if (!RMatrix.CompareDimension(m1, m2))
                {
                    throw new Exception("Размеры двух матриц должны быть одинаковыми!");
                }
                RMatrix result = new RMatrix(m1.GetnRows, m1.GetnCols);
                for (int i = 0; i < m1.GetnRows; i++)
                {
                    for (int j = 0; j < m1.GetnCols; j++)
                    {
                        result[i, j] = m1[i, j] + m2[i, j];
                    }
                }
                return result;
            }

            public static RMatrix operator -(RMatrix m)
            {
                for (int i = 0; i < m.GetnRows; i++)
                {
                    for (int j = 0; j < m.GetnCols; j++)
                    {
                        m[i, j] = -m[i, j];
                    }
                }
                return m;
            }

            public static RMatrix operator -(RMatrix m1, RMatrix m2)
            {
                if (!RMatrix.CompareDimension(m1, m2))
                {
                    throw new Exception("Размеры двух матриц должны быть одинаковыми!");
                }
                RMatrix result = new RMatrix(m1.GetnRows, m1.GetnCols);
                for (int i = 0; i < m1.GetnRows; i++)
                {
                    for (int j = 0; j < m1.GetnCols; j++)
                    {
                        result[i, j] = m1[i, j] - m2[i, j];
                    }
                }
                return result;
            }

            public static RMatrix operator *(RMatrix m, double d)
            {
                RMatrix result = new RMatrix(m.GetnRows, m.GetnCols);
                for (int i = 0; i < m.GetnRows; i++)
                {
                    for (int j = 0; j < m.GetnCols; j++)
                    {
                        result[i, j] = m[i, j] * d;
                    }
                }
                return result;
            }

            public static RMatrix operator *(double d, RMatrix m)
            {
                RMatrix result = new RMatrix(m.GetnRows, m.GetnCols);
                for (int i = 0; i < m.GetnRows; i++)
                {
                    for (int j = 0; j < m.GetnCols; j++)
                    {
                        result[i, j] = m[i, j] * d;
                    }
                }
                return result;
            }

            public static RMatrix operator *(RMatrix m1, RMatrix m2)
            {
                if (m1.GetnCols != m2.GetnRows)
                {
                    throw new Exception("Номера столбцов таблицы" +
                     " первая матрица должна быть равна числу " +
                     " - строки второй матрицы!");
                }
                double tmp;
                RMatrix result = new RMatrix(m1.GetnRows, m2.GetnCols);
                for (int i = 0; i < m1.GetnRows; i++)
                {
                    for (int j = 0; j < m2.GetnCols; j++)
                    {
                        tmp = result[i, j];
                        for (int k = 0; k < result.GetnRows; k++)
                        {
                            tmp += m1[i, k] * m2[k, j];
                        }
                        result[i, j] = tmp;
                    }
                }
                return result;
            }

            public bool IsSquared()
            {
                if (nRows == nCols)
                    return true;
                else
                    return false;
            }

            public static bool CompareDimension(RMatrix m1, RMatrix m2)
            {
                if (m1.GetnRows == m2.GetnRows && m1.GetnCols == m2.GetnCols)
                    return true;
                else
                    return false;
            }
            public static bool operator ==(RMatrix left, RMatrix right) => EqualityComparer<RMatrix>.Default.Equals(left, right);

            public static bool operator !=(RMatrix left, RMatrix right) => !(left == right);

            public static implicit operator double(RMatrix v)
            {
                throw new NotImplementedException();
            }

            public static double Determinant(RMatrix mat)
            {
                double result = 0.0;
                if (!mat.IsSquared())
                {
                    throw new Exception("The matrix must be squared!");
                }
                if (mat.GetnRows == 1)
                    result = mat[0, 0];
                else
                {
                    for (int i = 0; i < mat.GetnRows; i++)
                    {
                        result += Math.Pow(-1, i) * mat[0, i] * Determinant(RMatrix.Minor(mat, 0, i));
                    }
                }
                return result;
            }

            public static RMatrix Minor(RMatrix mat, int row, int col)
            {
                RMatrix mm = new RMatrix(mat.GetnRows - 1, mat.GetnCols - 1);
                int ii = 0, jj = 0;
                for (int i = 0; i < mat.GetnRows; i++)
                {
                    if (i == row)
                        continue;
                    jj = 0;
                    for (int j = 0; j < mat.GetnCols; j++)
                    {
                        if (j == col)
                            continue;
                        mm[ii, jj] = mat[i, j];
                        jj++;
                    }
                    ii++;
                }
                return mm;
            }
            public static RMatrix Inverse(RMatrix m, uint round = 0)
            {
                if (m.GetnCols != m.GetnRows) throw new ArgumentException("Обратная матрица существует только для квадратных, невырожденных, матриц.");
                RMatrix matrix = new RMatrix(m.GetnRows, m.GetnCols); //Делаем копию исходной матрицы
                double determinant = Determinant(m); //Находим детерминант

                if (determinant == 0) return matrix; //Если определитель == 0 - матрица вырожденная

                for (int i = 0; i < m.GetnRows; i++)
                {
                    for (int t = 0; t < m.GetnCols; t++)
                    {
                        RMatrix tmp = RMatrix.Minor(m, i, t);  //получаем матрицу без строки i и столбца t
                                                               //(1 / determinant) * Determinant(tmp) - формула поределения элемента обратной матрицы
                        matrix[t, i] = round == 0 ? (1 / determinant) * Math.Pow(-1, i + t) * Determinant(tmp) : Math.Round(((1 / determinant) * Math.Pow(-1, i + t) * Determinant(tmp)), (int)round, MidpointRounding.ToEven);

                    }
                }
                return matrix;
            }

            public static RMatrix Trans(RMatrix m)
            {
                RMatrix matrix = new RMatrix(m.GetnRows, m.GetnCols);
                for (int i = 0; i < m.GetnCols; i++)
                {
                    for (int j = 0; j < m.GetnRows; j++)
                    {
                        matrix[i, j] = m[j, i];
                    }
                }
                return matrix;
            }

            public static double Sumdiagonal(RMatrix m)
            {
                double summa = 0;
                for (int i = 0; i < m.GetnCols; i++)
                {
                    for (int j = 0; j < m.GetnRows; j++)
                    {
                        if (i == j)
                        {
                            summa += m[i, j];
                        }
                    }
                }
                return summa;
            }

            public static RMatrix Diag(RMatrix m)
            {
                for (int k = 0; k < m.GetnCols; k++)
                    for (int i = k + 1; i < m.GetnCols; i++)
                    {
                        double q = m[i, k] / m[k, k];
                        for (int j = 0; j < m.GetnRows; j++)
                            m[i, j] -= m[k, j] * q;
                    }
                return m;
            }
        }
    }
}
