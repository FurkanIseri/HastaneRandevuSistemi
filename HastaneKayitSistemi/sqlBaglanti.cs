using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;
using System.Data;

namespace HastaneKayitSistemi
{
    class sqlBaglanti
    {
        public NpgsqlConnection baglanti()
        {
            // PostgreSQL Bağlantı Cümlesi
            string baglantiAdresi = "Host=localhost;Port=5432;Database=HastaneYonetimSistemi;Username=postgres;Password=12345";

            NpgsqlConnection baglanma = new NpgsqlConnection(baglantiAdresi);

            // Eğer bağlantı zaten açık değilse aç
            if (baglanma.State != ConnectionState.Open)
            {
                baglanma.Open();
            }

            return baglanma;
        }
    }
}









