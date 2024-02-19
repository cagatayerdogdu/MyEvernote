using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.MySql
{
    public class RepositoryBase
    {
        /*Singleton repository mantığına gerek olursa EF deki repbase ile aynı mantkta burası da yazılmalı ama oradaki DbCOntext MYSQL e uygun olanı yazılmalı*/

        protected static object/*DatabaseContext*/ context;/*TODO: private idi Repository classı buradan miras aldığı için protecteda çektik çünkü protected miras alınanda görülebilecektir.*/
        private static object _lockObj = new object(); // Lock ifadesi bizden bir obje istiyor bu yüzden method static olduğu için static bir obje oluşturarak oraya ekledik.

        protected RepositoryBase()
        {
            // constructur tarafında alttaki methodu çağırarak db (context) yi burada oluşturuyoruz.
            CreatedContext();
        }

        private static void CreatedContext()  /* static yapmamızın sebebi new lenmesini istemediğimiz için protected yaptık ve repository içinde new lemeden bu methodu çağırıyor olacağız.
                                                          * Burası da önceden public idi, miras olayına girdikten sonra private yaptık sadece bu class içinden erişilebilsin diye
                                                          */
        {
            if (context == null)
            {
                lock (_lockObj) //lock multiple (multi thread) uygulamalarda birden fazla yerde aynı anda dbcontext çağırılırsa biri bitmeden diğeri çalışmasın mantığıyla kullanılır. Burada da onu düşünerek yazdık.
                {
                    if (context == null) // lock ifadesinden sonra ekstradan bir kontrol koymak istenebilir.
                    {
                        context = new /*DatabaseContext*/ object();
                    }
                }
            }

            //return db; // miras öncesi bu method; public static DatabaseContext CreatedContext()  olduğu için return vardı onu da kapadık.
        }
    }
}