using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryForAlgLab;
using System.IO;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            bool goOut = false;
            Graph graph = null;
            do
            {
                goOut = false;
                switch(Subroutines.PrintMenu())
                {
                    //
                    //  c - созадть граф (ручками)
                    //
                    case 'c':
                        {
                            int size;
                            do
                            {
                                do
                                {
                                    Console.WriteLine("\nУкажите количество элементов в графе (не более 10) ");
                                } while (!Int32.TryParse(Console.ReadLine(), out size));
                            } while (size < 1 || size > 10);

                            graph = new Graph(size);
                            int[][] matrix = new int[size][];
                            for (int i = 0; i < size; i++)
                            {
                                matrix[i] = new int[size];
                            }
                            //
                            // заполняем
                            //
                            for (int i = 0; i < size; i++)
                            {
                                for (int j = 0; j < size; j++)
                                {
                                    int number;
                                    do
                                    {
                                        do
                                        {
                                            Console.WriteLine("\nМатрица[" + i + "][" + j + "] = ");
                                        } while (!Int32.TryParse(Console.ReadLine(), out number));
                                    } while (number > 1 || number < 0);
                                    matrix[i][j] = number;
                                }
                            }
                            //
                            // создаем
                            //
                            graph.CreateAdjacencyMatrix(matrix);
                            //
                            // сохраняем в файл
                            //
                            StreamWriter writer = new StreamWriter("input.dat");
                            if (writer == null)
                            {
                                Console.WriteLine("Граф не был записан в файл. Нажмите что-нибудь...");
                                Console.ReadKey(true);
                                break;
                            }
                            graph.Print(writer);
                            writer.Close();
                            Console.WriteLine("Граф успешно создан. Нажмите что-нибудь...");
                            Console.ReadKey(true);
                            break;
                        }
                    //
                    //  r - восстановить граф из файла (рекомендуется)
                    //
                    case 'r':
                        {
                            try
                            {
                                int[][] matrix = GraphHelper.GetMatrixFromFile("input.dat");
                                graph = new Graph(matrix.Length);
                                graph.CreateAdjacencyMatrix(matrix);
                                Console.WriteLine("Граф успешно восстановлен. Нажмите что-нибудь...");
                                Console.ReadKey(true);
                                break;
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey(true);
                                break;
                            }

                        }
                    //
                    // b - показать Гамильтонов путь
                    //
                    case 'b':
                        {
                            
                            if (graph == null)
                            {
                                Console.WriteLine("Граф не существует. Нажмите что-нибудь...");
                                Console.ReadKey(true);
                                break;
                            }

                            int[][] pairs = graph.GetArrayOfPairs();

                            int pairsCount = pairs.GetLength(0);
                            List<int>[] pathList = new List<int>[pairsCount];
                            for (int i = 0; i < pairsCount; i++)
                            {
                                pathList[i] = graph.GetMinimumPathBetweenNodes(pairs[i][0], pairs[i][1]);
                            }

                            bool isPath = false;
                            for (int i = 0; i < pairsCount; i++)
                            {
                                for (int j = i + 1; j < pairsCount; j++)
                                {
                                    if (pathList[i] != null && pathList[j] != null)
                                    {
                                        if (pathList[i][pathList[i].Count - 1] == pathList[j][pathList[j].Count - 1])
                                        {
                                            isPath = true;
                                            Console.WriteLine("\nКратчайший путь одинакового веса между парами вершин " 
                                                + pairs[i][0] + "," + pairs[i][1] + " и " + pairs[j][0] + "," + pairs[j][1]
                                                + " = " + pathList[i][pathList[i].Count - 1]);
                                            Console.Write("\nПуть от " + pairs[i][0] + " до " + pairs[i][1] + " : ");
                                            for (int k = 0; k < pathList[i].Count - 1; k++)
                                            {
                                                Console.Write(pathList[i][k] + " ");
                                            }

                                            Console.Write("\nПуть от " + pairs[j][0] + " до " + pairs[j][1] + " : ");
                                            for (int k = 0; k < pathList[j].Count - 1; k++)
                                            {
                                                Console.Write(pathList[j][k] + " ");
                                            }
                                        }
                                    }
                                }
                            }
                            if (isPath == false)
                            {
                                Console.WriteLine("Пути не существует...");
                                Console.ReadKey(true);
                            }
                            else
                            {
                                Console.WriteLine("\nНажмите что-нибудь...");
                                Console.ReadKey(true);
                            }
                            break;
                        }
                    //
                    // d - показать матрицу смежности
                    //
                    case 'd':
                        {
                            if (graph == null)
                            {
                                Console.WriteLine("Граф не существует. Нажмите что-нибудь...");
                                Console.ReadKey(true);
                                break;
                            }
                            graph.Print(Console.Out);
                            Console.WriteLine("\nНажмите что-нибудь...");
                            Console.ReadKey(true);
                            break;
                        }
                    case (char)27:
                        {
                            goOut = true;
                            break;
                        }
                }
            } while (goOut == false);
        }
    }
}
