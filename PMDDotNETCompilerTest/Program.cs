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
            CoconaApp.Run<Program>(args);
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
                    Console.WriteLine("Dotnet compile - {0}", dotnet.result.CompiledBinary != null ? "OK" : "NG");
                    if (dotnet.result.CompiledBinary == null)
                    {
                        Console.WriteLine(dotnet.result.Log);
                    }
                    var dos = DosCompiler.Compile(mml, dotnet.outputFileName, tooldir);
                    Console.WriteLine("DOS compile - {0}", dos.CompiledBinary != null ? "OK" : "NG");
                    if (dos.CompiledBinary == null)
                    {
                        Console.WriteLine(dos.Log);
                    }

                    if (dotnet.result.CompiledBinary != null && dos.CompiledBinary != null)
                    {
                        int size = Math.Min(dotnet.result.CompiledBinary.Length, dos.CompiledBinary.Length);

                        bool eq = true;
                        for (int i = 0; i < size; i++)
                        {
                            if (dotnet.result.CompiledBinary[i] != dos.CompiledBinary[i])
                            {
                                Console.WriteLine("[{0:X4} {1:X2} -> {2:X2}", i, dos.CompiledBinary[i], dotnet.result.CompiledBinary[i]);
                                eq = false;
                            }
                        }

                        if (dotnet.result.CompiledBinary.Length != dos.CompiledBinary.Length)
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
