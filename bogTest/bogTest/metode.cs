using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace bogTest
{
    internal class metode
    {
       
        //NUMBER OF SPINS
        public static void SpinNumber(DateTime Cstartdate, DateTime Cendtdate)
        {
            BlueOceanDBEntities dc = new BlueOceanDBEntities();

            var x1 = (from a in dc.PoslaniPodatki
                      where a.amount < 0 && (a.DatumZapisa >= Cstartdate && a.DatumZapisa <= Cendtdate)
                      group a by a.nickname into z
                      select new { nickname = z.Key, round = z.Count() });
            var x2 = x1.OrderByDescending(b => b.round).Take(10);
            foreach (var x in x2)
            {
                Console.WriteLine("UPORABNIK: " + x.nickname + "          |          ŠTEVILO SPINOU" + x.round);
            }
        }

        //HIGHEST BET AMOUNT
        public static void HighestBet(DateTime Cstartdate, DateTime Cendtdate)
        {
            BlueOceanDBEntities dc = new BlueOceanDBEntities();
            decimal ubc = 0.0007m;
            decimal usd = 0.94m;
            Console.WriteLine("METHOD 2");
            var query = (from a in dc.PoslaniPodatki
                      where (a.DatumZapisa >= Cstartdate && a.DatumZapisa <= Cendtdate)
                      where ((a.currencycode == "USD" && (a.amount * usd) < -1) || (a.currencycode == "UBC" && (a.amount * ubc) < -1) || (a.currencycode == "EUR" && a.amount < -1))
                      select new { a.nickname, a.amount, a.currencycode, a.DatumZapisa}).OrderByDescending(b => b.amount) ;

            //var m = from l in query
            //        group l by l.nickname into z;

            
            var query2 = query.Take(10);
            foreach (var x in query2)
            {
                if (x.currencycode == "USD")
                    Console.WriteLine("UPORABNIK: " + x.nickname + "          |          VSOTA STAVE: " + x.amount * usd + "           |            DATUM: " + x.DatumZapisa);
                else if (x.currencycode == "UBC")
                    Console.WriteLine("UPORABNIK: " + x.nickname + "          |          VSOTA STAVE: " + x.amount * ubc + "           |            DATUM: " + x.DatumZapisa);
                else
                    Console.WriteLine("UPORABNIK: " + x.nickname + "          |          VSOTA STAVE: " + x.amount+x.currencycode+ "           |            DATUM: " + x.DatumZapisa);
            }
        }

        public static void Multyplier(DateTime Cstartdate, DateTime Cenddate)
        {
            BlueOceanDBEntities dc = new BlueOceanDBEntities();
            decimal ubc = 0.0007m;
            decimal usd = 0.94m;
            Console.WriteLine("METHOD 3");
            var query = (from a in dc.PoslaniPodatki
                         where (a.DatumZapisa >= Cstartdate && a.DatumZapisa <= Cenddate)
                         group a by a.round_id);
            //var query2 =  dc.PoslaniPodatki.GroupBy(a => a.round_id).Take(10);
            List<pomoc> nova = new List<pomoc>();
            foreach(IGrouping<string, PoslaniPodatki> groupP in query)
            {
                //Console.WriteLine(groupP.Key+": ");
                
                var bet = (from a1 in groupP
                           where a1.amount < 0
                           select a1.amount ).FirstOrDefault();
                var win = (from a1 in groupP
                           where a1.amount > 0
                           select a1.amount).FirstOrDefault();
                var name = (from a2 in groupP
                           select a2.nickname).First();
                //var multiply = (decimal)bet / (decimal)win;
                //Console.WriteLine();

                if (bet != null && win != null)
                {
                    pomoc n1 = new pomoc();
                    n1.Nickname = name;
                    n1.amount = (decimal)win / (decimal)bet;
                    nova.Add(n1);


                }
                    Console.WriteLine("MULTIPLYER:  " + (decimal)win / (decimal)bet + "NICKNAME: "+ name);
                //foreach (PoslaniPodatki a in groupP)
                //{
                //    Console.WriteLine(a.amount);
                    
                //}
            }
            
        }

        public static void NoWin(DateTime Cstartdate, DateTime Cenddate)
        {
            BlueOceanDBEntities dc = new BlueOceanDBEntities();
            decimal ubc = 0.0007m;
            decimal usd = 0.94m;
            Console.WriteLine("METHOD 3");
            var query = (from a in dc.PoslaniPodatki
                         where (a.DatumZapisa >= Cstartdate && a.DatumZapisa <= Cenddate)
                         
                         group a by a.round_id into z
                         select new { round_id = z.Key });
            foreach (var x in query)
            {
                Console.WriteLine("UPORABNIK: " + x.round_id);
            }
        }
    }
}
