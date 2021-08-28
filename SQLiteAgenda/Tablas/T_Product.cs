using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SQLiteAgenda.Tablas
{
    public class T_Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Barcode { get; set; }
        [MaxLength(255)]
        public string Cost { get; set; }
    }
}
