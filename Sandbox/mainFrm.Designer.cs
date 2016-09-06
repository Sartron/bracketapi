namespace Sandbox
{
    partial class mainFrm
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
            this.btnGetTournaments = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGetTournaments
            // 
            this.btnGetTournaments.Location = new System.Drawing.Point(12, 12);
            this.btnGetTournaments.Name = "btnGetTournaments";
            this.btnGetTournaments.Size = new System.Drawing.Size(97, 23);
            this.btnGetTournaments.TabIndex = 0;
            this.btnGetTournaments.Text = "Get Tournaments";
            this.btnGetTournaments.UseVisualStyleBackColor = true;
            this.btnGetTournaments.Click += new System.EventHandler(this.btnGetTournaments_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 424);
            this.Controls.Add(this.btnGetTournaments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sandbox";
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetTournaments;
    }
}

