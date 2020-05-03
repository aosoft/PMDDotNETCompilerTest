using System;
using System.ComponentModel;
using System.IO;
using Cocona;


namespace PMDDotNETCompilerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            CoconaLiteApp.Run<Program>(args);
        }

        [Description("Test MML Directory")]
        public void Test([Argument] string dir, string tooldir = ".")
        {
            var mmls = Directory.EnumerateFiles(dir, "*.mml", SearchOption.AllDirectories);
            foreach (var mml in mmls)
            {
                try
                {
                    Console.WriteLine(mml);
                    var dotnet = DotnetCompiler.Compile(mml);
                    Console.WriteLine("Dotnet compile - {0}", dotnet.binary != null ? "OK" : "NG");
                    var dos = DosCompiler.Compile(mml, dotnet.outputFileName, tooldir);
                    Console.WriteLine("DOS compile - {0}", dos.binary != null ? "OK" : "NG");

                    if (dotnet.binary != null && dos.binary != null)
                    {
                        int size = Math.Min(dotnet.binary.Length, dos.binary.Length);

                        bool eq = true;
                        for (int i = 0; i < size; i++)
                        {
                            if (dotnet.binary[i] != dos.binary[i])
                            {
                                Console.WriteLine("[{0:X4} {1:X2} -> {2:X2}", i, dos.binary[i], dotnet.binary[i]);
                                eq = false;
                            }
                        }

                        if (dotnet.binary.Length != dos.binary.Length)
                        {
                            Console.WriteLine("Binary Size is not equal");
                        }
                        else if (eq)
                        {
                            Console.WriteLine("The binaries matched.");
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
