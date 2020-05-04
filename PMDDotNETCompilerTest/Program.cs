using System;
using System.ComponentModel;
using System.IO;
using Cocona;
using Microsoft.Extensions.DependencyInjection;

namespace PMDDotNETCompilerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            CoconaApp.Create()
                .ConfigureServices(services =>
                {
                    services.AddTransient<PMDCompileTestService>();
                })
                .Run<Program>(args);
        }

        [Description("Test MML Directory")]
        public void Test([FromService] PMDCompileTestService service, [Argument] string dir, string tooldir = ".")
        {
            var mmls = Directory.EnumerateFiles(dir, "*.mml", SearchOption.AllDirectories);
            foreach (var mml in mmls)
            {
                service.SingleTest(mml, tooldir);
            }
        }
    }
}
