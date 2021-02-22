namespace WinFormClient
{
    partial class AvengersForm
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
            this.components = new System.ComponentModel.Container();
            this.grdAvengers = new System.Windows.Forms.DataGridView();
            this.superheroServiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdAvengers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.superheroServiceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAvengers
            // 
            this.grdAvengers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAvengers.Location = new System.Drawing.Point(33, 58);
            this.grdAvengers.Name = "grdAvengers";
            this.grdAvengers.RowTemplate.Height = 24;
            this.grdAvengers.Size = new System.Drawing.Size(678, 340);
            this.grdAvengers.TabIndex = 0;
            // 
            // superheroServiceBindingSource
            // 
            this.superheroServiceBindingSource.DataSource = typeof(Lib.SuperheroService);
            // 
            // AvengersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grdAvengers);
            this.Name = "AvengersForm";
            this.Text = "AvengersForm";
            ((System.ComponentModel.ISupportInitialize)(this.grdAvengers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.superheroServiceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdAvengers;
        private System.Windows.Forms.BindingSource superheroServiceBindingSource;
    }
}