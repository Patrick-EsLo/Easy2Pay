using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SQLiteAgenda.Datos
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
