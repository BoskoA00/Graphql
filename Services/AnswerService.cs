using Graphql_server.Interfaces;
using Graphql_server.Types;
using Microsoft.EntityFrameworkCore;

namespace Graphql_server.Services
{
    public class AnswerService : AnswerInterface
    {
        private readonly IDbContextFactory<DatabaseContext> _databaseContext;
        public AnswerService(IDbContextFactory<DatabaseContext> databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<string> CreateAnswer(Answer answer)
        {
            try
            {
                var db = _databaseContext.CreateDbContext();

                db.Answers.Add(answer);
                await db.SaveChangesAsync();
                return "Da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteAnswer(Answer answer)
        {
           
            try
            {
                var db = _databaseContext.CreateDbContext();

                db.Answers.Remove(answer);
                await db.SaveChangesAsync();
                return "Da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Answer?> GetAnswerById(int id)
        {
            var db = _databaseContext.CreateDbContext();

            Answer? answer = await db.Answers.Where(a => a.Id == id).Include(a => a.User).Include(a => a.Question).FirstOrDefaultAsync();
            if (answer == null) { return null; }
            return answer;

        }

        public async Task<List<Answer>?> GetAnswers()
        {
            
            var db = _databaseContext.CreateDbContext();

            return await db.Answers.Include(a => a.User).Include(a => a.Question).ToListAsync();
        }

        public async Task<List<Answer>?> GetAnswersByQuestionId(int questionId)
        {
            var db = _databaseContext.CreateDbContext();

            Question? question = await db.Questions.Where(q => q.Id == questionId).FirstOrDefaultAsync();
            if (question == null) { return null; }
            return await db.Answers.Where(a => a.QuestionId == questionId).Include(a => a.User).ToListAsync();
        }

        public async Task<string> UpdateAnswer(Answer answer)
        {
            
            try
            {
                var db = _databaseContext.CreateDbContext();

                Answer? answerToUpdate = await db.Answers.Where(a => a.Id == answer.Id).FirstOrDefaultAsync();
                if (answerToUpdate == null) { return "ne"; }
                answerToUpdate.Content = answer.Content;
                await db.SaveChangesAsync();
                return "da";
            }
            catch (Exception ex) { return ex.Message; }
        }
    }
}
