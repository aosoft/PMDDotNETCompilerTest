using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PMDDotNETCompilerTest
{
    public class PMDCompileTestService
    {
        private readonly ILogger _logger;

        public PMDCompileTestService(ILogger<PMDCompileTestService> logger)
        {
            _logger = logger;
        }

        public void SingleTest(string mmlFilePath, string tooldir)
        {
            try
            {
                _logger.LogInformation("Test Start - {0}", mmlFilePath);
                var dotnet = DotnetCompiler.Compile(mmlFilePath);
                _logger.LogInformation(".NET Compile Result: {0}", dotnet.result.Status);
                if (dotnet.result.Status != CompileStatus.Succeeded)
                {
                    _logger.LogInformation(dotnet.result.Log);
                }

                var dos = DosCompiler.Compile(mmlFilePath, dotnet.outputFileName, tooldir);
                _logger.LogInformation("DOS Compile Result: {0}", dos.Status);
                if (dos.Status != CompileStatus.Succeeded)
                {
                    _logger.LogInformation(dos.Log);
                }

                _logger.LogInformation("Compare Result: {0}", dotnet.result.Compare(dos));
            }
            catch(Exception e)
            {
                _logger.LogError(e.StackTrace);
            }
        }
    }
}
