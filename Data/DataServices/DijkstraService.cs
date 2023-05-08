using NoGravity.Data.Tables;

namespace NoGravity.Data.DataServices
{
    public static class DijkstraService
    {
        public static List<JourneySegment> FindCheapestRouteDijkstra(List<JourneySegment> segments, int departureStarportId, int arrivalStarportId)

        {

            var graph = BuildGraph(segments);

            var distances = new Dictionary<int, decimal>();

            var previous = new Dictionary<int, JourneySegment>();

            var unvisited = new HashSet<int>();

            foreach (var vertex in graph.Keys)

            {

                distances[vertex] = decimal.MaxValue;

                previous[vertex] = null;

                unvisited.Add(vertex);

            }

            distances[departureStarportId] = 0;

            while (unvisited.Count > 0)

            {

                var current = GetMinimumDistanceVertex(distances, unvisited);

                unvisited.Remove(current);

                if (current == arrivalStarportId)

                {

                    break; // Reached the destination

                }

                if (graph.TryGetValue(current, out var neighbors))

                {

                    foreach (var neighbor in neighbors)

                    {

                        var altDistance = distances[current] + neighbor.Weight;

                        if (altDistance < distances[neighbor.Segment.ArrivalStarportId])

                        {

                            distances[neighbor.Segment.ArrivalStarportId] = altDistance;

                            previous[neighbor.Segment.ArrivalStarportId] = neighbor.Segment;

                        }

                    }

                }

            }

            return ReconstructRoute(previous, arrivalStarportId);

        }

        public static List<JourneySegment> FindShortestRouteDijkstra(List<JourneySegment> segments, int departureStarportId, int arrivalStarportId)

        {

            var graph = BuildGraph(segments);

            var times = new Dictionary<int, DateTime>();

            var previous = new Dictionary<int, JourneySegment>();

            var unvisited = new HashSet<int>();

            foreach (var vertex in graph.Keys)

            {

                times[vertex] = DateTime.MaxValue;

                previous[vertex] = null;

                unvisited.Add(vertex);

            }

            times[departureStarportId] = DateTime.Now;

            while (unvisited.Count > 0)

            {

                var current = GetMinimumTimeVertex(times, unvisited);

                unvisited.Remove(current);

                if (current == arrivalStarportId)

                {

                    break; // Reached the destination

                }

                if (graph.TryGetValue(current, out var neighbors))

                {

                    foreach (var neighbor in neighbors)

                    {

                        var altTime = times[current].AddHours((neighbor.Segment.ArrivalDateTime - neighbor.Segment.DepartureDateTime).TotalHours);

                        if (altTime < times[neighbor.Segment.ArrivalStarportId])

                        {

                            times[neighbor.Segment.ArrivalStarportId] = altTime;

                            previous[neighbor.Segment.ArrivalStarportId] = neighbor.Segment;

                        }

                    }

                }

            }

            return ReconstructRoute(previous, arrivalStarportId);

        }

        private static Dictionary<int, List<Edge>> BuildGraph(List<JourneySegment> segments)

        {

            var graph = new Dictionary<int, List<Edge>>();

            foreach (var segment in segments)

            {

                if (!graph.ContainsKey(segment.DepartureStarportId))

                {

                    graph[segment.DepartureStarportId] = new List<Edge>();

                }

                if (!graph.ContainsKey(segment.ArrivalStarportId))

                {

                    graph[segment.ArrivalStarportId] = new List<Edge>();

                }

                graph[segment.DepartureStarportId].Add(new Edge(segment, segment.Price));

            }

            return graph;

        }

        private static int GetMinimumDistanceVertex(Dictionary<int, decimal> distances, HashSet<int> unvisited)

        {

            var minDistance = decimal.MaxValue;

            var minVertex = -1;

            foreach (var vertex in unvisited)

            {

                if (distances[vertex] < minDistance)

                {

                    minDistance = distances[vertex];

                    minVertex = vertex;

                }

            }

            return minVertex;

        }

        private static int GetMinimumTimeVertex(Dictionary<int, DateTime> times, HashSet<int> unvisited)

        {

            var minTime = DateTime.MaxValue;

            var minVertex = -1;

            foreach (var vertex in unvisited)

            {

                if (times[vertex] < minTime)

                {

                    minTime = times[vertex];

                    minVertex = vertex;

                }

            }

            return minVertex;

        }

        private static List<JourneySegment> ReconstructRoute(Dictionary<int, JourneySegment> previous, int destination)

        {

            var routeSegments = new List<JourneySegment>();

            var current = destination;

            while (previous[current] != null)

            {

                routeSegments.Insert(0, previous[current]);

                current = previous[current].DepartureStarportId;

            }

            return routeSegments;

        }


    }


    public class Edge

    {

        public JourneySegment Segment { get; set; }

        public decimal Weight { get; set; }

        public Edge(JourneySegment segment, decimal weight)

        {

            Segment = segment;

            Weight = weight;

        }

    }

}
