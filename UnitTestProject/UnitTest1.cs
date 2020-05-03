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
			Trace.WriteLine(r.stdout);
			Trace.WriteLine(r.stderr);
			Assert.IsTrue(r.binary != null);
		}

		[TestMethod]
		public void DotnetCompilerTestMethod()
		{
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

			var r = DotnetCompiler.Compile(MML);
			Trace.WriteLine(r.outputFileName);
			Trace.WriteLine(r.log);
			Assert.IsTrue(r.binary != null);
		}

		[TestMethod]
		public void CompileAndCompareTestMethod()
		{
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

			var r1 = DotnetCompiler.Compile(MML);
			Assert.IsTrue(r1.binary != null);
			var r2 = DosCompiler.Compile(MML, r1.outputFileName, DOSTOOLS);
			Assert.IsTrue(r2.binary != null);

			if (r1.binary != null && r2.binary != null)
			{
				Assert.IsTrue(r1.binary.Length == r2.binary.Length);
				bool eq = true;
				for (int i = 0; i < r1.binary.Length; i++)
				{
					if (r1.binary[i] != r2.binary[i])
					{
						Trace.WriteLine(string.Format("[{0:X4} {1:X2} -> {2:X2}", i, r1.binary[i], r2.binary[i]));
						eq = false;
					}
				}
				Assert.IsTrue(eq);
			}
		}
	}
}
