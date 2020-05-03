using System;
using System.IO;
using System.Diagnostics;

namespace PMDDotNETCompilerTest
{
	public class DosCompiler
	{
		public static (string stdout, string stderr) Compile(string mmlFilePath, string tooldir)
		{
			var tooldirFull = Path.GetFullPath(tooldir);
			var currentDir = Environment.CurrentDirectory;
			try
			{
				var fullpath = Path.GetFullPath(mmlFilePath);
				var dir = Path.GetDirectoryName(fullpath);
				var fname = Path.GetFileName(fullpath);

				if (dir != null)
				{
					Environment.CurrentDirectory = dir;
				}

				var psi = new ProcessStartInfo()
				{
					FileName = Path.Combine(tooldirFull, "msdos"),
					Arguments = string.Format("{0} /v {1}", Path.Combine(tooldirFull, "MC"), fname),
					RedirectStandardOutput = true,
					RedirectStandardError = true
				};

				using (var p = Process.Start(psi))
				{
					var stdout = p.StandardOutput.ReadToEnd();
					var stderr = p.StandardError.ReadToEnd();
					p.WaitForExit();

					return (stdout, stderr);
				}
			}
			finally
			{
				Environment.CurrentDirectory = currentDir;
			}
		}
	}
}
