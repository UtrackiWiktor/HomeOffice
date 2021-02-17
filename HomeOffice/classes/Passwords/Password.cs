using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeOffice.Data;
using System.Security.Cryptography;

namespace HomeOffice.classes.Passwords
{
    public class Password
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
        public string EncodePassword()
        {
            using (SHA256 sha256Hash = SHA256.Create())
            { 
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Password_));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                Password_ = builder.ToString();
                return Password_;
            }
        }
        public bool CompareWithPassword(string input)
        {
            string result;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                result = builder.ToString();
            }
            return (result == Password_);
        }
        public void FromDatabase(int id)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                var query = DbContext.Passwords.Where(p => p.ID == id);
                Password password= query.FirstOrDefault<Password>();
                if (password != null)
                {
                    ID = password.ID;
                    Password_ = password.Password_;
                }
                else
                    ID = -1;
            }
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
