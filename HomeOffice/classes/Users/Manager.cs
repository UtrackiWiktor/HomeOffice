using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOffice.classes.Users
{
    public class Manager: User
    {
        internal static object manager;

        public Manager(string name, string surname, DateTime date, UserRoles typeOfUser, int unit,long pesel) : base(name, surname, date, typeOfUser, unit, pesel) { }


        //Manager prints the report of all activities that has the same ManagerID
        public String PrintTheReport(List<String> list)
        {
            //search for a user's desktop and create folder there
            string path = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);
            var file = File.CreateText(path);
            //save task list content to the list
            file.Write(CreateCSVTextFile(list));
            return "Report saved as CSV file";
        }

        private string CreateCSVTextFile<T>(List<T> data)
        {
            var properties = typeof(T).GetProperties();
            var result = new StringBuilder();

            foreach (var row in data)
            {
                var values = properties.Select(p => p.GetValue(row, null))
                                       .Select(v => StringToCSVCell(Convert.ToString(v)));
                var line = string.Join(",", values);
                result.AppendLine(line);
            }

            return result.ToString();
        }

        private string StringToCSVCell(string str)
        {
            bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }

            return str;
        }

    }
}
