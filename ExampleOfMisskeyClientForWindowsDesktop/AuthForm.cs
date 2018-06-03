using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleOfMisskeyClientForWindowsDesktop
{
	public partial class AuthForm : Form
	{
		public AuthForm()
		{
			InitializeComponent();
		}

		public Func<Task<Misq.Me>> done
		{
			get;
			set;
		}

		private async void AuthForm_Load(object sender, EventArgs e)
		{
			var app = new Misq.App("https://misskey.xyz", "CLYauOEBwoyvlqLS1SvVJK970mSc1OAc");
			var me = await app.Authorize();
			
			this.Hide();

			var f = new MainForm(me);
			f.Show();
			f.FormClosed += (__1, __2) =>
			{
				this.Close();
			};
		}
	}
}
