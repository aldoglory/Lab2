using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }

    // ===================== LOGIN =====================
    public class LoginForm : Form
    {
        private readonly TextBox txtUser = new TextBox();
        private readonly TextBox txtPass = new TextBox();
        private readonly Button btnLogin = new Button();

        public LoginForm()
        {
            Text = "Login";
            ClientSize = new Size(300, 170);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            Controls.Add(new Label { Text = "Username:", Location = new Point(20, 28), AutoSize = true });
            txtUser.SetBounds(110, 25, 160, 25);
            Controls.Add(txtUser);

            Controls.Add(new Label { Text = "Password:", Location = new Point(20, 63), AutoSize = true });
            txtPass.SetBounds(110, 60, 160, 25);
            txtPass.UseSystemPasswordChar = true;
            Controls.Add(txtPass);

            btnLogin.Text = "Login";
            btnLogin.SetBounds(110, 105, 100, 30);
            btnLogin.Click += BtnLogin_Click;
            Controls.Add(btnLogin);
            AcceptButton = btnLogin;   // Enter key = Login
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "admin" && txtPass.Text == "1234")
            {
                txtPass.Clear();
                Hide();
                new ChooseForm(this).Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // ===================== CHOOSE =====================
    public class ChooseForm : Form
    {
        private readonly Form login;
        private bool navigating = false;

        public ChooseForm(Form login)
        {
            this.login = login;
            Text = "Choose";
            ClientSize = new Size(320, 140);
            StartPosition = FormStartPosition.CenterScreen;

            Controls.Add(new Label { Text = "Choose a program:", Location = new Point(20, 20), AutoSize = true });

            var btnBicycle = new Button { Text = "Bicycle" };
            btnBicycle.SetBounds(20, 60, 130, 40);
            btnBicycle.Click += (s, e) => OpenNext(new BicycleForm(login));
            Controls.Add(btnBicycle);

            var btnMushroom = new Button { Text = "Mushroom" };
            btnMushroom.SetBounds(170, 60, 130, 40);
            btnMushroom.Click += (s, e) => OpenNext(new MushroomForm(login));
            Controls.Add(btnMushroom);

            // Closing this window with X goes back to login
            FormClosed += (s, e) => { if (!navigating) login.Show(); };
        }

        private void OpenNext(Form next)
        {
            navigating = true;
            next.Show();
            Close();
        }
    }

    // ===================== PROBLEM 1: BICYCLE RENTAL =====================
    public class BicycleForm : Form
    {
        private readonly TextBox txtStart = new TextBox();
        private readonly TextBox txtEnd = new TextBox();
        private readonly TextBox txtTotal = new TextBox();

        public BicycleForm(Form login)
        {
            Text = "Bicycle Rental";
            ClientSize = new Size(330, 230);
            StartPosition = FormStartPosition.CenterScreen;

            Controls.Add(new Label { Text = "Start hour (0-23):", Location = new Point(20, 25), AutoSize = true });
            txtStart.SetBounds(160, 22, 140, 25);
            Controls.Add(txtStart);

            Controls.Add(new Label { Text = "End hour (1-24):", Location = new Point(20, 60), AutoSize = true });
            txtEnd.SetBounds(160, 57, 140, 25);
            Controls.Add(txtEnd);

            var btnCalc = new Button { Text = "Calculate" };
            btnCalc.SetBounds(160, 92, 140, 30);
            btnCalc.Click += BtnCalc_Click;
            Controls.Add(btnCalc);
            AcceptButton = btnCalc;

            Controls.Add(new Label { Text = "Total:", Location = new Point(20, 140), AutoSize = true });
            txtTotal.SetBounds(160, 137, 140, 25);
            txtTotal.ReadOnly = true;
            Controls.Add(txtTotal);

            var btnLogout = new Button { Text = "Logout" };
            btnLogout.SetBounds(20, 180, 100, 30);
            btnLogout.Click += (s, e) => Close();
            Controls.Add(btnLogout);

            FormClosed += (s, e) => login.Show();
        }

        private void BtnCalc_Click(object sender, EventArgs e)
        {
            // int.TryParse rejects decimal numbers like 7.5
            if (!int.TryParse(txtStart.Text.Trim(), out int start) ||
                !int.TryParse(txtEnd.Text.Trim(), out int end))
            {
                MessageBox.Show("Please enter whole numbers only (no decimals).", "Invalid input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (start < 0 || start > 23)
            {
                MessageBox.Show("Start hour must be between 0 and 23.", "Invalid input");
                return;
            }
            if (end < 1 || end > 24)
            {
                MessageBox.Show("End hour must be between 1 and 24.", "Invalid input");
                return;
            }
            if (start >= end)
            {
                MessageBox.Show("Start hour must be less than end hour.", "Invalid input");
                return;
            }

            int total = 0;
            for (int hour = start; hour < end; hour++)   // charge each hour separately
            {
                total += RateForHour(hour);
            }

            txtTotal.Text = total + " RWF";
        }

        // The hour that starts at h (h to h+1) is charged at:
        private static int RateForHour(int h)
        {
            if (h >= 14 && h < 19) return 1500;                          // 14h-19h
            if ((h >= 7 && h < 14) || (h >= 19 && h < 21)) return 1000;  // 7h-14h, 19h-21h
            return 500;                                                  // 0h-7h, 21h-24h
        }
    }

    // ===================== PROBLEM 2: MUSHROOM =====================
    public class MushroomForm : Form
    {
        private readonly Label lblResult = new Label();

        public MushroomForm(Form login)
        {
            Text = "Mushroom Guesser";
            ClientSize = new Size(340, 280);
            StartPosition = FormStartPosition.CenterScreen;

            Controls.Add(new Label
            {
                Text = "Think of one of these mushrooms:\n\n" +
                       "• Agaric jaunissant\n• Amanite tue-mouche\n• Cepe de Bordeaux\n" +
                       "• Coprin chevelu\n• Girolle\n• Pied bleu",
                Location = new Point(20, 15),
                AutoSize = true
            });

            var btnStart = new Button { Text = "Start" };
            btnStart.SetBounds(20, 160, 140, 30);
            btnStart.Click += BtnStart_Click;
            Controls.Add(btnStart);

            lblResult.SetBounds(20, 200, 300, 25);
            lblResult.Font = new Font(Font, FontStyle.Bold);
            Controls.Add(lblResult);

            var btnLogout = new Button { Text = "Logout" };
            btnLogout.SetBounds(20, 235, 100, 30);
            btnLogout.Click += (s, e) => Close();
            Controls.Add(btnLogout);

            FormClosed += (s, e) => login.Show();
        }

        private bool Ask(string question)
        {
            return MessageBox.Show(question, "Question", MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            string mushroom;

            // Never asks more than 3 questions
            if (Ask("Does your mushroom have a convex cup?"))
            {
                // Agaric jaunissant, Amanite tue-mouche, Pied bleu
                if (!Ask("Does your mushroom grow in a forest?"))
                    mushroom = "Agaric jaunissant";
                else if (Ask("Does your mushroom have a ring?"))
                    mushroom = "Amanite tue-mouche";
                else
                    mushroom = "Pied bleu";
            }
            else
            {
                // Cepe de Bordeaux, Coprin chevelu, Girolle
                if (!Ask("Does your mushroom grow in a forest?"))
                    mushroom = "Coprin chevelu";
                else if (Ask("Does your mushroom have gills?"))
                    mushroom = "Girolle";
                else
                    mushroom = "Cepe de Bordeaux";
            }

            lblResult.Text = "Your mushroom is: " + mushroom;
            MessageBox.Show("Your mushroom is: " + mushroom, "Result",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}