using NoGravity.Data.Tables;

namespace NoGravity.Data
{
    public static class RouteFinder
    {
        public static List<List<JourneySegment>> FindAllPaths(List<JourneySegment> segments, int departureStarportId, int arrivalStarportId, DateTime departureDate)
        {
            var allPaths = new List<List<JourneySegment>>();
            var currentPath = new List<JourneySegment>();
            var visitedSegments = new HashSet<JourneySegment>();

            // Find the segments with the specified departure date and start the traversal from there
            var startingSegments = segments.Where(s => s.DepartureDateTime.Date == departureDate.Date && s.DepartureStarportId == departureStarportId);

            foreach (var segment in startingSegments)
            {
                visitedSegments.Clear();
                currentPath.Clear();

                DFS(segments, segment, arrivalStarportId, visitedSegments, currentPath, allPaths);
            }

            return allPaths;
        }

        public static List<List<JourneySegment>> SortPathsByPrice(List<List<JourneySegment>> allPaths)
        {
            return allPaths.OrderBy(path => CalculateTotalPrice(path)).ToList();
        }

        public static List<List<JourneySegment>> SortPathsByTime(List<List<JourneySegment>> allPaths)
        {
            return allPaths.OrderBy(path => CalculateTotalTime(path)).ToList();
        }

        public static List<List<JourneySegment>> SortPathsByOptimal(List<List<JourneySegment>> allPaths)
        {
            return allPaths.OrderBy(path =>
            {
                decimal totalPrice = CalculateTotalPrice(path);
                TimeSpan totalTime = CalculateTotalTime(path);
                return totalPrice + (decimal)totalTime.TotalMinutes;
            }).ToList();
        }
        private static void DFS(List<JourneySegment> segments, JourneySegment currentSegment, int arrivalStarportId, HashSet<JourneySegment> visitedSegments, List<JourneySegment> currentPath, List<List<JourneySegment>> allPaths)
        {
            visitedSegments.Add(currentSegment);
            currentPath.Add(currentSegment);

            if (currentSegment.ArrivalStarportId == arrivalStarportId)
            {
                allPaths.Add(new List<JourneySegment>(currentPath));
            }
            else
            {
                var nextSegments = segments.Where(s => !visitedSegments.Contains(s) && s.DepartureDateTime > currentSegment.ArrivalDateTime && s.DepartureStarportId == currentSegment.ArrivalStarportId);

                foreach (var nextSegment in nextSegments)
                {
                    DFS(segments, nextSegment, arrivalStarportId, visitedSegments, currentPath, allPaths);
                }
            }

            visitedSegments.Remove(currentSegment);
            currentPath.Remove(currentSegment);
        }

        private static TimeSpan CalculateTotalTime(List<JourneySegment> path)
        {
            TimeSpan totalDuration = TimeSpan.Zero;
            TimeSpan idleTime = TimeSpan.Zero;

            for (int i = 0; i < path.Count; i++)
            {
                if (i > 0)
                {
                    idleTime = path[i].DepartureDateTime - path[i - 1].DepartureDateTime;
                    if (idleTime > TimeSpan.Zero)
                    {
                        totalDuration += idleTime;
                    }
                }

                totalDuration += path[i].ArrivalDateTime - path[i].DepartureDateTime;
            }

            return totalDuration;
        }

        private static decimal CalculateTotalPrice(List<JourneySegment> path)

        {

            decimal totalPrice = 0;

            foreach (var segment in path)

            {

                totalPrice += segment.Price;

            }

            return totalPrice;

        }


    }

}
