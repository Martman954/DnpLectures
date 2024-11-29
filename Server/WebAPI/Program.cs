using EfcRepositories;
using FileRepositories;
using RepositoryContracts;
using AppContext = EfcRepositories.AppContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserFileRepository>();
builder.Services.AddScoped<IPostRepository, PostFileRepository>();
builder.Services.AddScoped<ICommentRepository, CommentFileRepository>();

// builder.Services.AddDbContext<AppContext>();
// builder.Services.AddScoped<IPostRepository, EfcPostRepository>();
// builder.Services.AddScoped<IUserRepository, EfcUserRepository>();
// builder.Services.AddScoped<ICommentRepository, EfcCommentRepository>();

var app = builder.Build();

app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();
app.Run();