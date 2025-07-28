using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static Progr1_tarea_para_los_flojos.Program;
using static Progr1_tarea_para_los_flojos.Program.MedicalHistoryManager;
using static Progr1_tarea_para_los_flojos.Program.PatientManager;


namespace Progr1_tarea_para_los_flojos
{

    internal class Program
    {

        public class Patient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public int Age { get; set; }
            public DateTime Birthdate { get; set; }
            public string Sex { get; set; }
            public string Gender { get; set; }
        }

        public class MedicalHistory
        {
            public int IdPatient { get; set; }
            public string Diagnosis { get; set; }
            public string Treatment { get; set; }
            public string Observations { get; set; }
            public DateTime Date { get; set; }
        }

        public class PatientManager
        {
            private List<Patient> patients = new List<Patient>();
            private int NextId = 1;


            // ===== Patient Management Methods =====

            public List<Patient> Patients => patients;

            public void AddPatient()
            {
                Console.Clear();
                Console.WriteLine("=== Add New Patient ===");

                string name = PromptForNonEmptyString("Enter patient's first name: ");
                string lastName = PromptForNonEmptyString("Enter patient's last name: ");
                int age = PromptForValidAge("Enter patient's age: ");
                string gender = PromptForGender("Enter patient's gender (M/F): ");

                Console.WriteLine("\nPlease confirm the data:");
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Last Name: {lastName}");
                Console.WriteLine($"Age: {age}");
                Console.WriteLine($"Gender: {gender}");

                Console.Write("Do you want to save this patient? (Y/N): ");
                string confirmation = Console.ReadLine()?.Trim().ToUpper();

                if (confirmation == "Y")
                {
                    int newId = patients.Count > 0 ? patients.Max(p => p.Id) + 1 : 1;
                    patients.Add(new Patient
                    {
                        Id = newId,
                        Name = name,
                        LastName = lastName,
                        Age = age,
                        Gender = gender
                    });

                    Console.WriteLine("Patient added successfully!");
                }
                else
                {
                    Console.WriteLine("Operation cancelled.");
                }

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
            }

