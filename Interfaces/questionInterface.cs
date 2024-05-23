using Graphql_server.Types;

namespace Graphql_server.Interfaces
{
    public interface questionInterface
    {
        public Task<Question?> GetQuestionById(int id);
        public Task<List<Question>?> GetQuestions();
        public Task<string> CreateQuestion(Question question);
        public Task<string> UpdateQuestion(Question question);
        public Task<string> DeleteQuestion(Question question);

    }
}
