namespace Progr1_tarea_3
    //Leandro Leguisamo Garcia 2024-2580
{
    internal class Program
    {
        //Ids, names, lastnames, addresses, telephones, emails, ages, bestfriend
        
        public class Contact
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string Telephone { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
            public bool BestFriend { get; set; }
        }


        static void Main(string[] args)
        {
            //List
            List<Contact> contacts = new List<Contact>();


            Console.WriteLine("Welcome to my contact list \n");

            bool Running = true;
            
            while (Running)
            {

                Console.WriteLine(@"1. Add contact     2. View Contact    3. Search Contact     4. Modify Contacto   5. Delete Contacto    6. Exit ");
                Console.Write("\n");
                Console.WriteLine("Enter the desired option");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int typeOption))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.\n");
                    continue; 
                }

                
                switch (typeOption)
                {
                    case 1:
                        { AddContact(contacts); }
                        break;
                    case 2: //extract this to a method
                        { ViewContacts(contacts); }
                        break;
                    case 3: //search
                        { SearchContact(contacts); }
                        break;
                    case 4: //modify
                        { ModifyContact(contacts); }
                        break;
                    case 5: //delete
                        { DeleteContact(contacts); }
                        break;
                    case 6:
                        Running = false;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose a number from the menu.\n");
                        break;
                }

            }

            //Methods
            static void AddContact(List<Contact> contacts)
            {
                Console.WriteLine("\n=== ADD NEW CONTACT ===");

                Contact newContact = new Contact();

                // ID automático: el siguiente número según la lista
                newContact.Id = contacts.Count > 0 ? contacts[contacts.Count - 1].Id + 1 : 1;

                Console.Write("Enter Name: ");
                newContact.Name = Console.ReadLine();

                Console.Write("Enter Last Name: ");
                newContact.LastName = Console.ReadLine();

                Console.Write("Enter Address: ");
                newContact.Address = Console.ReadLine();

                Console.Write("Enter Telephone: ");
                newContact.Telephone = Console.ReadLine();

                Console.Write("Enter Email: ");
                newContact.Email = Console.ReadLine();

                Console.Write("Enter Age: ");
                int age;

                while (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.Write("Please enter a valid number for Age: ");
                }
                newContact.Age = age;

                Console.Write("Is this your best friend? (yes/no): ");
                string bfInput = Console.ReadLine().ToLower();
                newContact.BestFriend = bfInput == "yes" || bfInput == "y" || bfInput == "si";


                contacts.Add(newContact);

                Console.WriteLine("\n Contact added successfully!\n");
            }

            static void ViewContacts(List<Contact> contacts)
            {
                Console.WriteLine("\n=== CONTACT LIST ===");

                if (contacts.Count == 0)
                {
                    Console.WriteLine("There are no contacts to display.\n");
                    return;
                }

                foreach (var contact in contacts)
                {
                    Console.WriteLine($"\nID: {contact.Id}");
                    Console.WriteLine($"Name: {contact.Name} {contact.LastName}");
                    Console.WriteLine($"Address: {contact.Address}");
                    Console.WriteLine($"Telephone: {contact.Telephone}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Age: {contact.Age}");
                    Console.WriteLine($"Best Friend: {(contact.BestFriend ? "Yes" : "No")}");
                    Console.WriteLine(new string('-', 30));
                }
            }

            static void SearchContact(List<Contact> contacts)
            {
                Console.WriteLine("\n=== SEARCH CONTACT ===");
                Console.Write("Enter the name or last name to search: ");
                string searchInput = Console.ReadLine().ToLower();

                var foundContacts = contacts
                    .Where(c => c.Name.ToLower().Contains(searchInput) || c.LastName.ToLower().Contains(searchInput))
                    .ToList();

                if (foundContacts.Count == 0)
                {
                    Console.WriteLine("No contact found with that name.\n");
                }
                else
                {
                    Console.WriteLine($"\nFound {foundContacts.Count} contact(s):");
                    foreach (var contact in foundContacts)
                    {
                        Console.WriteLine($"\nID: {contact.Id}");
                        Console.WriteLine($"Name: {contact.Name} {contact.LastName}");
                        Console.WriteLine($"Address: {contact.Address}");
                        Console.WriteLine($"Telephone: {contact.Telephone}");
                        Console.WriteLine($"Email: {contact.Email}");
                        Console.WriteLine($"Age: {contact.Age}");
                        Console.WriteLine($"Best Friend: {(contact.BestFriend ? "Yes" : "No")}");
                        Console.WriteLine(new string('-', 30));
                    }
                }
            }

            static void ModifyContact(List<Contact> contacts)
            {
                Console.WriteLine("\n=== MODIFY CONTACT ===");
                Console.Write("Enter the ID of the contact you want to modify: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID format.\n");
                    return;
                }

                var contact = contacts.FirstOrDefault(c => c.Id == id);

                if (contact == null)
                {
                    Console.WriteLine("Contact not found.\n");
                    return;
                }

                Console.WriteLine($"Modifying contact: {contact.Name} {contact.LastName}");

                Console.Write("Enter new Name (leave empty to keep current): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    contact.Name = newName;
                }

                Console.Write("Enter new Last Name (leave empty to keep current): ");
                string newLastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newLastName))
                {
                    contact.LastName = newLastName;
                }

                Console.Write("Enter new Address (leave empty to keep current): ");
                string newAddress = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newAddress))
                {
                    contact.Address = newAddress;
                }

                Console.Write("Enter new Telephone (leave empty to keep current): ");
                string newTelephone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTelephone))
                {
                    contact.Telephone = newTelephone;
                }

                Console.Write("Enter new Email (leave empty to keep current): ");
                string newEmail = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newEmail))
                {
                    contact.Email = newEmail;
                }

                Console.Write("Enter new Age (leave empty to keep current): ");
                string ageInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(ageInput))
                {
                    if (int.TryParse(ageInput, out int newAge))
                    {
                        contact.Age = newAge;
                    }
                    else
                    {
                        Console.WriteLine("Invalid age input. Age not changed.");
                    }
                }

                Console.Write("Is this contact your best friend? (yes/no, leave empty to keep current): ");
                string bfInput = Console.ReadLine().ToLower();
                if (!string.IsNullOrWhiteSpace(bfInput))
                {
                    contact.BestFriend = (bfInput == "yes" || bfInput == "y" || bfInput == "si");
                }

                Console.WriteLine("\nContact updated successfully!\n");
            }

            static void DeleteContact(List<Contact> contacts)
            {
                Console.WriteLine("\n=== DELETE CONTACT ===");
                Console.Write("Enter the ID of the contact you want to delete: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID format.\n");
                    return;
                }

                var contact = contacts.FirstOrDefault(c => c.Id == id);

                if (contact == null)
                {
                    Console.WriteLine("Contact not found.\n");
                    return;
                }

                Console.Write($"Are you sure you want to delete {contact.Name} {contact.LastName}? (yes/no): ");
                string confirmation = Console.ReadLine().ToLower();

                if (confirmation == "yes" || confirmation == "y" || confirmation == "si")
                {
                    contacts.Remove(contact);
                    Console.WriteLine("Contact deleted successfully.\n");
                }
                else
                {
                    Console.WriteLine("Deletion canceled.\n");
                }
            }

        }
    }
}
