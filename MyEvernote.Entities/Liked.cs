using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Likes")]
    public class Liked
    {
        // TODO: Bir userın birden çok yorumu beğenebileceği ve bir kullanıcının birden çok notu olabileceğinden dolay Like tablosunda çoka çok ilişki olması gerektiği için, bu Liked isimli ara tablo oluşturuldu. Sql tarafında ki note ve user çokaçok ilişkisi bu ara tablodan yönetilecek.
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Note Note { get; set; }
        public virtual EvernoteUser LikedUser { get; set; }
    }
}