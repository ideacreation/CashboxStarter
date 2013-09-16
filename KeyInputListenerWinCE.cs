using System;
using System.Runtime.InteropServices;
using Microsoft.WindowsCE.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace Scanner
{
	/// <summary>
	/// Summary description for KeyInputListenerWinCE.
	/// </summary>
	public class KeyInputListenerWinCE : MessageWindow
	{
		private const int WM_USER = 0x400; 
		private const int WM_ASYNCDATA_RECEIVED = WM_USER + 1;

		Form1 _form;
		string _inputStream;

		public void Init(Form1 form)
		{
			_form = form;

			UnmanagedKeyInputListener.StartWndCommunication(Hwnd);
		}

		public void Release()
		{
			UnmanagedKeyInputListener.StopWndCommunication();
		}

		protected override void WndProc(ref Message msg)
		{
			try
			{
				switch (msg.Msg) 
				{
					case WM_ASYNCDATA_RECEIVED:
						Receive(msg.LParam.ToInt32());
						break;
				}
				base.WndProc(ref msg);			
			}
			catch (Exception ex)
			{
				_form.LogNewLine(ex.ToString());
			}
		}

		private void Receive(int input)
		{
			_inputStream += (char)input;

			if (FoundCommand("STARTEGUMA"))
			{
				StartCashbox();
			}
			else if (FoundCommand("SHOW"))
			{
				_form.Show();
			}
		}

		private bool FoundCommand(string command)
		{
			if (_inputStream.Length < command.Length)
				return false;

			string sequence = _inputStream.Substring(_inputStream.Length - command.Length);

			if (sequence == command)
			{
				_inputStream = string.Empty;

				return true;
			}

			return false;
		}

		private void StartCashbox()
		{
			string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\";

			_form.LogNewLine(string.Format("starting Cashbox in dir '{0}'", dir));

			new StartProcess().Start(dir + "Cashbox.exe");
		}

		public class UnmanagedKeyInputListener
		{
			[DllImport("KeyInputListener.dll")]
			public static extern int StartWndCommunication(IntPtr hWnd);

			[DllImport("KeyInputListener.dll")] 
			public static extern void StopWndCommunication();
		}
	}
}
