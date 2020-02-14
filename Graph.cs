using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab2Graph
{
    [Serializable]
    public class Graph<T> where T: IComparable<T>
    {
        /// <summary>
        /// Матрица смежности
        /// </summary>
        private readonly GraphNode<T>[][] adjacencyMatrix;

        /// <summary>
        /// Возвращает матрицу смежности
        /// </summary>
        public GraphNode<T>[][] AdjacencyMatrix
        {
            get
            {
                return adjacencyMatrix;
            }
            private set
            {

            }
        }
        /// <summary>
        /// Направленность графа
        /// </summary>
        public bool Direction { get; }
        public Graph()
        {
            Direction = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plenty">Множество вершин</param>
        /// <param name="plentyPairs">Связи между этими вершинами</param>
        public Graph(T[] plenty, Pairs<T>[] plentyPairs)
        {
            int adjacencyMatrixSize = plenty.Length;

        }
    }


}
