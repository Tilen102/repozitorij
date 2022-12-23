using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bogTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            //BlueOceanDBEntities dc = new BlueOceanDBEntities();
            //var s=metode.methodOne(Cstartdate, Cendtdate);
            //Console.WriteLine(s);




            //IZBIRA DATUMA
            Console.WriteLine("Select start date (11.11.2021)");
            var StartDate = Console.ReadLine();
            DateTime Cstartdate = DateTime.Parse(StartDate);

            Console.WriteLine("Select end date (11.11.2021)");
            var EndDate = Console.ReadLine();
            DateTime Cendtdate = DateTime.Parse(EndDate);

            //IZBIRA KATEGORIJE
            Console.WriteLine("Select category (1,2,3,4)");
            var categoryType = Console.ReadLine();



            //KATEGORIJA 1
            if (categoryType == "1")
            {
                metode.SpinNumber(Cstartdate, Cendtdate);
            }
            else if (categoryType == "2")
            {
                metode.HighestBet(Cstartdate, Cendtdate);
            }
            else if(categoryType == "3")
                metode.Multyplier(Cstartdate, Cendtdate);
            else if (categoryType == "4")
                metode.NoWin(Cstartdate, Cendtdate);





            else
            {
                Console.WriteLine("WOOPS, SOMETHING WENT WRONG");
            }


            
            Console.ReadLine();
        }

        
    }
}
