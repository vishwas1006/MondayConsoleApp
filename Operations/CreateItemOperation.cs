using MondayConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


namespace MondayConsoleApp.Operations
{
    public class CreateItemOperation:IOperation
    {
        private readonly MondayClient _mondayClient;

        public CreateItemOperation(MondayClient mondayClient)
        {
            _mondayClient = mondayClient;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Enter Board Id:");
            string? boardId = Console.ReadLine();

            Console.WriteLine("Enter Item Name:");
            string? itemName = Console.ReadLine();

            string query = $@"  
            mutation{{
                create_item(
                board_id:{boardId},
                item_name:""{itemName}""
            ){{
                id
                name
            }}
            }}";

            string result = await _mondayClient.SendQueryAsync(query);

            using JsonDocument document = JsonDocument.Parse(result);

            var createdItem = document.RootElement.GetProperty("data").GetProperty("create_item");

            Console.WriteLine();

            Console.WriteLine("Item crearted successfully");

            Console.WriteLine($"ID: {createdItem.GetProperty("id").GetString()}");
            Console.WriteLine($"Name : {createdItem.GetProperty("name").GetString()}");

            Console.WriteLine();
        }
    }
}
