using Graphql_server.Query;
using Graphql_server.Services;
using Graphql_server.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddCors();
builder.Services.AddPooledDbContextFactory<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("LocalConnection")));
builder.Services.AddGraphQLServer().AddQueryType<Query>().AddType<User>().AddType<Oglas>().AddType<Question>().AddType<Answer>().AddType<UploadType>().AddMutationType<Mutations>().AddMutationConventions();
builder.Services.AddTransient<Query>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<oglasService>();
builder.Services.AddTransient<QuestionService>();
builder.Services.AddTransient<AnswerService>();
var app = builder.Build();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapGraphQL();

app.Run();
