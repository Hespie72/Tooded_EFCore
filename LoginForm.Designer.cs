namespace Tooded_EFCore
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblParool = new System.Windows.Forms.Label();
            this.txtParool = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblVeateade = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 20);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email:";
            //
            // txtEmail
            //
            this.txtEmail.Location = new System.Drawing.Point(20, 42);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(260, 23);
            this.txtEmail.TabIndex = 1;
            //
            // lblParool
            //
            this.lblParool.AutoSize = true;
            this.lblParool.Location = new System.Drawing.Point(20, 80);
            this.lblParool.Name = "lblParool";
            this.lblParool.Size = new System.Drawing.Size(48, 15);
            this.lblParool.TabIndex = 2;
            this.lblParool.Text = "Parool:";
            //
            // txtParool
            //
            this.txtParool.Location = new System.Drawing.Point(20, 102);
            this.txtParool.Name = "txtParool";
            this.txtParool.PasswordChar = '*';
            this.txtParool.Size = new System.Drawing.Size(260, 23);
            this.txtParool.TabIndex = 3;
            //
            // btnLogin
            //
            this.btnLogin.Location = new System.Drawing.Point(20, 145);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(260, 32);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Logi sisse";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            //
            // lblVeateade
            //
            this.lblVeateade.AutoSize = true;
            this.lblVeateade.ForeColor = System.Drawing.Color.Firebrick;
            this.lblVeateade.Location = new System.Drawing.Point(20, 190);
            this.lblVeateade.Name = "lblVeateade";
            this.lblVeateade.Size = new System.Drawing.Size(0, 15);
            this.lblVeateade.TabIndex = 5;
            //
            // LoginForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 225);
            this.Controls.Add(this.lblVeateade);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtParool);
            this.Controls.Add(this.lblParool);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sisselogimine";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblParool;
        private System.Windows.Forms.TextBox txtParool;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblVeateade;
    }
}
