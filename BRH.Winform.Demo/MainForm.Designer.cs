namespace BRH.Winform.Demo
{
    partial class MainForm
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
            this.gridDisplay1 = new BRH.Winform.Controls.GridDisplay();
            this.SuspendLayout();
            // 
            // gridDisplay1
            // 
            this.gridDisplay1.BorderColor = System.Drawing.Color.Maroon;
            this.gridDisplay1.BorderWidth = 5;
            this.gridDisplay1.ColumnCount = 20;
            this.gridDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDisplay1.GridLineColor = System.Drawing.Color.Black;
            this.gridDisplay1.GridLineWidth = 2;
            this.gridDisplay1.Location = new System.Drawing.Point(0, 0);
            this.gridDisplay1.MinimumSize = new System.Drawing.Size(68, 68);
            this.gridDisplay1.Name = "gridDisplay1";
            this.gridDisplay1.RowCount = 20;
            this.gridDisplay1.Size = new System.Drawing.Size(719, 678);
            this.gridDisplay1.TabIndex = 0;
            this.gridDisplay1.UniformCell = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 678);
            this.Controls.Add(this.gridDisplay1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GridDisplay gridDisplay1;
    }
}