using MondayConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MondayConsoleApp.Operations
{
    public class ListItemsOperation : IOperation
    {

        private readonly MondayClient _mondayClient;

        public ListItemsOperation(MondayClient mondayClient)
        {
            _mondayClient = mondayClient;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Enter Board ID:");
            string? boardID = Console.ReadLine();

            string query = $@"  
            {{
                boards(ids:{boardID}){{ 
                    id
                    name
                        
                    items_page{{
                        items{{
                            id
                            name
                }}
            }}

        }}
}}";

            string result = await _mondayClient.SendQueryAsync(query);

            using JsonDocument document = JsonDocument.Parse(result);

            var boards = document.RootElement.GetProperty("data").GetProperty("boards");
            foreach (var board in boards.EnumerateArray())
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"Board : {board.GetProperty("name").GetString()}"
                );

                var items = board
                    .GetProperty("items_page")
                    .GetProperty("items");

                Console.WriteLine();

                foreach (var item in items.EnumerateArray())
                {
                    Console.WriteLine(
                        $"Item ID : {item.GetProperty("id").GetString()}"
                    );

                    Console.WriteLine(
                        $"Item Name : {item.GetProperty("name").GetString()}"
                    );

                    Console.WriteLine();
                }
            }


        }
    }
}