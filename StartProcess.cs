using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Scanner
{
	// code from here: http://www.codeproject.com/Articles/19466/Launch-an-Executable-Programatically-on-Windows-Mo?msg=2991458#xx2991458xx

	public class StartProcess
	{
		public class ProcessInfo
		{
			public IntPtr hProcess;
			public IntPtr hThread;
			public IntPtr ProcessID;
			public IntPtr ThreadID;
		}

		[DllImport("CoreDll.DLL", SetLastError = true)]
		private static extern int CreateProcess(String imageName, String cmdLine, 
			IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, 
			Int32 boolInheritHandles, Int32 dwCreationFlags, IntPtr lpEnvironment, 
			IntPtr lpszCurrentDir, byte[] si, ProcessInfo pi);

		[DllImport("coredll")]
		private static extern bool CloseHandle(IntPtr hObject);

		[DllImport("coredll")]
		private static extern uint WaitForSingleObject
			(IntPtr hHandle, uint dwMilliseconds);

		[DllImport("coredll.dll", SetLastError = true)]
		private static extern int GetExitCodeProcess
			(IntPtr hProcess, ref int lpExitCode);


		public void Start(string path)
		{
			ProcessInfo pi = new ProcessInfo();
			byte[] si = new byte[128];
			CreateProcess(path, string.Empty, IntPtr.Zero, IntPtr.Zero, 
				0, 0, IntPtr.Zero, IntPtr.Zero, si, pi);

			int exitCode = 0;
			GetExitCodeProcess(pi.hProcess, ref exitCode);
			CloseHandle(pi.hProcess);
			CloseHandle(pi.hThread);
		}
	}
}
