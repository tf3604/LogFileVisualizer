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
            this.refreshIntervalSeconds = new System.Windows.Forms.NumericUpDown();
            this.startButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.showVlfNumberCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.refreshIntervalSeconds)).BeginInit();
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
            // refreshIntervalSeconds
            // 
            this.refreshIntervalSeconds.Location = new System.Drawing.Point(190, 122);
            this.refreshIntervalSeconds.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.refreshIntervalSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.refreshIntervalSeconds.Name = "refreshIntervalSeconds";
            this.refreshIntervalSeconds.Size = new System.Drawing.Size(61, 20);
            this.refreshIntervalSeconds.TabIndex = 6;
            this.refreshIntervalSeconds.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(22, 200);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 7;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(104, 200);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // showVlfNumberCheckBox
            // 
            this.showVlfNumberCheckBox.AutoSize = true;
            this.showVlfNumberCheckBox.Checked = true;
            this.showVlfNumberCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showVlfNumberCheckBox.Location = new System.Drawing.Point(22, 162);
            this.showVlfNumberCheckBox.Name = "showVlfNumberCheckBox";
            this.showVlfNumberCheckBox.Size = new System.Drawing.Size(189, 17);
            this.showVlfNumberCheckBox.TabIndex = 10;
            this.showVlfNumberCheckBox.Text = "Show VLF number if space permits";
            this.showVlfNumberCheckBox.UseVisualStyleBackColor = true;
            // 
            // LiveViewOptionsForm
            // 
            this.AcceptButton = this.startButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(414, 244);
            this.Controls.Add(this.showVlfNumberCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.refreshIntervalSeconds);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.changeConnectionButton);
            this.Controls.Add(this.databaseTextBox);
            this.Controls.Add(this.databaseLabel);
            this.Controls.Add(this.connectToTextBox);
            this.Controls.Add(this.connectToLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LiveViewOptionsForm";
            this.Text = "Live View Options";
            this.Load += new System.EventHandler(this.LiveViewOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.refreshIntervalSeconds)).EndInit();
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
        private System.Windows.Forms.NumericUpDown refreshIntervalSeconds;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox showVlfNumberCheckBox;
    }
}