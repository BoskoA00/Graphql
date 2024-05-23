using System.ComponentModel.DataAnnotations.Schema;

namespace Graphql_server.Types
{
    public class Oglas
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PicturePath { get; set; }
        public int Price { get; set; }
        public float Size { get; set; }
        public int Type { get; set; }
        [ForeignKey(nameof(User))]
        public int userId { get; set; }
        public User User { get; set; }

    }
}
