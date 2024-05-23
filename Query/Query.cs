using Graphql_server.Services;
using Graphql_server.Types;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Graphql_server.Query
{
    public class Query
    {
        private readonly UserService _userService;
        private readonly oglasService _oglasService;
        private readonly QuestionService _questionService;
        private readonly AnswerService _answerService;
        public Query(UserService userService,oglasService OglasService, QuestionService questionService, AnswerService answerService)
        {
            _userService = userService;
            _oglasService = OglasService;
            _questionService = questionService;
            _answerService = answerService;
        }

        //user query
        public async Task<User?> GetUserById(int id)
        {
            return await _userService.getUserById(id);
        }
        public async Task<List<User>?> GetUsers()
        {
            return await _userService.getUsers();
        }
        //oglas query
        public async Task<Oglas?> GetOglasById(int id)
        {
            return await _oglasService.GetOglasById(id);
        }
        public async Task<List<Oglas>?> GetOglasi()
        {
            return await _oglasService.GetOglasi(); 
        }
        //question query
        public async Task<Question?> GetQuestionById(int id)
        {
            return await _questionService.GetQuestionById(id);
        }
        public async Task<List<Question>?> GetQuestions()
        {
            return await _questionService.GetQuestions();
        }
        //
        public async Task<Answer?> GetAnswerById(int id)
        {
            return await _answerService.GetAnswerById(id);
        }
        public async Task<List<Answer>?> GetAnswers()
        {
            return await _answerService.GetAnswers();
        }
        public async Task<List<Answer>?> GetAnswerByQuestionId(int questionId)
        {
            return await _answerService.GetAnswersByQuestionId(questionId);
        }
    }
    
}
