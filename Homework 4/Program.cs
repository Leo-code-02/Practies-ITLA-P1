using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Progr1_tarea_4
    //Leandro Leguisamo Garcia 2024-2580
{
    internal class Program
    {
        public class Contact 
        { 
            public int Id { get; set; }
            public string Name { get; set; }    
            public string LastName { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
            public bool IsFavorite { get; set; }

        }

        public class ContactManager
        {
            private List<Contact> contacts = new List<Contact>();

            public bool AddContact()
            {
                Console.WriteLine("\n=== ADD NEW CONTACT ===");

                Contact newContact = new Contact();
                newContact.Id = contacts.Count > 0 ? contacts[contacts.Count - 1].Id + 1 : 1;

                Console.Write("Enter Name: ");
                newContact.Name = Console.ReadLine();

                Console.Write("Enter Last Name: ");
                newContact.LastName = Console.ReadLine();

                Console.Write("Enter Address: ");
                newContact.Address = Console.ReadLine();

                Console.Write("Enter Phone: ");
                newContact.Phone = Console.ReadLine();

                Console.Write("Enter Email: ");
                newContact.Email = Console.ReadLine();

                Console.Write("Enter Age: ");
                int age;

                while (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.Write("Please enter a valid number for Age: ");
                }
                newContact.Age = age;

                Console.Write("Is this contact your favorite? (yes/no): ");
                string favInput = Console.ReadLine().ToLower();
                newContact.IsFavorite = favInput == "yes" || favInput == "y" || favInput == "si";

                contacts.Add(newContact);
                Console.WriteLine("\nContact added successfully!\n");

                return true;
            }

            public void ListContacts()
            {
                if (contacts.Count == 0)
                {
                    Console.WriteLine("There are no contacts to show.\n");
                    return;
                }

                Console.WriteLine("\n=== CONTACT LIST ===");
                foreach (var contact in contacts)
                {
                    Console.WriteLine($"ID: {contact.Id}");
                    Console.WriteLine($"Name: {contact.Name}");
                    Console.WriteLine($"Last Name: {contact.LastName}");
                    Console.WriteLine($"Address: {contact.Address}");
                    Console.WriteLine($"Phone: {contact.Phone}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Age: {contact.Age}");
                    Console.WriteLine($"Favorite: {(contact.IsFavorite ? "Yes" : "No")}");
                    Console.WriteLine(new string('-', 30));
                }
            }

            public void SearchContact()
            {
                Console.Write("Enter a name or last name to search: ");
                string input = Console.ReadLine().ToLower();

                var results = contacts
                    .Where(c => c.Name.ToLower().Contains(input) || c.LastName.ToLower().Contains(input))
                    .ToList();

                if (results.Count == 0)
                {
                    Console.WriteLine("\nNo contact found with that name.\n");
                    return;
                }

                Console.WriteLine($"\nFound {results.Count} contact(s):");
                foreach (var contact in results)
                {
                    Console.WriteLine($"ID: {contact.Id}");
                    Console.WriteLine($"Name: {contact.Name}");
                    Console.WriteLine($"Last Name: {contact.LastName}");
                    Console.WriteLine($"Address: {contact.Address}");
                    Console.WriteLine($"Phone: {contact.Phone}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Age: {contact.Age}");
                    Console.WriteLine($"Favorite: {(contact.IsFavorite ? "Yes" : "No")}");
                    Console.WriteLine(new string('-', 30));
                }
            }

            public bool EditContact()
            {
                Console.Write("Enter the ID of the contact to edit: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID format.\n");
                    return false;
                }

                var contact = contacts.FirstOrDefault(c => c.Id == id);
                if (contact == null)
                {
                    Console.WriteLine("Contact not found.\n");
                    return false;
                }

                Console.WriteLine("Leave a field empty to keep the current value.");

                Console.Write($"Name ({contact.Name}): ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name)) contact.Name = name;

                Console.Write($"Last Name ({contact.LastName}): ");
                string lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastName)) contact.LastName = lastName;

                Console.Write($"Address ({contact.Address}): ");
                string address = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(address)) contact.Address = address;

                Console.Write($"Phone ({contact.Phone}): ");
                string phone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phone)) contact.Phone = phone;

                Console.Write($"Email ({contact.Email}): ");
                string email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email)) contact.Email = email;

                Console.Write($"Age ({contact.Age}): ");
                string ageInput = Console.ReadLine();
                if (int.TryParse(ageInput, out int age)) contact.Age = age;

                Console.Write($"Is Favorite (yes/no) ({(contact.IsFavorite ? "yes" : "no")}): ");
                string favInput = Console.ReadLine().ToLower();
                if (favInput == "yes" || favInput == "y" || favInput == "si") contact.IsFavorite = true;
                else if (favInput == "no" || favInput == "n") contact.IsFavorite = false;

                Console.WriteLine("Contact updated successfully!\n");

                return true;
            }

            public bool DeleteContact()
            {
                Console.Write("Enter the ID of the contact to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID format.\n");
                    return false;
                }

                var contact = contacts.FirstOrDefault(c => c.Id == id);
                if (contact == null)
                {
                    Console.WriteLine("Contact not found.\n");
                    return false;
                }

                Console.WriteLine($"Are you sure you want to delete {contact.Name} {contact.LastName}? (yes/no): ");
                string confirm = Console.ReadLine().ToLower();

                if (confirm == "yes" || confirm == "y" || confirm == "si")
                {
                    contacts.Remove(contact);
                    Console.WriteLine("Contact deleted successfully!\n");
                    return true;
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.\n");
                    return false;
                }
            }

            public void LoadContactsFromFile(string filePath)
            {
                if (!File.Exists(filePath)) return;

                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 8)
                    {
                        Contact contact = new Contact
                        {
                            Id = int.Parse(parts[0]),
                            Name = parts[1],
                            LastName = parts[2],
                            Address = parts[3],
                            Phone = parts[4],
                            Email = parts[5],
                            Age = int.Parse(parts[6]),
                            IsFavorite = bool.Parse(parts[7])
                        };

                        contacts.Add(contact);
                    }
                }
            }

            public void SaveContactsToFile(string filePath)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var contact in contacts)
                    {
                        string line = $"{contact.Id}|{contact.Name}|{contact.LastName}|{contact.Address}|{contact.Phone}|{contact.Email}|{contact.Age}|{contact.IsFavorite}";
                        writer.WriteLine(line);
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            string filePath = "contacts.txt";
            ContactManager manager = new ContactManager();
            bool wasModifile = false;

            manager.LoadContactsFromFile(filePath);


            while (true) {

                Console.WriteLine("Welcome to my contact list. :) \n");
                Console.WriteLine(@"1. Add contact    2. List contact    3. Search contact    4. Edit contact    5. Delete contact    6. Exit ");
                Console.Write("\n");
                Console.WriteLine("Choose an option please: ");

                String input = Console.ReadLine();
                Console.Clear();

                if(!int.TryParse(input, out int option) || option < 1 || option > 6)
                {
                    Console.WriteLine("Invalid option, please enter a number between 1 and 6.\n");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        { if (manager.AddContact()) wasModifile = true; }
                        break;
                    case 2:
                        { manager.ListContacts(); }
                        break;
                    case 3:
                        { manager.SearchContact(); }
                        break;
                    case 4:
                        { if (manager.EditContact()) wasModifile = true; }
                        break;
                    case 5:
                        { if (manager.DeleteContact()) wasModifile = true; }
                        break;
                    case 6:
                        if (wasModifile)
                        {
                            manager.SaveContactsToFile(filePath);
                            Console.WriteLine("Contacts saved. Thanks for using the Contact List. Goodbye!");
                        }
                        else
                        {
                            Console.WriteLine("No changes made to contacts. Goodbye!");  
                        }
                        return;
                    default:
                        Console.WriteLine("Invalid option, please enter a number between 1 and 6.\n");
                        break;
                }
            }
        }
    }
}
