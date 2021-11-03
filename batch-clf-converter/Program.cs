using System;
using clf;
using System.IO;

namespace batch_clf_converter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: batch-clf-converter.exe <input> <output> [debug code " + args.Length + "]");
                Environment.Exit(-2);
            }
            //Console.WriteLine("Hello World! - " + args[0]);
            bool areClfs = false;
            string from = args[0];
            string to = args[1];
            if (!Directory.Exists(from))
            {
                Console.WriteLine("Input directory does not exist. Please make sure this folder exists & has atleast one CLF file in it.");
                Environment.Exit(-1);
            }
            if (!Directory.Exists(to))
            {
                Console.WriteLine("Output directory does not exist. I'll create it for you.\n");
                Directory.CreateDirectory(to);
            }
            DirectoryInfo d = new DirectoryInfo(from);
            foreach (var file in d.GetFiles("*.clf"))
            {
                Console.WriteLine("Converting " + file.Name + "... (" + from + "/" + file.Name + ")\n");
                clfFile newFile = clfUtils.convertLegacyFromFile(from + "/"  + file.Name);
                Console.WriteLine("Done! Saving to " + file.Name + "... (" + to + file.Name + ")\n");
                File.WriteAllText(to + "/" + file.Name, clfUtils.saveClfFile(newFile));
            }
        }
    }
}
