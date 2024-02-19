using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        /* TODO: Sadece EF ile MSSQL ile çalışmak yerine tüm db lerle çalışabileceğimiz esnek yapı kurmak adına bu interface oluşturduk. Bu itemı oluşturduktan sonra repository.cs den methodları
         * kopyala yapıştır ile aldık ve içlerindeki kodları silerek sadece method isimlerini bıraktık.
         * Daha sonra EF klasörü altındaki Repository.cs bu interface den implemente edildi.
         * Interface olarak yaptığımız bu class soyut bir class olduğundan içindeki kodları siliyoruz. Az önce bahsettiğim implemente işlemi yaptğımız classta bu methodlarn kodları yer alıyor.
         */

        List<T> List();

        List<T> List(Expression<Func<T, bool>> whereKosulu); //Linq ile where koşulu yazarken bizden sistemin istediği expression buraya yazılarak gelen objede kıst yaparak veriyi alıyoruz.

        int Insert(T obj);

        int Update(T obj);

        int Delete(T obj);

        int Save();

        T Find(Expression<Func<T, bool>> whereKosulu); // Burada da yukarıdaki where clause larındaki gibi liste değil tek bir kayıt dönsün istiyorsak diye yazıyoruz.
    }
}