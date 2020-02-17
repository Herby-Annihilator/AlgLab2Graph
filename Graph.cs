using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Graph
    {
        /// <summary>
        /// Матрица смежности
        /// </summary>
        private int[][] adjacencyMatrix;

        /// <summary>
        /// Список посещенных элементов
        /// </summary>
        private bool[] visitedElements;

        /// <summary>
        /// Здесь хранится текущий актуальный путь
        /// </summary>
        private int[] path;

        private LinkedList<int> pathList;

        /// <summary>
        /// Количество вершин графа
        /// </summary>
        private int count;

        /// <summary>
        /// Возвращает или задает количество вершин графа
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                if (value < 1)
                    count = 1;
                else if (value > 100)
                    count = 100;
                else
                    count = value;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="size">количесво вершин графа</param>
        public Graph(int size)
        {
            Count = size;
            adjacencyMatrix = new int[Count][];
            for (int i = 0; i < Count; i++)
            {
                adjacencyMatrix[i] = new int[Count];
            }
            path = new int[Count];
            visitedElements = new bool[Count];
            for (int i = 0; i < Count; i++)
            {
                visitedElements[i] = false;
            }
            pathList = new LinkedList<int>();
        }

        /// <summary>
        /// Указывает, существует ли в данном графе Гамильтонов путь
        /// </summary>
        /// <returns></returns>
        public bool HamiltonPath()
        {
            for (int i = 0; i < Count; i++)
                if (HamiltonPath(i) == true)
                {
                    return true;
                }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentElement">должен совпадать и индексом элемента в матрице смежности по строкам</param>
        /// <returns></returns>
        private bool HamiltonPath(int currentElement)
        {
            pathList.AddLast(currentElement);
            visitedElements[currentElement] = true;
            if (pathList.Count == Count)
            {
                return true;
            }
            //
            // проходим по соседям текущего элемента, они расположены в одной строке с ним
            //
            for (int i = 0; i < Count; i++)
            {
                if (adjacencyMatrix[currentElement][i] == 1 && visitedElements[i] == false)  // значит этот элемент еще ни разу не посещался
                {
                    if (HamiltonPath(i) == true)
                    {
                        return true;
                    }
                    else
                    {
                        //pathList.RemoveLast();
                        visitedElements[i] = false;
                        //return false;
                    }
                }
            }
            pathList.RemoveLast();
            visitedElements[currentElement] = false;
            return false;
        }

        public void CreateAdjacencyMatrix()
        {
            
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    if (i == j)
                    {
                        adjacencyMatrix[i][j] = 0;
                    }
                    else
                    {
                        Random random = new Random();
                        int number = random.Next(0, 50);
                        if (number % 2 == 0)
                        {
                            adjacencyMatrix[i][j] = 1;
                        }
                        else
                        {
                            adjacencyMatrix[i][j] = 0;
                        }
                        //adjacencyMatrix[i][j] = number;
                    }
                }
            }
        }

        public void CreateAdjacencyMatrix(int[][] matrix)
        {
            adjacencyMatrix = matrix;
        }

        public void Print(TextWriter stream = null)
        {
            TextWriter oldStream = Console.Out;
            if (stream != null)
            {
                Console.SetOut(stream);
            }           
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    Console.Write(adjacencyMatrix[i][j] + " ");
                }
                Console.WriteLine();
            }
            if (stream != null)
            {
                Console.SetOut(oldStream);
            }
        }
    }
}
