using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDDotNETCompilerTest;
using System;
using System.Diagnostics;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string MML = @"..\..\..\TEST.MML";
        private static readonly string DOSTOOLS = @"..\..\..\..\PMDDotNETCompilerTest\DOSTOOLS";

        [TestMethod]
        public void DosCompilerTestMethod()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var r = DosCompiler.Compile(MML, "TEST.M2", DOSTOOLS);
            Trace.WriteLine(r.Log);
            Assert.IsTrue(r.Status != CompileStatus.Warning);
            Assert.IsTrue(r.CompiledBinary != null);
        }

        [TestMethod]
        public void DotnetCompilerTestMethod()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var r = DotnetCompiler.Compile(MML);
            Trace.WriteLine(r.outputFileName);
            Trace.WriteLine(r.result.Log);
            Assert.IsTrue(r.result.Status != CompileStatus.Warning);
            Assert.IsTrue(r.result.CompiledBinary != null);
        }

        [TestMethod]
        public void CompileAndCompareTestMethod()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var r1 = DotnetCompiler.Compile(MML);
            Assert.IsTrue(r1.result.CompiledBinary != null);
            var r2 = DosCompiler.Compile(MML, r1.outputFileName, DOSTOOLS);
            Assert.IsTrue(r2.CompiledBinary != null);

            if (r1.result.CompiledBinary != null && r2.CompiledBinary != null)
            {
                Assert.IsTrue(r1.result.CompiledBinary.Length == r2.CompiledBinary.Length);
                bool eq = true;
                for (int i = 0; i < r1.result.CompiledBinary.Length; i++)
                {
                    if (r1.result.CompiledBinary[i] != r2.CompiledBinary[i])
                    {
                        Trace.WriteLine(string.Format("[{0:X4} {1:X2} -> {2:X2}", i, r1.result.CompiledBinary[i], r2.CompiledBinary[i]));
                        eq = false;
                    }
                }
                Assert.IsTrue(eq);
            }
        }
    }
}
