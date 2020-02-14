using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab2Graph
{
    [Serializable]
    public class GraphNode<T> where T: IComparable<T>
    {
        /// <summary>
        /// Поле данных
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Возвращает или задает флаг посещения данного узла
        /// false - узел еще не посещался
        /// true - узел уже был посещен
        /// </summary>
        public bool Visited { get; set; } = false;

        /// <summary>
        /// Взвращает или задает степень данного узла (количество связей)
        /// </summary>
        public virtual int Degree { get; set; }

        public GraphNode()
        {
            Data = default(T);
            Visited = false;
            Degree = 0;
        }

        public GraphNode(bool visited, int degree, T data = default)
        {
            Visited = visited;
            Degree = degree;
            Data = data;
        }
    }
}
