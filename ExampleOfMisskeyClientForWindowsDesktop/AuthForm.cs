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
			var app = new Misq.App("CLYauOEBwoyvlqLS1SvVJK970mSc1OAc");
			this.done = await app.Authorize();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			var me = await this.done();

			this.Hide();

			var f = new MainForm(me);
			f.Show();
			f.FormClosed += (_1, _2) =>
			{
				this.Close();
			};
		}
	}
}
