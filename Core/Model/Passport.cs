namespace Core.Model;

public class Passport(
    string passportNumber,
    string passportSeries,
    DateTime issueDate,
    string surname,
    string name,
    string patronymic,
    string gender,
    string nationality)
{
    public string PassportNumber { get; set; } = passportNumber;
    public string PassportSeries { get; set; } = passportSeries;
    public DateTime IssueDate { get; set; } = issueDate;
    public string Surname { get; set; } = surname;
    public string Name { get; set; } = name;
    public string Patronymic { get; set; } = patronymic;
    public string Gender { get; set; } = gender;
    public string Nationality { get; set; } = nationality;
}