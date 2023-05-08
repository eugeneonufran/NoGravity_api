using NoGravity.Data.Tables;

namespace NoGravity.Data
{
    public static class RouteFinder
    {
        public static List<List<JourneySegment>> FindAllPaths(List<JourneySegment> segments, int departureStarportId, int arrivalStarportId)

        {

            var allPaths = new List<List<JourneySegment>>();

            var currentPath = new List<JourneySegment>();

            var visitedSegments = new HashSet<JourneySegment>();

            DFS(segments, departureStarportId, arrivalStarportId, visitedSegments, currentPath, allPaths);

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
        private static void DFS(List<JourneySegment> segments, int currentStarportId, int arrivalStarportId, HashSet<JourneySegment> visitedSegments, List<JourneySegment> currentPath, List<List<JourneySegment>> allPaths)

        {

            if (currentStarportId == arrivalStarportId)

            {

                allPaths.Add(new List<JourneySegment>(currentPath));

                return;

            }

            foreach (var segment in segments)

            {

                if (!visitedSegments.Contains(segment) && segment.DepartureStarportId == currentStarportId)

                {

                    visitedSegments.Add(segment);

                    currentPath.Add(segment);

                    DFS(segments, segment.ArrivalStarportId, arrivalStarportId, visitedSegments, currentPath, allPaths);

                    visitedSegments.Remove(segment);

                    currentPath.Remove(segment);

                }

            }

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
