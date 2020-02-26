using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryForAlgLab;

namespace Graph
{
    public class Graph : IForAlgLabs
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

        /// <summary>
        /// Выводит матрицу смежности в указанный текстовый поток (если он указан, иначе в консоль)
        /// </summary>
        /// <param name="stream"></param>
        public void Print(TextWriter stream = null)
        {
            TextWriter oldStream = Console.Out;
            if (stream != null)
            {
                Console.SetOut(stream);
            }
            Console.WriteLine();
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

        /// <summary>
        /// Заполняет граф
        /// </summary>
        /// <param name="matrix">это матрица</param>
        public void Fill(object matrix)
        {
            //CreateAdjacencyMatrix(matrix);
        }

        public void PrintHamiltonPath(TextWriter writer)
        {
            if (pathList != null)
            {
                foreach(var number in pathList)
                {
                    writer.Write(number + " ");
                }
            }
            else
            {
                writer.WriteLine("Пути нет");
            }
        }

        //
        //
        //
        // Отсюда идет лаба 3 (поиск кратчайшего пути)
        //
        //
        //

        /// <summary>
        /// Возвращает список вершин, составляющих кратчайший путь от первой вершины до второй.
        /// Если путь не существует, то вернет null.
        /// Самый последний элемент списка равен длине данного пути.
        /// First меньше Second
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public List<int> GetMinimumPathBetweenNodes(int first, int second)
        {
            List<int> path = null;
            const int VERYMUTCH = 100000;
            if (IsNodesCorrect(first, second) == false)
            {
                throw new Exception("\nНекорректные вершины\n");
            }
            else
            {
                
                int[] smallestWeights = new int[count];  // тут хранятся самые короткие пути от начальной вершины до текущей 
                for (int i = 0; i < count; i++)
                {
                    smallestWeights[i] = VERYMUTCH;
                }
                smallestWeights[first] = 0;

                for (int i = 0; i < visitedElements.Length; i++)
                {
                    visitedElements[i] = false;
                }
                visitedElements[first] = true;
                int currentNode = first;
                int smallestWeight = 0;               
                while(smallestWeight < VERYMUTCH)  // нужен цикл, пока есть соседи для просмотра, i каждый раз разное 
                {
                    int i = currentNode;
                    visitedElements[i] = true;
                    for (int j = 0; j < count; j++)  // смотрим всех соседей для i
                    {
                        //если вес пути соседа больше, чем от текущего узла + ребро
                        if (adjacencyMatrix[i][j] > 0 && smallestWeights[j] > smallestWeights[i] + adjacencyMatrix[i][j])
                        {
                            // поменять вес до соседа
                            smallestWeights[j] = smallestWeights[i] + adjacencyMatrix[i][j];
                        }
                    }
                    smallestWeight = VERYMUTCH;  // условие становки - если просмотрены все доступные вершины

                    for (int j = 0; j < count; j++)  // ищем минимум до соседа
                    {
                        if (visitedElements[j] == false && smallestWeights[j] < smallestWeight)   // если данный узел еще не посещался и расстояние до него еще не посчитано
                        {
                            // берем этот узел на рассмотрение
                            smallestWeight = smallestWeights[j];
                            currentNode = j;
                        }
                    }
                }

                // запустить процедуру подсчета пути и возврата

                if (smallestWeights[second] == VERYMUTCH)
                {
                    return null;
                }
                path = new List<int>();
                path.Add(smallestWeights[second]);
                path.Add(second);

                currentNode = second;
                while (currentNode != first)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (adjacencyMatrix[currentNode][i] > 0 && smallestWeights[currentNode] - adjacencyMatrix[currentNode][i] == smallestWeights[i])  // значит я пришел сюда из этого соседа
                        {
                            currentNode = i;
                            path.Add(i);
                        }
                    }
                }
                path.Reverse();
            }
            return path;
        }


        /// <summary>
        /// Указывает, являются ли заданные вершины корректными
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public bool IsNodesCorrect(int first, int second)
        {
            bool isCorrect = true;
            if (first == second)
            {
                isCorrect = false;
            }
            else if (first < 0 || second < 0)
            {
                isCorrect = false;
            }
            else if (first >= count || second >= count)
            {
                isCorrect = false;
            }
            return isCorrect;
        }
    }
}
