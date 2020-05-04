using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PMDDotNETCompilerTest
{
    public enum CompileStatus
    {
        Succeeded = 0,
        Failed,
        Exception,
        Warning
    }

    public enum CompareResult
    {
        Unspecified = 0,
        Match,
        Unmatch,
        Match_NotEqualLength
    }

    public class CompileResult
    {
        public CompileStatus Status { get; }

        public byte[]? CompiledBinary { get; }

        public string Log { get; }

        public CompileResult(bool succeeded, byte[]? compiledBinary, string log)
        {
            if (succeeded)
            {
                if (log.IndexOf("Warning", StringComparison.CurrentCultureIgnoreCase) < 0)
                {
                    Status = CompileStatus.Succeeded;
                }
                else
                {
                    Status = CompileStatus.Warning;
                }
            }
            else
            {
                if (log.IndexOf("Exception", StringComparison.CurrentCultureIgnoreCase) < 0)
                {
                    Status = CompileStatus.Failed;
                }
                else
                {
                    Status = CompileStatus.Exception;
                }
            }
            CompiledBinary = compiledBinary;
            Log = log;
        }

        public void WriteLog(ILogger logger)
        {
            switch (Status)
            {
                case CompileStatus.Failed: logger.LogError(Log); break;
                case CompileStatus.Exception: logger.LogError(Log); break;
                case CompileStatus.Warning: logger.LogWarning(Log); break;
            }
        }

        public CompareResult Compare(CompileResult target)
        {
            if (Status == CompileStatus.Failed || target.Status == CompileStatus.Failed ||
                Status == CompileStatus.Exception || target.Status == CompileStatus.Exception ||
                CompiledBinary == null || target.CompiledBinary == null)
            {
                return CompareResult.Unspecified;
            }
            int size = Math.Min(CompiledBinary.Length, target.CompiledBinary.Length);

            for (int i = 0; i < size; i++)
            {
                if (CompiledBinary[i] != target.CompiledBinary[i])
                {
                    return CompareResult.Unmatch;
                }
            }

            return CompiledBinary.Length == target.CompiledBinary.Length ? CompareResult.Match : CompareResult.Match_NotEqualLength;
        }
    }
}
