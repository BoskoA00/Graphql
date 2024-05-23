using Graphql_server.Types;

namespace Graphql_server.Interfaces
{
    public interface AnswerInterface
    {
        public Task<Answer?> GetAnswerById(int id);
        public Task<List<Answer>?> GetAnswers();
        public Task<List<Answer>?> GetAnswersByQuestionId(int questionId);
        public Task<string> CreateAnswer(Answer answer);
        public Task<string> DeleteAnswer(Answer answer);
        public Task<string> UpdateAnswer(Answer answer);
    }
}
