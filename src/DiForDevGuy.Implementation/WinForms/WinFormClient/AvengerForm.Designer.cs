namespace WinFormClient
{
    partial class AvengerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSuperheroName = new System.Windows.Forms.Label();
            this.lblRealName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Avenger name:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(57, 101);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(316, 38);
            this.txtName.TabIndex = 1;
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGet.Location = new System.Drawing.Point(397, 73);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(249, 66);
            this.btnGet.TabIndex = 2;
            this.btnGet.Text = "Get Avenger";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(100, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "Superhero Name:";
            // 
            // lblSuperheroName
            // 
            this.lblSuperheroName.AutoSize = true;
            this.lblSuperheroName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuperheroName.Location = new System.Drawing.Point(344, 170);
            this.lblSuperheroName.Name = "lblSuperheroName";
            this.lblSuperheroName.Size = new System.Drawing.Size(84, 31);
            this.lblSuperheroName.TabIndex = 4;
            this.lblSuperheroName.Text = "          ";
            // 
            // lblRealName
            // 
            this.lblRealName.AutoSize = true;
            this.lblRealName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRealName.Location = new System.Drawing.Point(344, 222);
            this.lblRealName.Name = "lblRealName";
            this.lblRealName.Size = new System.Drawing.Size(84, 31);
            this.lblRealName.TabIndex = 6;
            this.lblRealName.Text = "          ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(100, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 31);
            this.label4.TabIndex = 5;
            this.label4.Text = "Real name:";
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPower.Location = new System.Drawing.Point(344, 279);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(84, 31);
            this.lblPower.TabIndex = 8;
            this.lblPower.Text = "          ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(100, 279);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 31);
            this.label6.TabIndex = 7;
            this.label6.Text = "Super power:";
            // 
            // AvengerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblPower);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblRealName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblSuperheroName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "AvengerForm";
            this.Text = "AvengerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSuperheroName;
        private System.Windows.Forms.Label lblRealName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Label label6;
    }
}