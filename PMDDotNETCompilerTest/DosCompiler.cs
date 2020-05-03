using System;
using System.IO;
using System.Diagnostics;

namespace PMDDotNETCompilerTest
{
	public class DosCompiler
	{
		public static (byte[]? binary, string stdout, string stderr) Compile(string mmlFilePath, string outputFileName, string tooldir)
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

					if (p.ExitCode == 0)
					{
						byte[] buffer;
						using (var fs = new FileStream(outputFileName, FileMode.Open))
						{
							buffer = new byte[fs.Length];
							fs.Read(buffer, 0, buffer.Length);
						}
						File.Delete(outputFileName);

						return (buffer, stdout, stderr);
					}
					else
					{
						return (null, stdout, stderr);
					}
				}
			}
			finally
			{
				Environment.CurrentDirectory = currentDir;
			}
		}
	}
}
