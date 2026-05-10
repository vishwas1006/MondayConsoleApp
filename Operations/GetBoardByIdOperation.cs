using MondayConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MondayConsoleApp.Operations
{
    using System.Text.Json;

    public class GetBoardByIdOperation : IOperation
    {
        private readonly MondayClient _mondayClient;

        public GetBoardByIdOperation(MondayClient mondayClient)
        {
            _mondayClient = mondayClient;
        }

        public async Task ExecuteAsync()
        {
            Console.Write("Enter Board ID : ");
            string? boardId = Console.ReadLine();

            string query = $@"
        {{
            boards(ids: {boardId!}) {{
                id
                name
            }}
        }}";

            string result =
                await _mondayClient.SendQueryAsync(query);

            using JsonDocument document =
                JsonDocument.Parse(result);

            var boards = document
                .RootElement
                .GetProperty("data")
                .GetProperty("boards");

            foreach (var board in boards.EnumerateArray())
            {
                Console.WriteLine();

                Console.WriteLine(
                    $"Board ID : {board.GetProperty("id").GetString()}"
                );

                Console.WriteLine(
                    $"Board Name : {board.GetProperty("name").GetString()}"
                );
            }
        }
    }
}
