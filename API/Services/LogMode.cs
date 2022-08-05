using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    /// <summary>
    /// Implementation of consolelogging notification mode
    /// </summary>
    public class LogMode : INotificationMode
    {
        public void Notify(string message)
        {
            Console.WriteLine(message);
        }
    }
}