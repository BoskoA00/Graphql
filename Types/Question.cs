using System.ComponentModel.DataAnnotations.Schema;

namespace Graphql_server.Types
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Answer>? Answers { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