            public void EditPatient()
            {
                Console.Clear();
                Console.WriteLine("=== Edit Patient ===\n");

                string input = PromptNonEmptyInput("Enter the patient's ID or Name: ");

                Patient patient = null;

                if (int.TryParse(input, out int id))
                {
                    patient = patients.FirstOrDefault(p => p.Id == id);
                }
                else
                {
                    patient = patients.FirstOrDefault(p =>
                        $"{p.Name} {p.LastName}".ToLower().Contains(input.ToLower()));
                }

                if (patient == null)
                {
                    Console.WriteLine("\nPatient not found.");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nEditing patient: {patient.Name} {patient.LastName} - Age: {patient.Age}, Gender: {patient.Gender}");

                string newName = PromptNonEmptyInput("Enter new name: ");
                string newLastName = PromptNonEmptyInput("Enter new last name: ");

                int newAge;
                while (true)
                {
                    Console.Write("Enter new age: ");
                    if (int.TryParse(Console.ReadLine(), out newAge) && newAge > 0 && newAge < 120)
                        break;
                    Console.WriteLine("Invalid age. Please enter a valid number between 1 and 119.");
                }

                string newGender;
                while (true)
                {
                    Console.Write("Enter new gender (M/F): ");
                    newGender = Console.ReadLine()?.Trim().ToUpper();
                    if (newGender == "M" || newGender == "F")
                        break;
                    Console.WriteLine("Invalid gender. Please enter 'M' or 'F'.");
                }

                Console.WriteLine("\nReview the changes:");
                Console.WriteLine($"Name: {patient.Name} -> {newName}");
                Console.WriteLine($"Last Name: {patient.LastName} -> {newLastName}");
                Console.WriteLine($"Age: {patient.Age} -> {newAge}");
                Console.WriteLine($"Gender: {patient.Gender} -> {newGender}");

                if (ConfirmAction("Do you want to save these changes?"))
                {
                    patient.Name = newName;
                    patient.LastName = newLastName;
                    patient.Age = newAge;
                    patient.Gender = newGender;

                    Console.WriteLine("\nPatient updated successfully.");
                }
                else
                {
                    Console.WriteLine("\nUpdate canceled.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            public void ListPatients()
            {
                Console.Clear();
                Console.WriteLine("=== List of Registered Patients ===\n");

                if (patients.Count == 0)
                {
                    Console.WriteLine("No patients found in the system.");
                }
                else
                {
                    foreach (var patient in patients)
                    {
                        Console.WriteLine($"ID: {patient.Id}");
                        Console.WriteLine($"Name: {patient.Name}");
                        Console.WriteLine($"Last Name: {patient.LastName}");
                        Console.WriteLine($"Age: {patient.Age}");
                        Console.WriteLine($"Gender: {patient.Gender}");
                        Console.WriteLine(new string('-', 30));
                    }
                }

            }

            public void DeletePatient()
            {
                Console.Clear();
                Console.WriteLine("=== Delete Patient ===\n");

                string input = PromptNonEmptyInput("Enter the patient's ID or Name: ");

                Patient patient = null;

                if (int.TryParse(input, out int id))
                {
                    patient = patients.FirstOrDefault(p => p.Id == id);
                }
                else
                {

                    patient = patients.FirstOrDefault(p =>
                        $"{p.Name} {p.LastName}".ToLower().Contains(input.ToLower()));
                }

                if (patient == null)
                {
                    Console.WriteLine("\nPatient not found.");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nFound patient: {patient.Name} {patient.LastName} - Age: {patient.Age}, Gender: {patient.Gender}");

                if (ConfirmAction("Are you sure you want to delete this patient?"))
                {
                    patients.Remove(patient);
                    Console.WriteLine("\nPatient successfully deleted.");
                }
                else
                {
                    Console.WriteLine("\nDeletion canceled.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            public void SearchPatient()
            {
                Console.Clear();
                Console.WriteLine("=== SEARCH PATIENT ===");
                Console.WriteLine("1. Search by ID");
                Console.WriteLine("2. Search by Name");
                Console.Write("Choose an option (1 or 2): ");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    int id = ReadValidInt("Enter the patient ID: ");
                    var patient = patients.Find(p => p.Id == id);
                    if (patient != null)
                    {
                        DisplayPatient(patient);
                    }
                    else
                    {
                        Console.WriteLine("Patient not found.");
                    }
                }
                else if (option == "2")
                {
                    string nameInput = ReadNonEmptyString("Enter the patient name or part of it: ").ToLower();
                    var results = patients.FindAll(p => p.Name.ToLower().Contains(nameInput));
                    if (results.Count > 0)
                    {
                        foreach (var patient in results)
                        {
                            DisplayPatient(patient);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No matching patients found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            private void DisplayPatient(Patient patient)
            {
                Console.WriteLine("\n----- Patient Information -----");
                Console.WriteLine($"ID: {patient.Id}");
                Console.WriteLine($"Name: {patient.Name} {patient.LastName}");
                Console.WriteLine($"Age: {patient.Age}");
                Console.WriteLine($"Sex: {patient.Sex}");
                Console.WriteLine($"Phone: {patient.Phone}");
                Console.WriteLine($"Address: {patient.Address}");
                Console.WriteLine($"Birthdate: {patient.Birthdate:yyyy-MM-dd}");
            }

            public void ListAllPatients()
            {
                Console.Clear();

                if (patients.Count == 0)
                {
                    Console.WriteLine("No patients have been registered.");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("===== Registered Patients =====\n");

                foreach (var patient in patients)
                {
                    DisplayPatient(patient);
                    Console.WriteLine("-------------------------------");
                }

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
            }

            public void SaveToFile()
            {
                PatientStorage.SaveDataToJson(patients);
            }

            public void LoadFromFile()
            {
                patients = PatientStorage.LoadDataFromJson();
            }



            //===== Reusable patient methods =====

            public static int ReadValidInt(string prompt)
            {
                int value;
                while (true)
                {
                    Console.Write(prompt);
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out value) && value >= 0)
                        return value;

                    Console.WriteLine("Invalid number. Please enter a valid non-negative integer.");
                }
            }

            private string ReadNonEmptyString(string prompt)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write("Input cannot be empty. " + prompt);
                    input = Console.ReadLine();
                }
                return input;
            }

            private bool ConfirmAction(string message)
            {
                Console.Write($"{message} (Y/N): ");
                string input = Console.ReadLine()?.Trim().ToUpper();
                return input == "Y";
            }

            private string PromptNonEmptyInput(string prompt)
            {
                string input;
                do
                {
                    Console.Write(prompt);
                    input = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Input cannot be empty. Please try again.");
                    }
                } while (string.IsNullOrWhiteSpace(input));
                return input;
            }

            private string PromptForNonEmptyString(string message)
            {
                string input;
                do
                {
                    Console.Write(message);
                    input = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("This field cannot be empty.");
                    }
                } while (string.IsNullOrWhiteSpace(input));
                return input;
            }

            private int PromptForValidAge(string message)
            {
                int age;
                do
                {
                    Console.Write(message);
                    if (!int.TryParse(Console.ReadLine(), out age) || age <= 0 || age > 120)
                    {
                        Console.WriteLine("Please enter a valid age (1-120).");
                        age = -1;
                    }
                } while (age == -1);
                return age;
            }

            private string PromptForGender(string message)
            {
                string gender;
                do
                {
                    Console.Write(message);
                    gender = Console.ReadLine()?.Trim().ToUpper();
                    if (gender != "M" && gender != "F")
                    {
                        Console.WriteLine("Please enter 'M' for Male or 'F' for Female.");
                    }
                } while (gender != "M" && gender != "F");
                return gender;
            }

            private int ReadValidInt(string prompt, string errorMessage)
            {
                int value;
                Console.Write(prompt);
                while (!int.TryParse(Console.ReadLine(), out value))
                {
                    Console.Write(errorMessage + " ");
                }
                return value;
            }

            private string ReadOptionalString(string prompt)
            {
                Console.Write(prompt);
                return Console.ReadLine();
            }

            private DateTime ReadValidDate(string prompt)
            {
                DateTime date;
                Console.Write(prompt);
                while (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.Write("Invalid date format. Please enter in dd/mm/yyyy format: ");
                }
                return date;
            }

            private static void Pause()
            {
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }

        }


        public class MedicalHistoryManager
        {
            private List<MedicalHistory> medicalHistories = new List<MedicalHistory>();
            private int nextId = 1;


            // ===== Medical History Management Methods =====

            public void AddMedicalHistory(List<Patient> patients)
            {
                Console.Clear();
                Console.WriteLine("\n===== ADD MEDICAL HISTORY =====");

                int patientId = ReadValidInt("Enter patient ID: ", "Invalid ID format. Please enter a valid number.");

                var patient = patients.Find(p => p.Id == patientId);
                if (patient == null)
                {
                    Console.WriteLine($"No patient found with ID {patientId}.");
                    Pause();
                    return;
                }

                string diagnosis = ReadNonEmptyString("Diagnosis: ");

                string treatment = ReadOptionalString("Treatment: ");
                string observations = ReadOptionalString("Observations: ");

                DateTime date = ReadValidDate("Date (dd/mm/yyyy): ");

                Console.WriteLine("\nPlease confirm the information:");
                Console.WriteLine($"Patient: {patient.Name} {patient.LastName}");
                Console.WriteLine($"Diagnosis: {diagnosis}");
                Console.WriteLine($"Treatment: {treatment}");
                Console.WriteLine($"Observations: {observations}");
                Console.WriteLine($"Date: {date:dd/MM/yyyy}");

                if (ConfirmAction("Save this medical history? (Y/N): "))
                {
                    MedicalHistory history = new MedicalHistory
                    {
                        IdPatient = patientId,
                        Diagnosis = diagnosis,
                        Treatment = treatment,
                        Observations = observations,
                        Date = date
                    };

                    medicalHistories.Add(history);

                    Console.WriteLine("\nMedical history added successfully.");
                }
                else
                {
                    Console.WriteLine("\nOperation cancelled. No data was saved.");
                }

                Pause();
            }

            public void ShowMedicalHistory(List<Patient> patients)

            {
                Console.Clear();
                Console.WriteLine("=== SHOW MEDICAL HISTORY ===");
                Console.WriteLine("1. Search by Patient ID");
                Console.WriteLine("2. Search by Patient Name");
                Console.Write("Choose an option (1 or 2): ");

                string option = Console.ReadLine();

                List<MedicalHistory> historiesToShow = new List<MedicalHistory>();

                if (option == "1")
                {
                    Console.Write("Enter the patient ID: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        historiesToShow = medicalHistories.FindAll(h => h.IdPatient == id);
                        if (historiesToShow.Count == 0)
                        {
                            Console.WriteLine("No medical history found for this patient ID.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format.");
                    }
                }
                else if (option == "2")
                {
                    Console.Write("Enter the patient name or part of it: ");
                    string nameInput = Console.ReadLine().Trim().ToLower();

                    var matchedPatients = patients.FindAll(p => p.Name.ToLower().Contains(nameInput));
                    if (matchedPatients.Count == 0)
                    {
                        Console.WriteLine("No patients found with that name.");
                    }
                    else
                    {
                        foreach (var patient in matchedPatients)
                        {
                            var patientHistories = medicalHistories.FindAll(h => h.IdPatient == patient.Id);
                            historiesToShow.AddRange(patientHistories);
                        }

                        if (historiesToShow.Count == 0)
                        {
                            Console.WriteLine("No medical history found for matched patients.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }

                if (historiesToShow.Count > 0)
                {
                    foreach (var h in historiesToShow)
                    {
                        Console.WriteLine("\n----- Medical History -----");
                        Console.WriteLine($"Patient ID: {h.IdPatient}");
                        Console.WriteLine($"Diagnosis: {h.Diagnosis}");
                        Console.WriteLine($"Treatment: {h.Treatment}");
                        Console.WriteLine($"Observations: {h.Observations}");
                        Console.WriteLine($"Date: {h.Date:yyyy-MM-dd}");
                    }
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            public void ListAllMedicalHistories(List<Patient> patients)

            {
                Console.Clear();
                Console.WriteLine("===== ALL MEDICAL HISTORIES =====");

                if (medicalHistories.Count == 0)
                {
                    Console.WriteLine("No medical histories found.");
                    return;
                }

                foreach (var history in medicalHistories)
                {
                    var patient = patients.Find(p => p.Id == history.IdPatient);
                    Console.WriteLine($"Patient ID: {history.IdPatient}");
                    Console.WriteLine($"Patient Name: {(patient != null ? patient.Name + " " + patient.LastName : "Unknown")}");
                    Console.WriteLine($"Diagnosis: {history.Diagnosis}");
                    Console.WriteLine($"Treatment: {history.Treatment}");
                    Console.WriteLine($"Observations: {history.Observations}");
                    Console.WriteLine($"Date: {history.Date.ToShortDateString()}");
                    Console.WriteLine("----------------------------------");
                }

                Console.WriteLine("Press Enter to return...");
                Console.ReadLine();
            }

            public void UpdateMedicalHistory()
            {
                Console.Clear();

                Console.Write("Enter the patient ID of the medical history to update: ");
                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Console.WriteLine("Invalid ID format.");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    return;
                }


                var patientHistories = medicalHistories.FindAll(h => h.IdPatient == patientId);

                if (patientHistories.Count == 0)
                {
                    Console.WriteLine($"No medical histories found for patient ID {patientId}.");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nFound {patientHistories.Count} record(s). Select which one to update:");
                for (int i = 0; i < patientHistories.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Diagnosis: {patientHistories[i].Diagnosis}, Date: {patientHistories[i].Date:yyyy-MM-dd}");
                }

                Console.Write("Enter the number of the record to update: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > patientHistories.Count)
                {
                    Console.WriteLine("Invalid selection.");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                var history = patientHistories[choice - 1];

                Console.WriteLine("\nLeave blank to keep current value.");

                Console.Write($"Diagnosis ({history.Diagnosis}): ");
                string diagnosis = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(diagnosis)) history.Diagnosis = diagnosis;

                Console.Write($"Treatment ({history.Treatment}): ");
                string treatment = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(treatment)) history.Treatment = treatment;

                Console.Write($"Observations ({history.Observations}): ");
                string observations = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(observations)) history.Observations = observations;

                Console.Write($"Date ({history.Date:yyyy-MM-dd}): ");
                string dateInput = Console.ReadLine();
                if (DateTime.TryParse(dateInput, out DateTime newDate)) history.Date = newDate;

                Console.WriteLine("\nMedical history updated successfully.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            public void DeleteMedicalHistory()
            {
                Console.Clear();

                Console.Write("Enter the patient ID of the medical history to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Console.WriteLine("Invalid ID format.");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                var patientHistories = medicalHistories.FindAll(h => h.IdPatient == patientId);

                if (medicalHistories.Count == 0)
                {
                    Console.WriteLine($"No medical histories found for patient ID {patientId}.");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nFound {medicalHistories.Count} record(s). Select which one to delete:");
                for (int i = 0; i < medicalHistories.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Diagnosis: {medicalHistories[i].Diagnosis}, Date: {medicalHistories[i].Date:yyyy-MM-dd}");
                }

                Console.Write("Enter the number of the record to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > medicalHistories.Count)
                {
                    Console.WriteLine("Invalid selection.");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                var history = medicalHistories[choice - 1];

                string confirm;
                do
                {
                    Console.Write($"\nAre you sure you want to delete this medical history record? (Y/N): ");
                    confirm = Console.ReadLine().Trim().ToUpper();
                    if (confirm != "Y" && confirm != "N")
                    {
                        Console.WriteLine("Invalid option. Please enter 'Y' or 'N'.");
                    }
                }
                while (confirm != "Y" && confirm != "N");

                if (confirm == "Y")
                {
                    medicalHistories.Remove(history);
                    Console.WriteLine("Medical history record deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Deletion canceled. No changes were made.");
                }

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
            }



            //===== Reusable methods for medical history =====
            private string ReadNonEmptyString(string prompt)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write("Input cannot be empty. Please enter again: ");
                    input = Console.ReadLine();
                }
                return input;
            }

            private static string ValidateDescription()
            {
                string description;
                do
                {
                    Console.Write("Enter description: ");
                    description = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(description))
                    {
                        Console.WriteLine("Description cannot be empty. Please try again.");
                    }

                } while (string.IsNullOrWhiteSpace(description));

                return description;
            }

            private static DateTime ValidateDate()
            {
                DateTime date;
                while (true)
                {
                    Console.Write("Enter date (yyyy-mm-dd): ");
                    string input = Console.ReadLine()?.Trim();

                    if (DateTime.TryParse(input, out date))
                    {
                        return date;
                    }

                    Console.WriteLine("Invalid date format. Please use yyyy-mm-dd.");
                }
            }

            private static Patient FindPatientByIdOrName(List<Patient> patients)
            {
                Console.Write("Enter Patient ID or Name: ");
                string input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out int id))
                {
                    return patients.FirstOrDefault(p => p.Id == id);
                }
                else
                {
                    return patients.FirstOrDefault(p => $"{p.Name} {p.LastName}".Equals(input, StringComparison.OrdinalIgnoreCase));
                }
            }

            private static MedicalHistory FindMedicalHistoryById(List<MedicalHistory> histories, int id)
            {
                return histories.FirstOrDefault(h => h.IdPatient == id);
            }

            private static bool ConfirmAction(string message)
            {
                Console.Write($"{message} (Y/N): ");
                string input = Console.ReadLine()?.Trim().ToUpper();
                return input == "Y";
            }

            private static void Pause()
            {
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }

            private int ReadValidInt(string prompt, string errorMessage)
            {
                int value;
                Console.Write(prompt);
                while (!int.TryParse(Console.ReadLine(), out value))
                {
                    Console.Write(errorMessage + " ");
                }
                return value;
            }

            private string ReadOptionalString(string prompt)
            {
                Console.Write(prompt);
                return Console.ReadLine();
            }

            private DateTime ReadValidDate(string prompt)
            {
                DateTime date;
                Console.Write(prompt);
                while (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.Write("Invalid date format. Please enter in dd/mm/yyyy format: ");
                }
                return date;
            }



            //===== Executable method of the .json file ====
            public static class PatientStorage
            {
                private static readonly string filePath = "patients.json";

                public static List<Patient> LoadDataFromJson()
                {
                    return JsonStorage.LoadFromFile<Patient>(filePath);
                }

                public static void SaveDataToJson(List<Patient> patients)
                {
                    JsonStorage.SaveToFile(filePath, patients);
                }
            }



            //===== JSON File Handling Methods =====

            public static class JsonStorage
            {
                public static void SaveToFile<T>(string filePath, List<T> data)
                {
                    try
                    {
                        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(filePath, json);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving to file '{filePath}': {ex.Message}");
                    }
                }

                public static List<T> LoadFromFile<T>(string filePath)
                {
                    try
                    {
                        if (File.Exists(filePath))
                        {
                            var json = File.ReadAllText(filePath);
                            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading from file '{filePath}': {ex.Message}");
                    }
                    return new List<T>();
                }
            }

        }




        static void Main(string[] args)
        {

            PatientManager manager = new PatientManager();
            MedicalHistoryManager historyManager = new MedicalHistoryManager();
            manager.LoadFromFile();

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("======================================");
                Console.WriteLine("  WELCOME TO THE PATIENT RECORD APP");
                Console.WriteLine("======================================\n");
                Console.WriteLine("This application helps you manage patient records and medical histories.");
                Console.WriteLine("You can register patients, update their information, and keep track of their medical records.\n");

                Console.WriteLine("--------- MAIN MENU ---------");
                Console.WriteLine("PATIENT MANAGEMENT:");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Edit Patient");
                Console.WriteLine("3. Delete Patient");
                Console.WriteLine("4. Search Patient");

                Console.WriteLine("\nMEDICAL HISTORY:");
                Console.WriteLine("5. Add Medical History");
                Console.WriteLine("6. Show Medical History");
                Console.WriteLine("7. Update Medical History");
                Console.WriteLine("8. Delete Medical History");
                Console.WriteLine("9. List All Medical Histories");
                Console.WriteLine("10. List All Patients");


                Console.WriteLine("\nGENERAL:");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect an option by entering its number: ");
                string input = Console.ReadLine().Trim();

                if (!int.TryParse(input, out int optionNumber) || optionNumber < 0 || optionNumber > 10)
                {
                    Console.WriteLine("\nInvalid input. Please enter a number between 0 and 10.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                }
                else
                {
                    switch (optionNumber)
                    {
                        case 1:
                            manager.AddPatient();
                            break;
                        case 2:
                            manager.EditPatient();
                            break;
                        case 3:
                            manager.DeletePatient();
                            break;
                        case 4:
                            manager.SearchPatient();
                            break;
                        case 5:
                            historyManager.AddMedicalHistory(manager.Patients);
                            break;
                        case 6:
                            historyManager.ShowMedicalHistory(manager.Patients);
                            break;
                        case 7:
                            historyManager.UpdateMedicalHistory();
                            break;
                        case 8:
                            historyManager.DeleteMedicalHistory();
                            break;
                        case 9:
                            historyManager.ListAllMedicalHistories(manager.Patients);
                            break;
                        case 10:
                            manager.ListAllPatients();
                            break;
                        case 0:
                            running = false;
                            manager.SaveToFile();
                            Console.WriteLine("\nThank you for using the Patient Record App. Goodbye! :)");
                            break;
                    }
                }

                if (running)
                { 
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}
