using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexTester
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            string regex = @"^[-+]?\d*\.?\d{0,2}$";

            list.Add("");
            list.Add("+1.2");
            list.Add("-1.23");
            list.Add("1.2");
            list.Add("1.23");
            list.Add(".2");
            list.Add("-.23");
            list.Add("+1.234");
            list.Add("-1.234");
            list.Add("1.234");
            list.Add("1.234");
            list.Add(".234");
            list.Add("-.234");
            list.Add("+11.2");
            list.Add("-11.23");
            list.Add("11.2");
            list.Add("11.23");
            list.Add("+111111111111.2");
            list.Add("-111111111111.23");
            list.Add("111111111111.2");
            list.Add("111111111111.23");


            foreach (string item in list)
            {
                Console.WriteLine("{0} {1}", item, Regex.IsMatch(item, regex));
            }
            Console.ReadKey();
        }
    }
}
