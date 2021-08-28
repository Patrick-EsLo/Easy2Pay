using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SQLiteAgenda.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User() { }
        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public bool CheckInformatLgn()
        {
            // if (!this.Username.Equals("") && !this.Password.Equals(""))//credenciais
            //   return true; 
            //else 
            //return false;
            if (string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password))
                return false;

            return true;
        }
    }
}
