using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeOffice.Data;

namespace HomeOffice.classes.Passwords
{
    class Password
    {
        public int ID { get; set; }
        public string Password_ { get; set; }
        public Password() {}
        public Password(int id, string word)
        {
            ID = id;
            Password_ = word;
        }
        public Password(int id) 
        {
            ID = id;

            StringBuilder passwordBuilder = new StringBuilder();
            Random random = new Random();
            int length = random.Next(8, 15);
            string legalChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            for (int i=0;i<length;i++)
            {
                passwordBuilder.Append(legalChars[random.Next(legalChars.Length)]);
            }

            Password_ = passwordBuilder.ToString();
        }
        public string GetPassword()
        {
            return Password_;
        }
        public void EncodePassword(int seed, string toEncode)
        {
            Password_ = toEncode;
        }
        public void DecodePassword(int seed, string toDecode)
        {
            Password_ = toDecode;
        }
        public void AddPassword()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();

                DbContext.Passwords.Add(this);
                DbContext.SaveChanges();
            }
        }

        public List<Password> AllPasswordsToList()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var passwords = DbContext.Passwords.ToList();

                return passwords;
            }
        }
        public void DeletePassword()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(this);
                DbContext.SaveChanges();
            }
        }
    }
}
