namespace WinFormClient
{
    partial class Form1
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
            this.btnAvengers = new System.Windows.Forms.Button();
            this.btnAvenger = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAvengers
            // 
            this.btnAvengers.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvengers.Location = new System.Drawing.Point(39, 32);
            this.btnAvengers.Name = "btnAvengers";
            this.btnAvengers.Size = new System.Drawing.Size(252, 89);
            this.btnAvengers.TabIndex = 0;
            this.btnAvengers.Text = "Avengers";
            this.btnAvengers.UseVisualStyleBackColor = true;
            this.btnAvengers.Click += new System.EventHandler(this.btnAvengers_Click);
            // 
            // btnAvenger
            // 
            this.btnAvenger.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvenger.Location = new System.Drawing.Point(39, 158);
            this.btnAvenger.Name = "btnAvenger";
            this.btnAvenger.Size = new System.Drawing.Size(252, 89);
            this.btnAvenger.TabIndex = 1;
            this.btnAvenger.Text = "Single Avenger";
            this.btnAvenger.UseVisualStyleBackColor = true;
            this.btnAvenger.Click += new System.EventHandler(this.btnAvenger_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAvenger);
            this.Controls.Add(this.btnAvengers);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAvengers;
        private System.Windows.Forms.Button btnAvenger;
    }
}

