using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            bool newMatrix = true;
            do
            {
                Graph graph = new Graph(5);
                //if (newMatrix)
                //{
                //    graph.CreateAdjacencyMatrix();
                //    newMatrix = false;
                //}
                int[][] matrix = new int[5][];
                for (int i = 0; i < 5; i++)
                {
                    matrix[i] = new int[5];
                }

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        matrix[i][j] = Convert.ToInt32(Console.ReadLine());
                    }
                    Console.WriteLine("====");
                }
                graph.CreateAdjacencyMatrix(matrix);
                graph.Print();

                graph.HamiltonPath();
            } while (Console.ReadKey().KeyChar != 27);
        }
    }
}
