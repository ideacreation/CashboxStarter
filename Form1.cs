using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace Scanner
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
	
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.buttonHideWindow = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 64);
			this.textBox1.Multiline = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(512, 376);
			this.textBox1.Text = "";
			// 
			// buttonHideWindow
			// 
			this.buttonHideWindow.Location = new System.Drawing.Point(16, 16);
			this.buttonHideWindow.Size = new System.Drawing.Size(136, 32);
			this.buttonHideWindow.Text = "Fenster ausblenden";
			this.buttonHideWindow.Click += new System.EventHandler(this.buttonHideWindow_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
			this.buttonClose.Location = new System.Drawing.Point(168, 16);
			this.buttonClose.Size = new System.Drawing.Size(136, 32);
			this.buttonClose.Text = "Schliessen";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(626, 463);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonHideWindow);
			this.Controls.Add(this.textBox1);
			this.Text = "E-GUMA Cashbox Starter";
			this.Load += new System.EventHandler(this.Form1_Load);

		}
		#endregion

		private System.Windows.Forms.Button buttonHideWindow;
		private System.Windows.Forms.Button buttonClose;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static KeyInputListenerWinCE _listener;

		static void Main() 
		{
			Form1 form = new Form1();

			_listener = new KeyInputListenerWinCE();
			_listener.Init(form);

			Application.Run(form);
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			Hide();
		}

		public void LogNewLine(string text)
		{
			textBox1.Text += text + "\r\n";
		}

		public void Log(string text)
		{
			textBox1.Text += text;
		}

		private void buttonHideWindow_Click(object sender, System.EventArgs e)
		{
			Hide();
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			_listener.Release();
			Application.Exit();
		}
	}
}
