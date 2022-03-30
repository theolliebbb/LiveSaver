using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using LiveSaves.Services;
using LiveSaves.Models;
namespace ISQLites
{
    public interface ISQLite
    {
        SQLiteConnection GetConnectionWithCreateDatabase();

        bool SaveEmployee(Live live);

        List<Live> GetEmployees();

        bool UpdateEmployee(Live live);
        void DeleteEmployee(int Id);
    }
}
