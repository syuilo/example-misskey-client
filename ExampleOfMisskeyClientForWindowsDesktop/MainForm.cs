using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Windows.Forms;

namespace ExampleOfMisskeyClientForWindowsDesktop
{
	public partial class MainForm : Form
	{
		public string userkey
		{
			get;
			set; 
		}

		public dynamic user
		{
			get;
			set;
		}


		public MainForm(string userkey, dynamic user)
		{
			this.userkey = userkey;
			this.user = user;
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.ActiveControl = this.textBox1;
			this.Text = "New post (@" + this.user.username + ")";
			this.label1.Text = "Hey " + this.user.name + ", whats up?";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var wc = new System.Net.WebClient();
			wc.Headers.Add("userkey", this.userkey);
			var ps = new System.Collections.Specialized.NameValueCollection();
			ps.Add("text", this.textBox1.Text);
			var res = wc.UploadValues(Core.api + "/posts/create", ps);
			wc.Dispose();
			this.textBox1.Text = string.Empty;
		}
	}
}
