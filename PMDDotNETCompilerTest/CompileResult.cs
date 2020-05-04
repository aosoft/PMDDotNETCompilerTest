using System;
using System.Collections.Generic;
using System.Text;

namespace PMDDotNETCompilerTest
{
    public enum CompileStatus
    {
        Succeeded = 0,
        Failed,
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
                if (log.IndexOf("Warning") < 0)
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
                Status = CompileStatus.Failed;
            }
            CompiledBinary = compiledBinary;
            Log = log;
        }

        public CompareResult Compare(CompileResult target)
        {
            if (Status == CompileStatus.Failed || target.Status == CompileStatus.Failed ||
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
