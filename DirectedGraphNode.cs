using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab2Graph
{
    [Serializable]
    public class DirectedGraphNode<T> : GraphNode<T> where T :IComparable<T>
    {
        /// <summary>
        /// Возвращает или задает число входящих в узел связей (для ориентированного графа)
        /// </summary>
        public int InputPath { get; set; } = 0;

        /// <summary>
        /// Возваращает или задает число исходящий из узла связей (для ориентированного графа)
        /// </summary>
        public int OutputPath { get; set; } = 0;

        public override int Degree
        {
            set
            {
                Degree = OutputPath + InputPath;
            }
        }

        public DirectedGraphNode()
        {
            OutputPath = 0;
            InputPath = 0;
        }

        public DirectedGraphNode(bool visited, int outputPath, int inputPath, T data = default)
        {
            Visited = visited;
            OutputPath = outputPath;
            InputPath = inputPath;
            Data = data;
        }
    }
}
