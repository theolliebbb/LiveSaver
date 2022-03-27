using System;
using SQLite;
namespace LiveSaves.Models
{
    public class RegUser
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
