using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace ExampleOfMisskeyClientForWindowsDesktop
{
	public partial class AuthForm : Form
	{
		public AuthForm()
		{
			InitializeComponent();
		}

		public string token
		{
			get;
			set;
		}

		private void AuthForm_Load(object sender, EventArgs e)
		{
			var wc = new System.Net.WebClient();
			var ps = new System.Collections.Specialized.NameValueCollection();
			ps.Add("app_secret", Core.secret);
			var res = wc.UploadValues(Core.api + "/auth/session/generate", ps);
			wc.Dispose();
			var json = Encoding.UTF8.GetString(res);
			var obj = JsonConvert.DeserializeObject<dynamic>(json);
			this.token = obj.token.Value;

			System.Diagnostics.Process.Start(Core.auth + "/" + this.token);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var wc = new System.Net.WebClient();
			var ps = new System.Collections.Specialized.NameValueCollection();
			ps.Add("app_secret", Core.secret);
			ps.Add("token", this.token);
			var res = wc.UploadValues(Core.api + "/auth/session/userkey", ps);
			wc.Dispose();
			var json = Encoding.UTF8.GetString(res);
			var obj = JsonConvert.DeserializeObject<dynamic>(json);
			var userkey = obj.userkey.Value;

			var wc2 = new System.Net.WebClient();
			wc2.Headers.Add("userkey", userkey);
			var res2 = wc2.UploadValues(Core.api + "/i", new System.Collections.Specialized.NameValueCollection());
			wc2.Dispose();
			var json2 = Encoding.UTF8.GetString(res2);
			var obj2 = JsonConvert.DeserializeObject<dynamic>(json2);

			this.Hide();

			var f = new MainForm(userkey, obj2);
			f.Show();
			f.FormClosed += (x, y) =>
			{
				this.Close();
			};
		}
	}
}
