using System.Net;
using ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        ConsoleHelper.GetHelp();
        Console.Out.WriteLine("Для начала введите путь к файлу с контактами или введите 0, если его нет:");
        string path = Console.In.ReadLine();
        var archive = new List<NotebookEntry>();
        if (path != "0")
        {
            if (!File.Exists(path))
            {
                Console.Out.WriteLine("Нет файла с таким путем");
            }

            TextReader reader = new StreamReader(path);
            string input;
            while ((input = reader.ReadLine()) != null && input != "")
            {
                string name = "";
                int i = 0;
                while (input[i] != ',')
                {
                    name += input[i];
                    i++;
                }
                i++;

                string surname = "";
                while (input[i] != ',')
                {
                    surname += input[i];
                    i++;
                }
                i++;

                string phoneNumber = "";
                while (input[i] != ',')
                {
                    phoneNumber += input[i];
                    i++;
                }
                i++;

                string email = "";
                while (i < input.Length)
                {
                    email += input[i];
                    i++;
                }

                archive.Add(new NotebookEntry(name, surname, phoneNumber, email));
            }
        }

        var notebook = new Notebook(archive);

        while (true)
        {
            Console.Out.WriteLine("Введите номер команды");
            int commandId = Convert.ToInt16(Console.ReadLine());
            if (commandId == 5)
            {
                break;
            }

            switch (commandId)
            {
                case 1:
                    //добавление контакта
                    Console.Out.WriteLine("Введите имя:");
                    string name = Console.ReadLine();

                    Console.Out.WriteLine("Введите фамилию:");
                    string surname = Console.ReadLine();

                    Console.Out.WriteLine("Введите номер телефона:");
                    string phoneNumber = Console.ReadLine();

                    Console.Out.WriteLine("Введите электронную почту:");
                    string email = Console.ReadLine();

                    notebook.AddContact(name, surname, phoneNumber, email);
                    Console.Out.WriteLine();
                    break;

                case 2:
                    // нахождение контакта

                    Console.Out.WriteLine("Чтобы найти контакт по имени, нажмите 1"); //справка
                    Console.Out.WriteLine("По фамилии, нажмите 2");
                    Console.Out.WriteLine("По телефону, нажмите 3");
                    Console.Out.WriteLine("По почте, нажмите 4");
                    Console.Out.WriteLine("По всем признакам, нажмите 5");
                    
                    int searchBy = Convert.ToInt16(Console.ReadLine());
                    NotebookEntry? found = null;
                    if (searchBy == 1)
                    {
                        Console.Out.WriteLine("Введите имя:");
                        string contactName = Console.In.ReadLine();
                        found = notebook.FindContactByName(contactName);
                    }
                    else if (searchBy == 2)
                    {
                        Console.Out.WriteLine("Введите фамилию:");
                        string contactSurname = Console.In.ReadLine();
                        found = notebook.FindContactBySurname(contactSurname);
                    }
                    else if (searchBy == 3)
                    {
                        Console.Out.WriteLine("Введите номер телефона:");
                        string contactPhoneNumber = Console.In.ReadLine();
                        found = notebook.FindContactByPhoneNumber(contactPhoneNumber);
                    }
                    else if (searchBy == 4)
                    {
                        Console.Out.WriteLine("Введите электронную почту:");
                        string contactEmail = Console.In.ReadLine();
                        found = notebook.FindContactByEmail(contactEmail);
                    }
                    else if (searchBy == 5)
                    {
                        Console.Out.WriteLine("Введите имя:");
                        string contactName = Console.In.ReadLine();
                        Console.Out.WriteLine("Введите фамилию:");
                        string contactSurname = Console.In.ReadLine();
                        Console.Out.WriteLine("Введите номер телефона:");
                        string contactPhoneNumber = Console.In.ReadLine();
                        Console.Out.WriteLine("Введите электронную почту:");
                        string contactEmail = Console.In.ReadLine();

                        found = notebook.FindContact(contactName, contactSurname, contactPhoneNumber, contactEmail);
                    }

                    if (found is not null)
                    {
                        ConsoleHelper.ShowContact(found);
                    }
                    else
                    {
                        Console.Out.WriteLine("Не существует такого контакта");
                    }

                    break;

                case 3:
                    // отобразить все контакты
                    foreach (var entry in notebook.Archive)
                    {
                        ConsoleHelper.ShowContact(entry);
                    }
                    break;

                case 4:
                    // сохранение в файл
                    Console.Out.WriteLine("Для начала введите путь к файлу, в который вы хотите сохранить базу:");
                    path = Console.In.ReadLine();
                    StreamWriter writetext = new StreamWriter(File.Create(path));

                    foreach (var entry in notebook.Archive)
                    {
                        writetext.WriteLine(entry.Name + "," + entry.Surname + "," + entry.PhoneNumber + "," + entry.Email);
                    }
                    writetext.Flush();
                    break;
            }
        }

    }
}

