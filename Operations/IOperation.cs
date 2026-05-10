using System;
using System.Collections.Generic;
using System.Text;

namespace MondayConsoleApp.Operations
{
    internal interface IOperation
    {
        Task ExecuteAsync();
    }
}
