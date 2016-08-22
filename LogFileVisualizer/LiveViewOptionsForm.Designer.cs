namespace LogFileVisualizer
{
    partial class LiveViewOptionsForm
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
            this.connectToLabel = new System.Windows.Forms.Label();
            this.connectToTextBox = new System.Windows.Forms.TextBox();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.databaseTextBox = new System.Windows.Forms.TextBox();
            this.changeConnectionButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // connectToLabel
            // 
            this.connectToLabel.AutoSize = true;
            this.connectToLabel.Location = new System.Drawing.Point(13, 13);
            this.connectToLabel.Name = "connectToLabel";
            this.connectToLabel.Size = new System.Drawing.Size(62, 13);
            this.connectToLabel.TabIndex = 0;
            this.connectToLabel.Text = "Connect to:";
            // 
            // connectToTextBox
            // 
            this.connectToTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectToTextBox.Location = new System.Drawing.Point(123, 13);
            this.connectToTextBox.Name = "connectToTextBox";
            this.connectToTextBox.ReadOnly = true;
            this.connectToTextBox.Size = new System.Drawing.Size(279, 20);
            this.connectToTextBox.TabIndex = 1;
            // 
            // databaseLabel
            // 
            this.databaseLabel.AutoSize = true;
            this.databaseLabel.Location = new System.Drawing.Point(16, 41);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(56, 13);
            this.databaseLabel.TabIndex = 2;
            this.databaseLabel.Text = "Database:";
            // 
            // databaseTextBox
            // 
            this.databaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseTextBox.Location = new System.Drawing.Point(123, 41);
            this.databaseTextBox.Name = "databaseTextBox";
            this.databaseTextBox.ReadOnly = true;
            this.databaseTextBox.Size = new System.Drawing.Size(279, 20);
            this.databaseTextBox.TabIndex = 3;
            // 
            // changeConnectionButton
            // 
            this.changeConnectionButton.Location = new System.Drawing.Point(19, 76);
            this.changeConnectionButton.Name = "changeConnectionButton";
            this.changeConnectionButton.Size = new System.Drawing.Size(135, 23);
            this.changeConnectionButton.TabIndex = 4;
            this.changeConnectionButton.Text = "Change connection ...";
            this.changeConnectionButton.UseVisualStyleBackColor = true;
            this.changeConnectionButton.Click += new System.EventHandler(this.ChangeConnectionButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Refresh interval (seconds):";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(190, 122);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // LiveViewOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 279);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.changeConnectionButton);
            this.Controls.Add(this.databaseTextBox);
            this.Controls.Add(this.databaseLabel);
            this.Controls.Add(this.connectToTextBox);
            this.Controls.Add(this.connectToLabel);
            this.Name = "LiveViewOptionsForm";
            this.Text = "Live View Options";
            this.Load += new System.EventHandler(this.LiveViewOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label connectToLabel;
        private System.Windows.Forms.TextBox connectToTextBox;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.TextBox databaseTextBox;
        private System.Windows.Forms.Button changeConnectionButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}