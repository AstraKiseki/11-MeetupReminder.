using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetupReminder.Core.Service;

namespace MeetupReminder
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the name of the group you want to see meetups for.");
            string groupname = Console.ReadLine();

            var meetups = MeetupService.GetMeetupsFor(groupname).Result;

        }
    }
    }
}
