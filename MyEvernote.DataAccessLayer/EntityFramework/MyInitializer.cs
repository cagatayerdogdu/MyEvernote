using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            // TODO : ilk başta veri oluşsun diye örnek kullancı ekliyoruz.
            EvernoteUser admin = new EvernoteUser()
            {
                Name = "Çağatay",
                Surname = "Erdoğdu",
                Email = "erdogdu3434@gmail.com",
                AdtivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "cagatayerdogdu",
                Password = "1453",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "cagatayerdogdu"
            };

            EvernoteUser standartUser = new EvernoteUser()
            {
                Name = "Çağatay",
                Surname = "Erdoğdu",
                Email = "erdogdu3434@gmail.com",
                AdtivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "erdogducagatay",
                Password = "1453",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "cagatayerdogdu"
            };

            context.EvernoteUsers.Add(admin);
            context.EvernoteUsers.Add(standartUser);

            for (int u = 0; u < 8; u++)
            {
                EvernoteUser user = new EvernoteUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    AdtivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{u}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"user{u}",
                };

                context.EvernoteUsers.Add(user);
            }

            context.SaveChanges();

            List<EvernoteUser> users = context.EvernoteUsers.ToList();

            // TODO: FakeData ile kategori ekliyoruz.

            for (int i = 0; i < 30; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "cagatayerdogdu"
                };
                context.Categories.Add(cat);

                // Adding fake notes
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    EvernoteUser owner = users[FakeData.NumberData.GetNumber(0, users.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        //Category = cat,
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        //(k % 2 == 0) ? admin : standartUser,    // bu for döngüsünün k item ının kökünü alarak çift se admin tekse standartUser olacak şekilde karışık owner versin diyoruz.
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now), // ister GetDateTime() diyerek kendisi random üretsin diyoruz istersek de şuan yaptığımız gibi 2 tarih arasında bi tarih seçsin diyoruz.
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = owner.Username
                    };
                    cat.Notes.Add(note);

                    // Adding fake comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        EvernoteUser commentOwner = users[FakeData.NumberData.GetNumber(0, users.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = commentOwner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUserName = commentOwner.Username
                        };
                        note.Comments.Add(comment);

                        // Adding fake likes
                        for (int l = 0; l < note.LikeCount; l++)
                        {
                            Liked liked = new Liked()
                            {
                                LikedUser = users[l]
                            };
                            note.Likes.Add(liked);
                        }
                    }
                }
            }
            context.SaveChanges();
        }
    }
}