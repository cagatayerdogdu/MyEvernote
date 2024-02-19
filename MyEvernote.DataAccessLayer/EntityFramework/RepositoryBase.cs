using MyEvernote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class RepositoryBase

    {
        /* TODO: Singleton Repository Pattern
         * Comment ve Note eklenirken Repository e bağlanılıyorlar her bağlandıklarında oradaki DbContext new leniyor ve bu durum multiple (çoklandığı için) hatasına sebep oluyor.
         * Bu hatayı almamak için Singleton Repository Pattern kullanmak gerekiyor.
         * Bu yüzden RepositoryBase classı oluşturuldu ve buranın construckter ını protected olarak oluşturuyoruz ki dışarıdan new lenemesin.
        */

        protected static DatabaseContext context;/*TODO: private idi Repository classı buradan miras aldığı için protecteda çektik çünkü protected miras alınanda görülebilecektir.*/
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
                        context = new DatabaseContext();
                    }
                }
            }

            //return db; // miras öncesi bu method; public static DatabaseContext CreatedContext()  olduğu için return vardı onu da kapadık.
        }
    }
}