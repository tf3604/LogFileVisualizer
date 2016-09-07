namespace LogFileVisualizer
{
    partial class DisplayUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.activeVlfLabel = new System.Windows.Forms.Label();
            this.activeVlfColorComboBox = new System.Windows.Forms.ColorComboBox();
            this.activeVlfCustomButton = new System.Windows.Forms.Button();
            this.currentVlfCustomButton = new System.Windows.Forms.Button();
            this.currentVlfColorComboBox = new System.Windows.Forms.ColorComboBox();
            this.currentVlfLabel = new System.Windows.Forms.Label();
            this.inactiveVlfCustomButton = new System.Windows.Forms.Button();
            this.inactiveVlfColorComboBox = new System.Windows.Forms.ColorComboBox();
            this.inactiveVlfLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // activeVlfLabel
            // 
            this.activeVlfLabel.AutoSize = true;
            this.activeVlfLabel.Location = new System.Drawing.Point(4, 4);
            this.activeVlfLabel.Name = "activeVlfLabel";
            this.activeVlfLabel.Size = new System.Drawing.Size(88, 13);
            this.activeVlfLabel.TabIndex = 0;
            this.activeVlfLabel.Text = "Active VLF color:";
            // 
            // activeVlfColorComboBox
            // 
            this.activeVlfColorComboBox.FormattingEnabled = true;
            this.activeVlfColorComboBox.Location = new System.Drawing.Point(125, 4);
            this.activeVlfColorComboBox.Name = "activeVlfColorComboBox";
            this.activeVlfColorComboBox.Size = new System.Drawing.Size(144, 21);
            this.activeVlfColorComboBox.TabIndex = 1;
            // 
            // activeVlfCustomButton
            // 
            this.activeVlfCustomButton.Location = new System.Drawing.Point(276, 1);
            this.activeVlfCustomButton.Name = "activeVlfCustomButton";
            this.activeVlfCustomButton.Size = new System.Drawing.Size(75, 23);
            this.activeVlfCustomButton.TabIndex = 2;
            this.activeVlfCustomButton.Text = "Custom ...";
            this.activeVlfCustomButton.UseVisualStyleBackColor = true;
            // 
            // currentVlfCustomButton
            // 
            this.currentVlfCustomButton.Location = new System.Drawing.Point(276, 31);
            this.currentVlfCustomButton.Name = "currentVlfCustomButton";
            this.currentVlfCustomButton.Size = new System.Drawing.Size(75, 23);
            this.currentVlfCustomButton.TabIndex = 5;
            this.currentVlfCustomButton.Text = "Custom ...";
            this.currentVlfCustomButton.UseVisualStyleBackColor = true;
            // 
            // currentVlfColorComboBox
            // 
            this.currentVlfColorComboBox.FormattingEnabled = true;
            this.currentVlfColorComboBox.Location = new System.Drawing.Point(125, 34);
            this.currentVlfColorComboBox.Name = "currentVlfColorComboBox";
            this.currentVlfColorComboBox.Size = new System.Drawing.Size(144, 21);
            this.currentVlfColorComboBox.TabIndex = 4;
            // 
            // currentVlfLabel
            // 
            this.currentVlfLabel.AutoSize = true;
            this.currentVlfLabel.Location = new System.Drawing.Point(4, 34);
            this.currentVlfLabel.Name = "currentVlfLabel";
            this.currentVlfLabel.Size = new System.Drawing.Size(92, 13);
            this.currentVlfLabel.TabIndex = 3;
            this.currentVlfLabel.Text = "Current VLF color:";
            // 
            // inactiveVlfCustomButton
            // 
            this.inactiveVlfCustomButton.Location = new System.Drawing.Point(275, 62);
            this.inactiveVlfCustomButton.Name = "inactiveVlfCustomButton";
            this.inactiveVlfCustomButton.Size = new System.Drawing.Size(75, 23);
            this.inactiveVlfCustomButton.TabIndex = 8;
            this.inactiveVlfCustomButton.Text = "Custom ...";
            this.inactiveVlfCustomButton.UseVisualStyleBackColor = true;
            // 
            // inactiveVlfColorComboBox
            // 
            this.inactiveVlfColorComboBox.FormattingEnabled = true;
            this.inactiveVlfColorComboBox.Location = new System.Drawing.Point(124, 65);
            this.inactiveVlfColorComboBox.Name = "inactiveVlfColorComboBox";
            this.inactiveVlfColorComboBox.Size = new System.Drawing.Size(144, 21);
            this.inactiveVlfColorComboBox.TabIndex = 7;
            // 
            // inactiveVlfLabel
            // 
            this.inactiveVlfLabel.AutoSize = true;
            this.inactiveVlfLabel.Location = new System.Drawing.Point(3, 65);
            this.inactiveVlfLabel.Name = "inactiveVlfLabel";
            this.inactiveVlfLabel.Size = new System.Drawing.Size(96, 13);
            this.inactiveVlfLabel.TabIndex = 6;
            this.inactiveVlfLabel.Text = "Inactive VLF color:";
            // 
            // DisplayUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.inactiveVlfCustomButton);
            this.Controls.Add(this.inactiveVlfColorComboBox);
            this.Controls.Add(this.inactiveVlfLabel);
            this.Controls.Add(this.currentVlfCustomButton);
            this.Controls.Add(this.currentVlfColorComboBox);
            this.Controls.Add(this.currentVlfLabel);
            this.Controls.Add(this.activeVlfCustomButton);
            this.Controls.Add(this.activeVlfColorComboBox);
            this.Controls.Add(this.activeVlfLabel);
            this.Name = "DisplayUserControl";
            this.Size = new System.Drawing.Size(450, 285);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label activeVlfLabel;
        private System.Windows.Forms.ColorComboBox activeVlfColorComboBox;
        private System.Windows.Forms.Button activeVlfCustomButton;
        private System.Windows.Forms.Button currentVlfCustomButton;
        private System.Windows.Forms.ColorComboBox currentVlfColorComboBox;
        private System.Windows.Forms.Label currentVlfLabel;
        private System.Windows.Forms.Button inactiveVlfCustomButton;
        private System.Windows.Forms.ColorComboBox inactiveVlfColorComboBox;
        private System.Windows.Forms.Label inactiveVlfLabel;
    }
}
