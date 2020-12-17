using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDS_2020.Class
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance {
            get
            {
                if(instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
            set
            {
                instance = value;
            }           
        }
        public BDS_2020Entities Database { get; set; }
        public DataProvider()
        {
            Database = new BDS_2020Entities();
        }
    }
}
