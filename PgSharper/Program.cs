using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using PgSharper.Core;
using PgSharper.Core.GnuPG;

namespace PgSharper
{
    class Program
    {
        static void Main(string[] args)
        {


            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            Console.WriteLine(configuration["PgSettings:ExeLocation"]);

            IPgpTool tool = new GnuPGTool();

            GnuPGConfig.GnuPGExePath = configuration["PgSettings:ExeLocation"];

            var encryptArg = new FileDataInput
            {
                Armor = true,
                InputFile = @"d:\\shitforjacob.txt",
                OutputFile = @"d:\\shitforjacob.txt.gpg",
                Operation = DataOperation.Encrypt,
              //  Recipient = "0024E3B797E424FC0C64462B9A94B4AE200EE548",
              Recipient = configuration["PgSettings:Recipient"]
            };
            tool.ProcessData(encryptArg);




        }





    }
}
