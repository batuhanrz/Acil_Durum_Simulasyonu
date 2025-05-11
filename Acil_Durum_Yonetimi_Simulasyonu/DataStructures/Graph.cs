using System;
using System.Collections.Generic;

namespace Acil_Durum_Yonetimi_Simulasyonu.DataStructures
{
    public class GraphNode<T>
    {
        public T Data { get; }
        public List<GraphNode<T>> Neighbors { get; }

        public GraphNode(T data)
        {
            Data = data;
            Neighbors = new List<GraphNode<T>>();
        }
    }

    public class Graph<T>
    {
        private readonly Dictionary<T, GraphNode<T>> _nodes;

        public event Action GraphChanged;

        public Graph()
        {
            _nodes = new Dictionary<T, GraphNode<T>>();
        }

        public void AddNode(T data)
        {
            if (!_nodes.ContainsKey(data))
            {
                _nodes[data] = new GraphNode<T>(data);
                OnGraphChanged();
            }
        }

        public void AddEdge(T source, T destination, bool bidirectional = false)
        {
            if (!_nodes.ContainsKey(source) || !_nodes.ContainsKey(destination))
            {
                Console.WriteLine($"Kaynak veya hedef düğüm bulunamadı: {source}, {destination}");
                return;
            }

            GraphNode<T> sourceNode = _nodes[source];
            GraphNode<T> destinationNode = _nodes[destination];

            if (!sourceNode.Neighbors.Contains(destinationNode))
            {
                sourceNode.Neighbors.Add(destinationNode);
            }

            if (bidirectional && !destinationNode.Neighbors.Contains(sourceNode))
            {
                destinationNode.Neighbors.Add(sourceNode);
            }

            OnGraphChanged();
        }

        public void Clear()
        {
            _nodes.Clear();
            OnGraphChanged();
        }

        private void OnGraphChanged()
        {
            GraphChanged?.Invoke();
        }

        public IEnumerable<GraphNode<T>> GetAllNodes()
        {
            return _nodes.Values;
        }

        public bool Contains(T data)
        {
            return _nodes.ContainsKey(data);
        }

        public GraphNode<T> GetNode(T data)
        {
            return _nodes.TryGetValue(data, out GraphNode<T> node) ? node : null;
        }
    }
}
