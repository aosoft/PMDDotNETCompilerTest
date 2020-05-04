using System;
using System.Collections.Generic;
using System.Text;

namespace PMDDotNETCompilerTest
{
    public class CompileResult
    {
        public bool Succeed { get; }

        public byte[]? CompiledBinary { get; }

        public string Log { get; }

        public CompileResult(bool succeeded, byte[]? compiledBinary, string log)
        {
            Succeed = succeeded;
            CompiledBinary = compiledBinary;
            Log = log;
        }
    }
}
