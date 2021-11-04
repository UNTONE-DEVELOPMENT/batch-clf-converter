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
                // user hasn't passed in any arguments (or hasnt passed enough, or too many) - just tell them how to use it
                Console.WriteLine("Usage: batch-clf-converter.exe <input> <output>");
                Environment.Exit(-2);
            }

            string input = args[0];
            string output = args[1];
            if (!Directory.Exists(input))
            {
                // input directory does not exist
                Console.WriteLine("Input directory does not exist. Please make sure this folder exists & has atleast one CLF file in it.");
                Environment.Exit(-1);
            }
            if (!Directory.Exists(output))
            {
                // output directory does not exist, create it for them
                Console.WriteLine("Output directory does not exist. I'll create it for you.\n");
                Directory.CreateDirectory(output);
            }
            DirectoryInfo d = new DirectoryInfo(input);
            foreach (var file in d.GetFiles("*.clf"))
            {
                Console.WriteLine("Converting " + file.Name + "... (" + input + "/" + file.Name + ")\n");

                clfFile newFile = clfUtils.convertLegacyFromFile(input + "/"  + file.Name);
                // create file using clf-utils

                Console.WriteLine("Done! Saving to " + file.Name + "... (" + output + file.Name + ")\n");

                File.WriteAllText(output + "/" + file.Name, clfUtils.saveClfFile(newFile));
                // save using clf-utils
            }
        }
    }
}
