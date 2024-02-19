using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class DBCreatedOn
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
        private Repository<Category> repo_category = new Repository<Category>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        private Repository<Note> repo_note = new Repository<Note>();

        public DBCreatedOn()
        {
            /*
             * TODO: bu blok db ilk olultururken kullandığımız bloktu, repository class oluşturulduktan sonra burası kapatıldı...

            DataAccessLayer.DatabaseContext db = new DataAccessLayer.DatabaseContext();
            //db.Database.CreateIfNotExists(); Boş db oluşturmak için kullandık.
            db.Categories.ToList(); // MyInitializer i çağırıp verileri insert edebilmek için herhangi bir tabloya select atıyoruz. O initializeri tetikleyerek add işlemlerini yapmamıza sebep oluyor.
            */

            List<Category> categories = repo_category.List();
            //List<Category> categoriesFiltered = repo_category.List(z => z.Id > 5); // Çalışp çalışmadığının testi için çekiliyor.
        }

        public void Insert_Test()
        {
            {
                int result = repo_user.Insert(new EvernoteUser()
                {
                    Name = "TestName1",
                    Surname = "TestSurname1",
                    Email = "testmail1@mail.com",
                    AdtivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = true,
                    Username = "testuser1",
                    Password = "123",
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                    ModifiedUserName = "testuser1"
                });
            }
        }

        public void Update_Test()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "testuser1");
            if (user != null)
            {
                user.Username = "TestUser1";
                int result = repo_user.Save();
            }
        }

        public void Delete_Test()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "TestUser1");
            if (user != null)
            {
                int result = repo_user.Delete(user);
            }
        }

        public void Insert_Comment()
        {
            EvernoteUser user = repo_user.Find(x => x.Id == 1);
            Note note = repo_note.Find(z => z.Id == 3);

            Comment comment = new Comment()
            {
                Text = "Test commenti",
                CreatedOn = DateTime.Now,
                Note = note,
                Owner = user,
                ModifiedOn = DateTime.Now,
                ModifiedUserName = "cagatayerdogdu"
            };
            repo_comment.Insert(comment);
        }
    }
}