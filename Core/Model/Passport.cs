namespace Core.Model;

public class Passport
{
    public string PassportNumber { get; set; }
    public string PassportSeries { get; set; }
    public DateTime IssueDate { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public string Gender { get; set; }
    public string Nationality { get; set; }
}