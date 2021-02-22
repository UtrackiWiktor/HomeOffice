using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeOffice.Data;
namespace HomeOffice.classes.Units
{
    public class Unit
    {
        public int ID { get; set; }
        public string UnitName { get; set; }
        public Unit() { }//used by entity framework, otherwise it throws exp
        public Unit(string unitName)
        {
            UnitName = unitName;
        }
        public void AddUnit()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                
                DbContext.Units.Add(this);
                DbContext.SaveChanges();
            }
        }

        public List<Unit> AllUnitsToList()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var units = DbContext.Units.ToList();

                return units;
            }
        }
        public void DeleteUnit()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(this);
                DbContext.SaveChanges();
            }
        }

        public void UpdateUnit()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var result = DbContext.Units.SingleOrDefault(u => u.ID == ID);
                if (result != null)
                {
                    DbContext.Entry(result).CurrentValues.SetValues(this);
                    DbContext.SaveChanges();

                }

            }
        }
    }
}
