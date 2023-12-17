namespace ConsoleApp1;

public class NotebookEntry // запись
{
    public NotebookEntry(string name, string surname, string phoneNumber, string email)
    {
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public string Name { get; init; }
    public string Surname { get; init; }
    public string PhoneNumber { get; init; }
    public string Email { get; init; }
}