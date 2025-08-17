using Cafeteria_Final_Project_C_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Cafeteria_Final_Project_C_.Services
{
    internal class MenuService
    {
        public MenuService() { }
        public MenuService(string menuName) { }
        public List<MenuItem> GetMenuItems()
        {
            using (var db = new Data.DBconection())
            {
                return db.MenuItems.ToList();
            }
        }
        public MenuItem GetMenuItemById(int id)
        {
            using (var db = new Data.DBconection())
            {
                return db.MenuItems.FirstOrDefault(m => m.Id == id);
            }
        }
        public void AddMenuItem(MenuItem menuItem)
        {
            using (var db = new Data.DBconection())
            {
                db.MenuItems.Add(menuItem);
                db.SaveChanges();
            }
        }
        public void UpdateMenuItem(MenuItem menuItem)
        {
            using (var db = new Data.DBconection())
            {
                var existingMenuItem = db.MenuItems.FirstOrDefault(m => m.Id == menuItem.Id);
                if (existingMenuItem != null)
                {
                    existingMenuItem.ProductId = menuItem.ProductId;
                    existingMenuItem.Date = menuItem.Date;
                    db.SaveChanges();
                }
            }
        }
        public void DeleteMenuItem(int id)
        {
            using (var db = new Data.DBconection())
            {
                var menuItem = db.MenuItems.FirstOrDefault(m => m.Id == id);
                if (menuItem != null)
                {
                    db.MenuItems.Remove(menuItem);
                    db.SaveChanges();
                }
            }
        }
        public List<MenuItem> GetMenuItemsByDate(DateTime date)
        {
            using (var db = new Data.DBconection())
            {
                return db.MenuItems.Where(m => m.Date.Date == date.Date).ToList();
            }
        }
        public List<MenuItem> GetMenuItemsByProductId(int productId)
        {
            using (var db = new Data.DBconection())
            {
                return db.MenuItems.Where(m => m.ProductId == productId).ToList();
            }

        }
    }
}

