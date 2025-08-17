using System;
using System.Collections.Generic;
using Cafeteria_Final_Project_C_.Models;
using Cafeteria_Final_Project_C_.Services;
using Cafeteria_Final_Project_C_.Data;
//Leandro Leguisamo Garcia 2024-2580


namespace Cafeteria_Final_Project_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Crear instancias de servicios
            var productService = new ProductService();
            var studentService = new StudentService();
            var transactionService = new TransactionService();
            var menuService = new MenuService();
            var purchaseService = new PurchaseService();

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("===== Welcome to the Coffee & Code =====");
                Console.WriteLine("1. Manage Products");
                Console.WriteLine("2. Manage Students");
                Console.WriteLine("3. Manage Transactions");
                Console.WriteLine("4. Manage Menu");
                Console.WriteLine("5. See Daily Menu");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageProducts(productService);
                        break;
                    case "2":
                        ManageStudents(studentService);
                        break;
                    case "3":
                        ManageTransactions(transactionService);
                        break;
                    case "4":
                        ManageMenu(menuService, productService);
                        break;
                    case "5":
                        SeeDailyMenu(menuService, productService);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ======== Manage Products ========
        static void ManageProducts(ProductService productService)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("----- Manage Products -----");
                Console.WriteLine("1. List Products");
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var products = productService.GetProducts();
                        foreach (var p in products)
                        {
                            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}, Stock: {p.Stock}, Active: {p.IsActive}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Write("Enter product name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter price: ");
                        decimal price = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter stock: ");
                        int stock = int.Parse(Console.ReadLine());

                        productService.AddProduct(new Product
                        {
                            Name = name,
                            Price = price,
                            Stock = stock,
                            IsActive = true
                        });

                        Console.WriteLine("Product added successfully!");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("Enter product ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        var productToUpdate = productService.GetProductById(updateId);
                        if (productToUpdate != null)
                        {
                            Console.Write("Enter new name: ");
                            productToUpdate.Name = Console.ReadLine();
                            Console.Write("Enter new price: ");
                            productToUpdate.Price = decimal.Parse(Console.ReadLine());
                            Console.Write("Enter new stock: ");
                            productToUpdate.Stock = int.Parse(Console.ReadLine());
                            Console.Write("Is Active? (true/false): ");
                            productToUpdate.IsActive = bool.Parse(Console.ReadLine());

                            productService.UpdateProduct(productToUpdate);
                            Console.WriteLine("Product updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Product not found.");
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Write("Enter product ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        productService.DeleteProduct(deleteId);
                        Console.WriteLine("Product deleted successfully!");
                        Console.ReadKey();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ======== Manage Students ========
        static void ManageStudents(StudentService studentService)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("----- Manage Students -----");
                Console.WriteLine("1. List Students");
                Console.WriteLine("2. Add Student");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Add Credit");
                Console.WriteLine("5. Deduct Credit");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var students = studentService.GetStudents();
                        foreach (var s in students)
                        {
                            Console.WriteLine($"ID: {s.Id}, Name: {s.FullName}, Student Number: {s.StudentNumber}, Credit: {s.Credit}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Write("Enter full name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter student number: ");
                        string number = Console.ReadLine();
                        Console.Write("Enter initial credit: ");
                        decimal credit = decimal.Parse(Console.ReadLine());

                        studentService.AddStudent(new Student
                        {
                            FullName = name,
                            StudentNumber = number,
                            Credit = credit
                        });

                        Console.WriteLine("Student added successfully!");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("Enter student ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        var studentToUpdate = studentService.GetStudentById(updateId);
                        if (studentToUpdate != null)
                        {
                            Console.Write("Enter new full name: ");
                            studentToUpdate.FullName = Console.ReadLine();
                            Console.Write("Enter new student number: ");
                            studentToUpdate.StudentNumber = Console.ReadLine();
                            Console.Write("Enter new credit: ");
                            studentToUpdate.Credit = decimal.Parse(Console.ReadLine());

                            studentService.UpdateStudent(studentToUpdate);
                            Console.WriteLine("Student updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Student not found.");
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Write("Enter student ID to add credit: ");
                        int creditId = int.Parse(Console.ReadLine());
                        Console.Write("Enter amount: ");
                        decimal addAmount = decimal.Parse(Console.ReadLine());
                        studentService.AddCredit(creditId, addAmount);
                        Console.WriteLine("Credit added successfully!");
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Write("Enter student ID to deduct credit: ");
                        int deductId = int.Parse(Console.ReadLine());
                        Console.Write("Enter amount: ");
                        decimal deductAmount = decimal.Parse(Console.ReadLine());
                        studentService.DeductCredit(deductId, deductAmount);
                        Console.WriteLine("Credit deducted successfully!");
                        Console.ReadKey();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ======== Manage Transactions ========
        static void ManageTransactions(TransactionService transactionService)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("----- Manage Transactions -----");
                Console.WriteLine("1. List Transactions");
                Console.WriteLine("2. Find Transaction by ID");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var transactions = transactionService.GetTransactions();
                        foreach (var t in transactions)
                        {
                            Console.WriteLine($"ID: {t.Id}, StudentID: {t.StudentId}, Date: {t.Date}, Total: {t.Total}");
                        }
                        Console.WriteLine("Press any key...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Write("Enter transaction ID: ");
                        int id = int.Parse(Console.ReadLine());
                        var transaction = transactionService.GetTransactionById(id);
                        if (transaction != null)
                        {
                            Console.WriteLine($"ID: {transaction.Id}, StudentID: {transaction.StudentId}, Date: {transaction.Date}, Total: {transaction.Total}");
                        }
                        else
                        {
                            Console.WriteLine("Transaction not found.");
                        }
                        Console.ReadKey();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ======== Manage Menu ========
        static void ManageMenu(MenuService menuService, ProductService productService)
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("----- Manage Menu -----");
                Console.WriteLine("1. List Menu Items");
                Console.WriteLine("2. Add Menu Item");
                Console.WriteLine("3. Update Menu Item");
                Console.WriteLine("4. Delete Menu Item");
                Console.WriteLine("5. Find Menu by Date");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var menuItems = menuService.GetMenuItems();
                        foreach (var m in menuItems)
                        {
                            Console.WriteLine($"ID: {m.Id}, ProductID: {m.ProductId}, Date: {m.Date}");
                        }
                        Console.WriteLine("Press any key...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Write("Enter product ID: ");
                        int productId = int.Parse(Console.ReadLine());
                        Console.Write("Enter date (yyyy-MM-dd): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());

                        menuService.AddMenuItem(new MenuItem
                        {
                            ProductId = productId,
                            Date = date
                        });

                        Console.WriteLine("Menu item added successfully!");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("Enter menu item ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        var menuToUpdate = menuService.GetMenuItemById(updateId);
                        if (menuToUpdate != null)
                        {
                            Console.Write("Enter new product ID: ");
                            menuToUpdate.ProductId = int.Parse(Console.ReadLine());
                            Console.Write("Enter new date (yyyy-MM-dd): ");
                            menuToUpdate.Date = DateTime.Parse(Console.ReadLine());

                            menuService.UpdateMenuItem(menuToUpdate);
                            Console.WriteLine("Menu item updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Menu item not found.");
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Write("Enter menu item ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        menuService.DeleteMenuItem(deleteId);
                        Console.WriteLine("Menu item deleted successfully!");
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Write("Enter date to search (yyyy-MM-dd): ");
                        DateTime searchDate = DateTime.Parse(Console.ReadLine());
                        var itemsByDate = menuService.GetMenuItemsByDate(searchDate);
                        foreach (var item in itemsByDate)
                        {
                            Console.WriteLine($"ID: {item.Id}, ProductID: {item.ProductId}, Date: {item.Date}");
                        }
                        Console.WriteLine("Press any key...");
                        Console.ReadKey();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ======== See Daily Menu ========
        static void SeeDailyMenu(MenuService menuService, ProductService productService)
        {
            Console.Clear();
            Console.WriteLine("----- Today's Menu -----");
            var todayMenu = menuService.GetMenuItemsByDate(DateTime.Now);
            foreach (var item in todayMenu)
            {
                var product = productService.GetProductById(item.ProductId);
                if (product != null)
                    Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
