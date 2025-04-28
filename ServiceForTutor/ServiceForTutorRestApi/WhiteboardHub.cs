using Microsoft.AspNetCore.SignalR;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using System.Text.RegularExpressions;

public class WhiteboardHub : Hub
{
    private readonly IStudentWhiteboardStorage _storage;

    public WhiteboardHub(IStudentWhiteboardStorage storage)
    {
        _storage = storage;
    }

    public async Task JoinStudentRoom(int studentId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"student-{studentId}");

        // Загружаем сохраненную доску
        var board = _storage.GetElement(new StudentWhiteboardSearchModel { StudentId = studentId });
        if (board?.Data != null)
        {
            await Clients.Caller.SendAsync("ReceiveDrawingData", board.Data);
        }
    }

    public async Task SendDrawingData(int studentId, string jsonData)
    {
        // Сохраняем в базу
        var model = new StudentWhiteboardBindingModel
        {
            StudentId = studentId,
            Data = jsonData,
            LastUpdated = DateTime.UtcNow
        };
        _storage.Update(model);

        // Рассылаем обновление
        await Clients.Group($"student-{studentId}").SendAsync("ReceiveDrawingData", jsonData);
    }
}