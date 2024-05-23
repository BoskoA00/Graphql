using System.ComponentModel.DataAnnotations.Schema;

namespace Graphql_server.Types
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
