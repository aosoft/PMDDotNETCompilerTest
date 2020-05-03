using System;
using System.Collections.Generic;
using System.Text;
using musicDriverInterface;

namespace PMDDotNETCompilerTest
{
	public class DotnetCompiler
	{
		public static (byte[] binary, string outputFileName) Compile(string mmlFilePath)
		{
			Log.writeLine = WriteLine;



			return default;
		}

		static void WriteLine(LogLevel level, string msg)
		{
			System.Console.WriteLine("[{0,-7}] {1}", level, msg);
		}

	}
}
