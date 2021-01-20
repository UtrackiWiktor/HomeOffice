using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeOffice.Data;
namespace HomeOffice.classes.Units
{
    class Unit
    {
        public int ID { get; set; }
        public string UnitName { get; set; }

        public void AddUnit(string unitName)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                Unit unit = new Unit
                {
                    UnitName=unitName
                };
                DbContext.Units.Add(unit);
                DbContext.SaveChanges();
            }
        }

        public List<Unit> UnitsToList()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var units = DbContext.Units.ToList();

                return units;
            }
        }
        public void DeleteUnit(Unit unit)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(unit);
                DbContext.SaveChanges();
            }
        }
    }
}
