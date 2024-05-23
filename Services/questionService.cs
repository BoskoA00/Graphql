using Azure;
using Graphql_server.Interfaces;
using Graphql_server.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graphql_server.Services
{
    public class QuestionService : questionInterface
    {
        private readonly IDbContextFactory<DatabaseContext> _databaseContext;
        public QuestionService(IDbContextFactory<DatabaseContext> contextFactory)
        {
            _databaseContext = contextFactory;
        }


        public async Task<string> CreateQuestion(Question question)
        {
            try
            {
                var db = _databaseContext.CreateDbContext();
                if (question == null)
                {
                    return "Ne";
                }
                
                db.Questions.Add(question);
                await db.SaveChangesAsync();
                return "Da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteQuestion(Question question)
        {
            try
            {
                var db = _databaseContext.CreateDbContext();
                if (question == null) { return "ne"; }
                
                db.Questions.Remove(question);

                await db.SaveChangesAsync();
                return "da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Question?> GetQuestionById(int id)
        {
            try
            {

                var db = _databaseContext.CreateDbContext();
                return await db.Questions.Where(q => q.Id == id).Include(x => x.User).Include(x => x.Answers).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("questionService:" + ex.Message);
                return null;
            }
        }

        public async Task<List<Question>?> GetQuestions()
        {
            var db = _databaseContext.CreateDbContext();
            return await db.Questions.Include(q => q.User).Include(q => q.Answers).ToListAsync();
        }

        public async Task<string> UpdateQuestion(Question question)
        {
            
            try
            {
            var db = _databaseContext.CreateDbContext();
            Question? questionToUpdate = await db.Questions.Where(q => q.Id == question.Id).FirstOrDefaultAsync();
            if (questionToUpdate == null) { return "Ne"; }
            questionToUpdate.Title = question.Title;
            questionToUpdate.Content = question.Content;
            await db.SaveChangesAsync();
            return "Da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
