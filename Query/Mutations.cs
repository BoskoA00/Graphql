using Graphql_server.Services;
using Graphql_server.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace Graphql_server.Query
{
    public class Mutations
    {
        private readonly UserService _userService;
        private readonly oglasService _oglasService;
        private readonly QuestionService _questionService;
        private readonly AnswerService _answerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Mutations(oglasService oglasService, UserService userService, QuestionService questionService, AnswerService answerService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _oglasService = oglasService;
            _questionService = questionService;
            _answerService = answerService;
            _webHostEnvironment = webHostEnvironment;
        }
        //user mutations
        public async Task<string> CreateUser(string firstName,string lastName, string password, int role, string imageName,string email,IFile? image)
        {
             try
            {
                
                UserService us = _userService;
                string imageNameWithoutSpaces = imageName.Replace(" ", "");

                string userDirectoryPath = System.IO.Path.Combine(_webHostEnvironment.WebRootPath, email);

                if (!Directory.Exists(userDirectoryPath))
                {
                    Directory.CreateDirectory(userDirectoryPath);
                }

                string imagePath = System.IO.Path.Combine(userDirectoryPath, imageNameWithoutSpaces);

                using (var stream = File.Create(imagePath))
                {
                    await image.CopyToAsync(stream);
                }

                string imageUrl = $"https://localhost:7081/{email}/{imageNameWithoutSpaces}";

                User newUser = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    Role = role,
                    ImageName = imageNameWithoutSpaces
                };
                await us.CreateUser(newUser);

                return "User created";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> DeleteUserById(int id)
        {

            try
            {

            UserService userService = _userService;

            User? userToDel = await userService.getUserById(id);

            if (userToDel == null)
            {
                return "Ne"; 
            }

            string userDirectoryPath = System.IO.Path.Combine(_webHostEnvironment.WebRootPath, userToDel.Email);
            if (Directory.Exists(userDirectoryPath))
            {
                Directory.Delete(userDirectoryPath, true); 
            }

            await userService.DeleteUser(userToDel);

            return "da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> UpdateUser(int id, string firstName, string lastName, string email, string password, int role, string imageName)
        {
            try
            {
            UserService us = _userService;
            User? userToUpdate = await us.getUserById(id);

            if (userToUpdate == null)
            {
                return "ne";
            }
            userToUpdate.FirstName = firstName;
            userToUpdate.LastName = lastName;
            userToUpdate.Email = email;
            userToUpdate.Password = password;
            userToUpdate.Role = role;
            userToUpdate.ImageName = imageName;

            await us.UpdateUser(userToUpdate);

            return "da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //oglas mutations
        public async Task<string> CreateOglas(string title, string city, string country, string picturePath, int price, float size, int type, int ownerId)
        {
            try
            {
                oglasService os = _oglasService;
                UserService us = _userService;
                User? owner = await us.getUserById(ownerId);
                if (owner == null)
                {
                    return "Ne";
                }
                Oglas oglasToCreate = new Oglas()
                {
                    Title = title,
                    City = city,
                    Country = country,
                    PicturePath = picturePath,
                    Price = price,
                    Size = size,
                    Type = type,
                    userId = ownerId,
                };

                await os.CreateOglas(oglasToCreate);
                return "Da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> DeleteOglasById(int id)
        {
            try
            {
                oglasService os = _oglasService;
            Oglas? oglasToDelete = await os.GetOglasById(id);
            if (oglasToDelete == null)
            {
                return "Ne";
            }
                await os.DeleteOglas(oglasToDelete);
                return "Da";
            
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> UpdateOglas(int id, string title, string city, string country, string picturePath, int price, float size, int type)
        {
            try
            {
                oglasService os = _oglasService;
            Oglas? oglasToUpdate = await os.GetOglasById(id);
            if (oglasToUpdate == null)
            {
                return "Ne";
            }
            oglasToUpdate.Title = title;
            oglasToUpdate.City = city;
            oglasToUpdate.Country = country;
            oglasToUpdate.PicturePath = picturePath;
            oglasToUpdate.Price = price;
            oglasToUpdate.Size = size;
            oglasToUpdate.Type = type;

            await os.UpdateOglas(oglasToUpdate);
            return "Da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //question mutations
        public async Task<string> CreateQuestion(string title, string content, int userId)
        {
            try
            {
                QuestionService qs = _questionService;
                UserService us = _userService;
                User? user = await us.getUserById(userId);
                if (user == null) { return "Ne"; }
                Question questonToCreate = new Question()
                {
                    Title = title,
                    Content = content,
                    UserId = userId,
                };
                await qs.CreateQuestion(questonToCreate);
                return "Da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> DeleteQuestionById(int id)
        {
            try
            {
                Question? questionToDelete = await _questionService.GetQuestionById(id);
                if (questionToDelete == null) { return "Ne"; }
                await _questionService.DeleteQuestion(questionToDelete);
                return "Da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> UpdateQuestion(int id, string title, string content)
        {
            try
            {
                QuestionService qs = _questionService;
                Question? questionToUpdate = await qs.GetQuestionById(id);
                if (questionToUpdate == null) { return "Ne"; }
                questionToUpdate.Title = title;
                questionToUpdate.Content = content;
                await qs.UpdateQuestion(questionToUpdate);
                return "Da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //answers mutations
        public async Task<string> CreateAnswer(string content, int userId, int questionId)
        {
            try
            {
                QuestionService qs = _questionService;
                UserService us = _userService;
                AnswerService anss = _answerService;
                User? user = await us.getUserById(userId);
                if (user == null) { return $"Nema korisnika sa id-jem {userId}!"; }
                Question? question = await qs.GetQuestionById(questionId);
                if (question == null) { return $"Nema  pitanja sa id-jem {questionId}!"; }
                Answer answer = new Answer()
                {
                    Content = content,
                    UserId = userId,
                    QuestionId = questionId
                };
                await anss.CreateAnswer(answer);
                return "Uspesno napravljen odgovor!";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> DeleteAnswer(int answerId)
        {
            try
            {
                AnswerService ass = _answerService;
                Answer? answer = await ass.GetAnswerById(answerId);
                if (answer == null) { return "Ne"; }
                await ass.DeleteAnswer(answer);
                return "Da";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> UpdateAnswer(int answerId, string content)
        {

            try
            {
                AnswerService ass = _answerService;
                Answer? answer = await ass.GetAnswerById(answerId);
                if (answer == null) { return "Ne"; }
                answer.Content = content;
                await ass.UpdateAnswer(answer);
                return "Da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
