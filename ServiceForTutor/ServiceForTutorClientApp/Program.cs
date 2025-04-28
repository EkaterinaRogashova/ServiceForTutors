using ServiceForTutorClientApp;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorDatabaseImplements.Implements;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Установите время жизни сеанса
    options.Cookie.HttpOnly = true; // Защита от JavaScript
    options.Cookie.IsEssential = true; // Требуется в некоторых сценариях
});

// Add services to the container.
builder.Services.AddControllersWithViews();
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
var app = builder.Build();
APIClient.Connect(builder.Configuration);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
