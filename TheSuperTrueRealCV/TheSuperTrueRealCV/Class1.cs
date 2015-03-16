using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TheSuperTrueRealCV
{
    class Class1
    {
        DataGridView grid;

        public List<string> selectDataFromDB()
        {
            string sql = "SELECT * FROM PEOPLE WHERE LAND = 'SVERIGE' ";

            ///EXECUTE SQL
            ///

            var asd = new List<string>();
            asd.Add("ASD");
            asd.Add("DSA");


           return asd;
        }

    }
}
