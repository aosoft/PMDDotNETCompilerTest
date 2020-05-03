using System;
using Cocona;

namespace PMDDotNETCompilerTest
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
			CoconaLiteApp.Run<Program>(args);
		}
	}
}
