using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Matrix1.Program;

namespace Matrix
{
    class Program
    {
        delegate RMatrix Client(RMatrix m);
        static void Main(string[] args)
        {
            Console.WriteLine("\n\nЗадание Матрица\n\n");

            RMatrix A = new RMatrix(3);
            RMatrix B = new RMatrix(3);

            Console.WriteLine("Объекты скопированны A-in C,B-in D");
            //RMatrix C = A.DeepCopy();
            //RMatrix D = B.DeepCopy();

            string choice;//переменная ввода

            Client client = Diag;
            client(A);

            while (true)
            {
                Console.WriteLine("\n=======================\n");
                Console.WriteLine("Matrix A:");
                Console.WriteLine(A.ToString() + "\n");
                Console.WriteLine("Matrix B:");
                Console.WriteLine(B.ToString() + "\n");

                Console.Write("Меню: \n " +
                    "1 - сложить \n " +
                    "2 - вычесть \n " +
                    "3 - умножить \n " +
                    "4 - сравнение \n " +
                    "5 - детерминант А \n " +
                    "6 - детерминант B \n " +
                    "7 - обратная матрица A \n " +
                    "8 - обратная матрица B \n " +
                    "9 - использовать глубокое копирование матрицы A \n" +
                    "0 - использовать глубокое копирование матрицы B \n" +
                    "А - Введите матрицу у которой хотите узнать Хеш-Код \n" +
                    "B - Привести матрицу к диагональному виду \n" +
                    "f - ВЫХОД ИЗ ПРОГРАММЫ \n " +
                    "Ввод: ");
                choice = Console.ReadLine();

                Console.Clear();

                //проверка на выход
                if (choice == "exit")
                {
                    Console.WriteLine("\n\n\n\nВЫХОД\n\n\n\n");
                    break;
                }
                switch (choice)//действия по нажатию
                {
                    case "1":
                        Console.WriteLine((A + B).ToString());
                        break;
                    case "2":
                        Console.WriteLine((A - B).ToString());
                        break;
                    case "3":
                        Console.WriteLine((A * B).ToString());
                        break;
                    case "4":
                        Console.WriteLine(A > B);
                        break;
                    case "5":
                        Console.WriteLine(RMatrix.Determinant(A));
                        break;
                    case "6":
                        Console.WriteLine(RMatrix.Determinant(B));
                        break;
                    case "7":
                        Console.WriteLine(RMatrix.Inverse(B));
                        break;
                    case "A":
                        Console.WriteLine(A.GetHashCode());
                        break;
                    case "B":
                        RMatrix.Diag(A).ToString();
                        break;

                    default:
                        Console.WriteLine("Введено некорректное значение");
                        break;

                }
            }
            while (true)
            {

                choice = Console.ReadLine();
                Console.Clear();

                //проверка на выход
                if (choice == "exit")
                {
                    Console.WriteLine("\n\n\n\nВЫХОД\n\n\n\n");
                    break;
                }
                switch (choice)//действия по нажатию
                {


                }
            }
            Console.ReadLine();
        }
        private static RMatrix Diag(RMatrix m)
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