namespace LsnConverter
{
    partial class LsnConverterForm
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
            this.decimalLabel = new System.Windows.Forms.Label();
            this.decimalTextBox = new System.Windows.Forms.TextBox();
            this.hexadecimalSeparatedTextBox = new System.Windows.Forms.TextBox();
            this.hexadecimalSeparatedLabel = new System.Windows.Forms.Label();
            this.decimalSeparatedTextBox = new System.Windows.Forms.TextBox();
            this.decimalSeparatedLabel = new System.Windows.Forms.Label();
            this.hexadecimalTextBox = new System.Windows.Forms.TextBox();
            this.hexadecimalLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.zoomLabel = new System.Windows.Forms.ToolStripLabel();
            this.zoomComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // decimalLabel
            // 
            this.decimalLabel.AutoSize = true;
            this.decimalLabel.Location = new System.Drawing.Point(18, 42);
            this.decimalLabel.Name = "decimalLabel";
            this.decimalLabel.Size = new System.Drawing.Size(45, 13);
            this.decimalLabel.TabIndex = 0;
            this.decimalLabel.Text = "Decimal";
            // 
            // decimalTextBox
            // 
            this.decimalTextBox.Location = new System.Drawing.Point(161, 39);
            this.decimalTextBox.Name = "decimalTextBox";
            this.decimalTextBox.Size = new System.Drawing.Size(208, 20);
            this.decimalTextBox.TabIndex = 1;
            this.decimalTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            this.decimalTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // hexadecimalSeparatedTextBox
            // 
            this.hexadecimalSeparatedTextBox.Location = new System.Drawing.Point(161, 73);
            this.hexadecimalSeparatedTextBox.Name = "hexadecimalSeparatedTextBox";
            this.hexadecimalSeparatedTextBox.Size = new System.Drawing.Size(208, 20);
            this.hexadecimalSeparatedTextBox.TabIndex = 3;
            // 
            // hexadecimalSeparatedLabel
            // 
            this.hexadecimalSeparatedLabel.AutoSize = true;
            this.hexadecimalSeparatedLabel.Location = new System.Drawing.Point(18, 76);
            this.hexadecimalSeparatedLabel.Name = "hexadecimalSeparatedLabel";
            this.hexadecimalSeparatedLabel.Size = new System.Drawing.Size(120, 13);
            this.hexadecimalSeparatedLabel.TabIndex = 2;
            this.hexadecimalSeparatedLabel.Text = "Hexadecimal Separated";
            // 
            // decimalSeparatedTextBox
            // 
            this.decimalSeparatedTextBox.Location = new System.Drawing.Point(161, 142);
            this.decimalSeparatedTextBox.Name = "decimalSeparatedTextBox";
            this.decimalSeparatedTextBox.Size = new System.Drawing.Size(208, 20);
            this.decimalSeparatedTextBox.TabIndex = 7;
            // 
            // decimalSeparatedLabel
            // 
            this.decimalSeparatedLabel.AutoSize = true;
            this.decimalSeparatedLabel.Location = new System.Drawing.Point(18, 145);
            this.decimalSeparatedLabel.Name = "decimalSeparatedLabel";
            this.decimalSeparatedLabel.Size = new System.Drawing.Size(97, 13);
            this.decimalSeparatedLabel.TabIndex = 6;
            this.decimalSeparatedLabel.Text = "Decimal Separated";
            // 
            // hexadecimalTextBox
            // 
            this.hexadecimalTextBox.Location = new System.Drawing.Point(161, 108);
            this.hexadecimalTextBox.Name = "hexadecimalTextBox";
            this.hexadecimalTextBox.Size = new System.Drawing.Size(208, 20);
            this.hexadecimalTextBox.TabIndex = 5;
            // 
            // hexadecimalLabel
            // 
            this.hexadecimalLabel.AutoSize = true;
            this.hexadecimalLabel.Location = new System.Drawing.Point(18, 111);
            this.hexadecimalLabel.Name = "hexadecimalLabel";
            this.hexadecimalLabel.Size = new System.Drawing.Size(68, 13);
            this.hexadecimalLabel.TabIndex = 4;
            this.hexadecimalLabel.Text = "Hexadecimal";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomLabel,
            this.zoomComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(387, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // zoomLabel
            // 
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(39, 22);
            this.zoomLabel.Text = "Zoom";
            // 
            // zoomComboBox
            // 
            this.zoomComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zoomComboBox.Name = "zoomComboBox";
            this.zoomComboBox.Size = new System.Drawing.Size(75, 25);
            this.zoomComboBox.SelectedIndexChanged += new System.EventHandler(this.ZoomComboBox_SelectedIndexChanged);
            // 
            // LsnConverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 180);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.decimalSeparatedTextBox);
            this.Controls.Add(this.decimalSeparatedLabel);
            this.Controls.Add(this.hexadecimalTextBox);
            this.Controls.Add(this.hexadecimalLabel);
            this.Controls.Add(this.hexadecimalSeparatedTextBox);
            this.Controls.Add(this.hexadecimalSeparatedLabel);
            this.Controls.Add(this.decimalTextBox);
            this.Controls.Add(this.decimalLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LsnConverterForm";
            this.Text = "LSN Converter";
            this.Load += new System.EventHandler(this.LsnConverterForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label decimalLabel;
        private System.Windows.Forms.TextBox decimalTextBox;
        private System.Windows.Forms.TextBox hexadecimalSeparatedTextBox;
        private System.Windows.Forms.Label hexadecimalSeparatedLabel;
        private System.Windows.Forms.TextBox decimalSeparatedTextBox;
        private System.Windows.Forms.Label decimalSeparatedLabel;
        private System.Windows.Forms.TextBox hexadecimalTextBox;
        private System.Windows.Forms.Label hexadecimalLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel zoomLabel;
        private System.Windows.Forms.ToolStripComboBox zoomComboBox;
    }
}

