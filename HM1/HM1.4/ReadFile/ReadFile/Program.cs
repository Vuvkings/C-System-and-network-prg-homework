using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ReadFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string word = args[0]; //args[0]
                string filePath = args[1];
                int count = 0;

                string data = File.ReadAllText(filePath);
                //Console.WriteLine(data);
                var splitData = data.Split(' ', ',');
                foreach (string text in splitData)
                {
                    if (text == word) { count++; }

                }
                Console.WriteLine(count);
            }
            catch { }
            
        }
    }
}
