using Microsoft.OpenApi.Models;
using ServiceForTutorBusinessLogic.BusinessLogic;
using ServiceForTutorBusinessLogic.MailWorker;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorDatabaseImplements.Implements;
using ServiceForTutorRestApi;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddLog4Net("log4net.config");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ServiceForTutorRestApi",
        Version = "v1"
    });
});

builder.Services.AddTransient<IAssignedTaskStorage, AssignedTaskStorage>();
builder.Services.AddTransient<IInvitationCodeStorage, InvitationCodeStorage>();
builder.Services.AddTransient<IPurchasedTariffPlanStorage, PurchasedTariffPlanStorage>();
builder.Services.AddTransient<IQuestionStorage, QuestionStorage>();
builder.Services.AddTransient<IReviewStorage, ReviewStorage>();
builder.Services.AddTransient<IScheduleStorage, ScheduleStorage>();
builder.Services.AddTransient<IStudentAnswerStorage, StudentAnswerStorage>();
builder.Services.AddTransient<ITariffPlanStorage, TariffPlanStorage>();
builder.Services.AddTransient<ITaskStorage, TaskStorage>();
builder.Services.AddTransient<ITutorStudentStorage, TutorStudentStorage>();
builder.Services.AddTransient<IUserStorage, UserStorage>();
builder.Services.AddTransient<IStudentWhiteboardStorage, StudentWhiteboardStorage>();

builder.Services.AddTransient<IInvitationCodeLogic, InvitationCodeLogic>();
builder.Services.AddTransient<IAssignedTaskLogic, AssignedTaskLogic>();
builder.Services.AddTransient<IPurchasedTariffPlanLogic, PurchasedTariffPlanLogic>();
builder.Services.AddTransient<IQuestionLogic, QuestionLogic>();
builder.Services.AddTransient<IReviewLogic, ReviewLogic>();
builder.Services.AddTransient<IScheduleLogic, ScheduleLogic>();
builder.Services.AddTransient<IStudentAnswerLogic, StudentAnswerLogic>();
builder.Services.AddTransient<ITariffPlanLogic, TariffPlanLogic>();
builder.Services.AddTransient<ITaskLogic, TaskLogic>();
builder.Services.AddTransient<ITutorStudentLogic, TutorStudentLogic>();
builder.Services.AddTransient<IUserLogic, UserLogic>();
builder.Services.AddTransient<IStudentWhiteboardLogic, StudentWhiteboardLogic>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<AbstractMailWorker, MailKitWorker>();

var app = builder.Build();

var mailSender = app.Services.GetService<AbstractMailWorker>();

mailSender?.MailConfig(new MailConfigBindingModel
{
    MailLogin = builder.Configuration?.GetSection("MailLogin")?.Value?.ToString() ?? string.Empty,
    MailPassword = builder.Configuration?.GetSection("MailPassword")?.Value?.ToString() ?? string.Empty,
    SmtpClientHost = builder.Configuration?.GetSection("SmtpClientHost")?.Value?.ToString() ?? string.Empty,
    SmtpClientPort = Convert.ToInt32(builder.Configuration?.GetSection("SmtpClientPort")?.Value?.ToString()),
    PopHost = builder.Configuration?.GetSection("PopHost")?.Value?.ToString() ?? string.Empty,
    PopPort = Convert.ToInt32(builder.Configuration?.GetSection("PopPort")?.Value?.ToString())
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
   "ServiceForTutorRestApi v1"));
}
app.UseRouting();

app.UseHttpsRedirection();
app.UseCors(builder => builder
    .WithOrigins("https://localhost:7016") // Замените на ваш домен
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

app.UseAuthorization();

app.MapHub<WhiteboardHub>("/whiteboardHub");

app.MapControllers();

app.Run();
