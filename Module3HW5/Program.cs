using System;
using System.IO;
using System.Threading.Tasks;

namespace Module3HW5
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var helloNameFile = "Hello.txt";
            var worldNameFile = "World.txt";

            await CreateFiles(helloNameFile, worldNameFile);

            await ReadFromFiles(helloNameFile, worldNameFile);
        }

        public static async Task<string> ReadFromFiles(string nameFile1, string nameFile2)
        {
            var task1 = Task.Run(async () => await ReadFileAsync(nameFile1));
            var task2 = Task.Run(async () => await ReadFileAsync(nameFile2));

            await Task.WhenAll(new[] { task1, task2 });

            var result = $"{task1.Result} {task2.Result}";
            Console.WriteLine(result);
            return result;
        }

        public static async Task<string> ReadFileAsync(string nameFile)
        {
            using (StreamReader reader = new StreamReader(nameFile, false))
            {
                var result = await reader.ReadToEndAsync();
                result = result.Trim();
                Console.WriteLine($"Readed from file {nameFile} string: '{result}'");
                return result;
            }
        }

        public static async Task WriteAsync(string nameFile, string inputString)
        {
            if (!File.Exists(nameFile))
            {
                using (StreamWriter writer = new StreamWriter(nameFile, false))
                {
                    await writer.WriteLineAsync(inputString);
                }
            }
        }

        public static async Task CreateFiles(string nameFile1, string nameFile2)
        {
            await WriteAsync(nameFile1, $"this is text of file {nameFile1}");
            await WriteAsync(nameFile2, $"THIS IS TEXT OF FILE {nameFile2}");
        }
    }
}
