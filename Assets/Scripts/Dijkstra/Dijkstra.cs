using System;
using System.Collections.Generic;
using System.Linq;

public static class Dijkstra<T> where T : notnull
{
    public static IEnumerable<T> ShortestPath(IWeightedGraph<T> graph, T start, T end)
    {
        var distances = new Dictionary<T, double>();
        var previous = new Dictionary<T, T>();
        var queue = new PriorityQueue<T>();
        var visited = new HashSet<T>();

        distances[start] = 0;
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Equals(end))
            {
                return GetPath(previous, end);
            }

            visited.Add(current);

            foreach (var neighbor in graph.Neighbors(current))
            {
                if (visited.Contains(neighbor)) continue;

                var tentativeDistance = distances[current] + graph.Weight(current, neighbor);

                if (!distances.ContainsKey(neighbor) || tentativeDistance < distances[neighbor])
                {
                    distances[neighbor] = tentativeDistance;
                    previous[neighbor] = current;
                    queue.Enqueue(neighbor, tentativeDistance);
                }
            }
        }

        return new List<T>();
    }

    private static IEnumerable<T> GetPath(Dictionary<T, T> previous, T end)
    {
        var path = new List<T> { end };
        while (previous.ContainsKey(end))
        {
            end = previous[end];
            path.Add(end);
        }
        path.Reverse();
        return path;
    }
}

public class PriorityQueue<T> where T : notnull
{
    private readonly SortedDictionary<double, Queue<T>> _queues = new SortedDictionary<double, Queue<T>>();

    public int Count { get; private set; }

    public void Enqueue(T item, double priority)
    {
        if (!_queues.ContainsKey(priority))
        {
            _queues.Add(priority, new Queue<T>());
        }
        _queues[priority].Enqueue(item);
        Count++;
    }

    public T Dequeue()
    {
        var priority = _queues.Keys.First();
        var queue = _queues[priority];
        var item = queue.Dequeue();
        if (queue.Count == 0)
        {
            _queues.Remove(priority);
        }
        Count--;
        return item;
    }
}
