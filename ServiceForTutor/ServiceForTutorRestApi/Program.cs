using Microsoft.OpenApi.Models;
using ServiceForTutorBusinessLogic.BusinessLogic;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorDatabaseImplements.Implements;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddLog4Net("log4net.config");

// Add services to the container.
builder.Services.AddControllers();

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
   "ServiceForTutorRestApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
