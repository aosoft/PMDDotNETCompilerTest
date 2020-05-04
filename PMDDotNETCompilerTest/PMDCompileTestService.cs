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
            _logger.LogInformation("---- Test Start - {0}", mmlFilePath);
            try
            {
                var dotnet = DotnetCompiler.Compile(mmlFilePath);
                _logger.LogInformation(".NET Compile Result: {0}", dotnet.result.Status);
                dotnet.result.WriteLog(_logger);

                var dos = DosCompiler.Compile(mmlFilePath, dotnet.outputFileName, tooldir);
                _logger.LogInformation("DOS Compile Result: {0}", dos.Status);
                dos.WriteLog(_logger);

                _logger.LogInformation("Compare Result: {0}", dotnet.result.Compare(dos));
            }
            catch(Exception e)
            {
                _logger.LogError(e.StackTrace);
            }
            finally
            {
                _logger.LogInformation("---- Test End - {0}", mmlFilePath);
            }
        }
    }
}
