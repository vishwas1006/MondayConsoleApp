using MondayConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MondayConsoleApp.Operations
{
    public class UpdateItemNameOperation : IOperation
    {

        private readonly MondayClient _mondayClient;

        public UpdateItemNameOperation(MondayClient mondayClient)
        {
            _mondayClient = mondayClient;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Enter Board ID: ");
            string? boardId = Console.ReadLine();

            Console.WriteLine("Enter Item ID: ");
            string? itemId = Console.ReadLine();

            Console.WriteLine("Enter New Name for Item: ");
            string? newName = Console.ReadLine();

            string query = $@"  
                mutation {{
                    change_simple_column_value(
                        board_id:{boardId},
                        item_id : {itemId},
                        column_id : ""name""
                        value: ""{newName}""
            ){{
                id
                name
            }}
            }}";

            string result = await _mondayClient.SendQueryAsync(query);

            using JsonDocument document = JsonDocument.Parse(result);

            var updatedItem = document.RootElement.GetProperty("data").GetProperty("change_simple_column_value");

            Console.WriteLine();

            Console.WriteLine("Item Updated Successfully");

            Console.WriteLine($"Updated Item Id: {updatedItem.GetProperty("id").GetString()} ");

            Console.WriteLine($"Updated Item Name : {updatedItem.GetProperty("name").GetString()}");
        }
    }
}
