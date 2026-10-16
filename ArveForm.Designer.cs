namespace Tooded_EFCore
{

    partial class ArveForm
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblToode = new System.Windows.Forms.Label();
            this.comboToode = new System.Windows.Forms.ComboBox();
            this.btnLisaKorvi = new System.Windows.Forms.Button();
            this.lblOstukorv = new System.Windows.Forms.Label();
            this.listOstukorv = new System.Windows.Forms.ListBox();
            this.btnEemalda = new System.Windows.Forms.Button();
            this.lblKokku = new System.Windows.Forms.Label();
            this.btnGenPDF = new System.Windows.Forms.Button();
            this.btnSaadaEmail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblToode
            //
            this.lblToode.AutoSize = true;
            this.lblToode.Location = new System.Drawing.Point(20, 20);
            this.lblToode.Name = "lblToode";
            this.lblToode.Size = new System.Drawing.Size(42, 15);
            this.lblToode.TabIndex = 0;
            this.lblToode.Text = "Toode:";
            //
            // comboToode
            //
            this.comboToode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboToode.FormattingEnabled = true;
            this.comboToode.Location = new System.Drawing.Point(20, 42);
            this.comboToode.Name = "comboToode";
            this.comboToode.Size = new System.Drawing.Size(400, 23);
            this.comboToode.TabIndex = 1;
            //
            // btnLisaKorvi
            //
            this.btnLisaKorvi.Location = new System.Drawing.Point(440, 40);
            this.btnLisaKorvi.Name = "btnLisaKorvi";
            this.btnLisaKorvi.Size = new System.Drawing.Size(120, 27);
            this.btnLisaKorvi.TabIndex = 2;
            this.btnLisaKorvi.Text = "Lisa korvi";
            this.btnLisaKorvi.UseVisualStyleBackColor = true;
            this.btnLisaKorvi.Click += new System.EventHandler(this.btnLisaKorvi_Click);
            //
            // lblOstukorv
            //
            this.lblOstukorv.AutoSize = true;
            this.lblOstukorv.Location = new System.Drawing.Point(20, 85);
            this.lblOstukorv.Name = "lblOstukorv";
            this.lblOstukorv.Size = new System.Drawing.Size(57, 15);
            this.lblOstukorv.TabIndex = 3;
            this.lblOstukorv.Text = "Ostukorv:";
            //
            // listOstukorv
            //
            this.listOstukorv.FormattingEnabled = true;
            this.listOstukorv.ItemHeight = 15;
            this.listOstukorv.Location = new System.Drawing.Point(20, 107);
            this.listOstukorv.Name = "listOstukorv";
            this.listOstukorv.Size = new System.Drawing.Size(540, 169);
            this.listOstukorv.TabIndex = 4;
            //
            // btnEemalda
            //
            this.btnEemalda.Location = new System.Drawing.Point(440, 285);
            this.btnEemalda.Name = "btnEemalda";
            this.btnEemalda.Size = new System.Drawing.Size(120, 27);
            this.btnEemalda.TabIndex = 5;
            this.btnEemalda.Text = "Eemalda";
            this.btnEemalda.UseVisualStyleBackColor = true;
            this.btnEemalda.Click += new System.EventHandler(this.btnEemalda_Click);
            //
            // lblKokku
            //
            this.lblKokku.AutoSize = true;
            this.lblKokku.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblKokku.Location = new System.Drawing.Point(20, 288);
            this.lblKokku.Name = "lblKokku";
            this.lblKokku.Size = new System.Drawing.Size(90, 21);
            this.lblKokku.TabIndex = 6;
            this.lblKokku.Text = "Kokku: 0 €";
            //
            // btnGenPDF
            //
            this.btnGenPDF.Location = new System.Drawing.Point(20, 335);
            this.btnGenPDF.Name = "btnGenPDF";
            this.btnGenPDF.Size = new System.Drawing.Size(150, 32);
            this.btnGenPDF.TabIndex = 7;
            this.btnGenPDF.Text = "Loo PDF";
            this.btnGenPDF.UseVisualStyleBackColor = true;
            this.btnGenPDF.Click += new System.EventHandler(this.btnGenPDF_Click);
            //
            // btnSaadaEmail
            //
            this.btnSaadaEmail.Location = new System.Drawing.Point(185, 335);
            this.btnSaadaEmail.Name = "btnSaadaEmail";
            this.btnSaadaEmail.Size = new System.Drawing.Size(150, 32);
            this.btnSaadaEmail.TabIndex = 8;
            this.btnSaadaEmail.Text = "Saada e-mailile";
            this.btnSaadaEmail.UseVisualStyleBackColor = true;
            this.btnSaadaEmail.Click += new System.EventHandler(this.btnSaadaEmail_Click);
            //
            // ArveForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 391);
            this.Controls.Add(this.btnSaadaEmail);
            this.Controls.Add(this.btnGenPDF);
            this.Controls.Add(this.lblKokku);
            this.Controls.Add(this.btnEemalda);
            this.Controls.Add(this.listOstukorv);
            this.Controls.Add(this.lblOstukorv);
            this.Controls.Add(this.btnLisaKorvi);
            this.Controls.Add(this.comboToode);
            this.Controls.Add(this.lblToode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Arve";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblToode;
        private System.Windows.Forms.ComboBox comboToode;
        private System.Windows.Forms.Button btnLisaKorvi;
        private System.Windows.Forms.Label lblOstukorv;
        private System.Windows.Forms.ListBox listOstukorv;
        private System.Windows.Forms.Button btnEemalda;
        private System.Windows.Forms.Label lblKokku;
        private System.Windows.Forms.Button btnGenPDF;
        private System.Windows.Forms.Button btnSaadaEmail;

    }
}