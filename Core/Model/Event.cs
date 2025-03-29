namespace Core.Model;

public class Event
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int ParticipantsCount { get; set; }
    public Schedule Duration { get; set; }
    public EventType EventType { get; set; }
    public int GuardiansCount { get; set; }

    public Event(string name, string location, int participantsCount, EventType eventType, Schedule duration)
    {
        Name = name;
        Date = duration.StartDate;
        Location = location;
        ParticipantsCount = participantsCount;
        EventType = eventType;
        Duration = duration;
        GuardiansCount = CalculateGuardiansCount();
    }

    public int CalculateGuardiansCount()
    {
        return EventType switch
        {
            EventType.LowRisk => Math.Max(1, ParticipantsCount / 50),
            EventType.MediumRisk => Math.Max(2, ParticipantsCount / 30),
            EventType.HighRisk => Math.Max(4, ParticipantsCount / 20),
            _ => Math.Max(1, ParticipantsCount / 50)
        };
    }
}

public enum EventType
{
    LowRisk,
    MediumRisk,
    HighRisk
}