using System;
using System.IO;
using System.Text;

namespace TestDataset
{
    class Program
    {
        static void Main(string[] args)
        {
            var names = File.ReadAllLines("asteroids.txt");

            var phrases = new StringBuilder();

            for (var i = 0; i < 1000000; i++)
            {
                var numberOfWords = RandomHelper.NextRandom(new[] { 20, 10, 5 }) + 1;
                Console.WriteLine(numberOfWords);
                var phrase = names[RandomHelper.NextRandom(names.Length)];

                for (var j = 1; j < numberOfWords; j++)
                {
                    phrase += " " + names[RandomHelper.NextRandom(names.Length)];
                }

                phrases.AppendLine(phrase);
            }

            File.WriteAllText("phrases.txt", phrases.ToString());
        }
    }
}
