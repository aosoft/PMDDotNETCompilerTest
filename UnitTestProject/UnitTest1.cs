using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMDDotNETCompilerTest;
using System;

namespace UnitTestProject
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void DosCompilerTestMethod()
		{
			var r = DosCompiler.Compile(@"..\..\..\TEST.MML", @"..\..\..\..\PMDDotNETCompilerTest\DOSTOOLS");
			Console.WriteLine(r.stdout);
			Console.WriteLine(r.stderr);
		}
	}
}
