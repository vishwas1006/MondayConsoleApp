using DotNetEnv;
using MondayConsoleApp.Operations;
using MondayConsoleApp.Services;

Env.Load();

string? apiToken = Environment.GetEnvironmentVariable("MONDAY_API_TOKEN");

MondayClient mondayClient = new MondayClient(apiToken!);

Dictionary<int, IOperation> operations = new()
{
    {1, new GetBoardsOperation(mondayClient) },
    {2, new GetBoardByIdOperation(mondayClient) },
    { 3, new CreateItemOperation(mondayClient) },
    {4, new ListItemsOperation(mondayClient) },
    {5, new UpdateItemNameOperation(mondayClient) }
};

Console.WriteLine("1.Get Boards");
Console.WriteLine("2.Get Board By Id");
Console.WriteLine("3.Create Item");
Console.WriteLine("4.List Item Operation");
Console.WriteLine("5.Update Item Name");
Console.WriteLine("Choose Option:");

int choice = Convert.ToInt32(Console.ReadLine());

if (operations.ContainsKey(choice))
{
    await operations[choice].ExecuteAsync();
}
else
{
    Console.WriteLine("Invalid Choice");
}