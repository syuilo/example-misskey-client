using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace ExampleOfMisskeyClientForWindowsDesktop
{
	public partial class MainForm : Form
	{
		public Misq.Me me
		{
			get;
			set;
		}

		public MainForm(Misq.Me me)
		{
			this.me = me;
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.ActiveControl = this.textBox1;
			this.Text = "New post (@" + this.me.Username + ")";
			this.label1.Text = "Hey " + this.me.Name + ", whats up?";
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			await this.me.Request("notes/create", new Dictionary<string, object> {
				{ "text", this.textBox1.Text }
			});
			this.textBox1.Text = string.Empty;
		}
	}
}
