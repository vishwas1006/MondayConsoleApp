using MondayConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MondayConsoleApp.Operations
{
    public class GetBoardsOperation : IOperation
    {
        private readonly MondayClient _mondayClient;

        public GetBoardsOperation(MondayClient mondayClient)
        {
            _mondayClient = mondayClient;
        }

        public async Task ExecuteAsync()
        {
            string query = @"{
                boards{
                        id
                        name
                      }
                }";

            string result = await _mondayClient.SendQueryAsync(query);

            using JsonDocument document = JsonDocument.Parse(result);

            var boards = document.RootElement.GetProperty("data").GetProperty("boards");

            foreach(var board in boards.EnumerateArray())
            {
                Console.WriteLine($"Board ID : {board.GetProperty("id").GetString()}");

                Console.WriteLine($"Board Name : {board.GetProperty("name").GetString()}");

                Console.WriteLine();
            }
        }

       
    }
}
