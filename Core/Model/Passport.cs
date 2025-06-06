using System.Text.RegularExpressions;

namespace Core.Model;

public class Passport
{
    private string _gender;
    private DateTime _issueDate;
    private string _name;
    private string _nationality;
    private string _passportNumber;
    private string _passportSeries;
    private string _patronymic;

    private string _surname;

    public Passport(
        string passportSeries,
        string passportNumber,
        DateTime issueDate,
        string surname,
        string name,
        string patronymic,
        string gender,
        string nationality)
    {
        PassportNumber = passportNumber;
        PassportSeries = passportSeries;
        IssueDate = issueDate;
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Gender = gender;
        Nationality = nationality;
    }

    public string PassportNumber
    {
        get => _passportNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                value.Length != 6)
                throw new ArgumentException($"Номер паспорта должен быть 6-значным числом. {value}");
            _passportNumber = value;
        }
    }

    public string PassportSeries
    {
        get => _passportSeries;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                value.Length != 4)
                throw new ArgumentException("Серия паспорта должна быть 4-значным числом.");
            _passportSeries = value;
        }
    }

    public DateTime IssueDate
    {
        get => _issueDate;
        set
        {
            _issueDate = value;
        }
    }

    public string Surname
    {
        get => _surname;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 50)
                throw new ArgumentException("Фамилия должна быть от 1 до 50 символов.");
            _surname = value;
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 50)
                throw new ArgumentException("Имя должно быть от 1 до 50 символов.");
            _name = value;
        }
    }

    public string Patronymic
    {
        get => _patronymic;
        set
        {
            if (value != null && value.Length > 50)
                throw new ArgumentException("Отчество должно быть до 50 символов.");
            _patronymic = value;
        }
    }

    public string FullName => Surname + " " + Name + " " + Patronymic;
    public string FormattedName => Surname + " " + Name[0] + ".";

    public string Gender
    {
        get => _gender;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 10)
                throw new ArgumentException("Пол должен быть от 1 до 10 символов.");
            _gender = value;
        }
    }

    public string Nationality
    {
        get => _nationality;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 50)
                throw new ArgumentException("Гражданство должно быть от 1 до 50 символов.");
            _nationality = value;
        }
    }
}