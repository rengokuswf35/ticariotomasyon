﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-GTF8I3K\BERKAY;Initial Catalog=DboTicariOtomasyon;Integrated Security=True"); // SQL Sunucusuna bağlantı sağladım.
            baglan.Open();
            return baglan;
        }
    }
}
