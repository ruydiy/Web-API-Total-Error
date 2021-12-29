using Data;
using Services.Implementation;
using System;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            FileReader fileReader = new FileReader(new ApplicationDbContext());
            var models = fileReader.ReadFileFromDirectory(@"D:\software engineering\TotalErrorFiles");

        }
    }
}
