SELECT t.Id, t.PassengerFirstName, t.PassengerSecondName, t.CIF, t.BookingDateTime, 
       u.FirstName AS UserFirstName, u.SecondName AS UserSecondName, u.Email,
       j.Code AS JourneyCode, j.Name AS JourneyName, 
       sc.Name AS StarcraftName, sc.Description AS StarcraftDescription,
       ss.Name AS StartStarportName, ss.Location AS StartStarportLocation,
       es.Name AS EndStarportName, es.Location AS EndStarportLocation
FROM Tickets t
INNER JOIN Users u ON t.UserId = u.Id
INNER JOIN Journeys j ON t.JourneyId = j.Id
INNER JOIN Starcrafts sc ON j.StarcraftId = sc.Id
INNER JOIN Starports ss ON t.StartStarportId = ss.Id
INNER JOIN Starports es ON t.EndStarportId = es.Id
