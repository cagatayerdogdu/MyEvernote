using MyEvernote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    // TODO: Bu class tüm tablolara tek bir yerden crud işlemleri yapabilmemiz için yazlmıştır.
    public class Repository<T> : RepositoryBase where T : class // Repository her gelen objenin tipini alabilmesi için generic<T> olarak tanımladık. Ayrıca int, string gibi tipte gönderilememesi için sadece class olarak filtre verdik.
    {
        //private DatabaseContext db = new DatabaseContext();
        ////private DatabaseContext db; // Singleton Repository sonrası bu haliyle çağırıyor olduk, yukardaki kodu kapadık. // Daha sonra bu satırıda kapadık, Altta TODO da burayı kapatmanın detaylı açıklaması mevcut.

        private DbSet<T> _objectSet; // burası DbSet yerine ObjectSet te olabilir çünkü DbSet objectset ten türeyen bir nesnedir.

        public Repository()
        {
            /* db = RepositoryBase.CreatedContext(); /* TODO: Singleton Repository sonrası bu haliyle çağırıyor olduk.
                                                   * Fakat en sonunda db nin buralardan çağırılması yerine bu classın RepositoryBase den miras almasını sağlayarak orada db tanımını oluşturarak
                                                   * buraya çağırıyoruz.
                                                   */

            _objectSet = context.Set<T>(); // Bu class dışarıdan her new lenerek çağırıldığında ilk başta gelen objeyi db den çekerek bir object tipinde değişkene atıyoruz. SOnra bu değişken üzerinde crud ları yapıyoruz.
                                           // Böylelikle db yi her seferinde sorgulayıp yormuyoruz.
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> whereKosulu) //Linq ile where koşulu yazarken bizden sistemin istediği expression buraya yazılarak gelen objede kıst yaparak veriyi alıyoruz.
        {
            return _objectSet.Where(whereKosulu).ToList();
        }

        /*
        public IQueryable List(Expression<Func<T, bool>> whereKosulu) //Yukarıdaki kıst ta list olarak sorguyu sonlandıryoruz fakat kullanıcı order by yada top 10 gibi veriyi isterse IQueryable kullanılıyor.
        {
            return _objectSet.Where(whereKosulu);
        }
        */

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }

        public int Update(T obj)
        {
            return Save(); // Update işlemi entity de select ile çekip sonra savechanges ile çağırmak olduğu için bu kadar yazılııyor.
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> whereKosulu) // Burada da yukarıdaki where clause larındaki gibi liste değil tek bir kayıt dönsün istiyorsak diye yazıyoruz.
        {
            return _objectSet.FirstOrDefault(whereKosulu);
        }
    }
}