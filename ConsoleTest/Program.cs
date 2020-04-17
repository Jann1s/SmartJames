using JamesCore.Weather;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            JamesCore.Core manager = new JamesCore.Core();
            Console.ReadLine();
            manager.TriggerWeatherVoice();
            Console.ReadLine();
            manager.TriggerNewsVoice();
            Console.ReadLine();
        }
    }
}
