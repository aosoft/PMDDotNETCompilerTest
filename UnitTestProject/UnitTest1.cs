using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDDotNETCompilerTest;
using System;
using System.Diagnostics;

namespace UnitTestProject
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void DosCompilerTestMethod()
		{
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

			var r = DosCompiler.Compile(@"..\..\..\TEST.MML", "TEST.M2", @"..\..\..\..\PMDDotNETCompilerTest\DOSTOOLS");
			Trace.WriteLine(r.stdout);
			Trace.WriteLine(r.stderr);
			Assert.IsTrue(r.binary != null);
		}

		[TestMethod]
		public void DotnetCompilerTestMethod()
		{
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

			var r = DotnetCompiler.Compile(@"..\..\..\TEST.MML");
			Trace.WriteLine(r.outputFileName);
			Trace.WriteLine(r.log);
			Assert.IsTrue(r.binary != null);
		}
	}
}
