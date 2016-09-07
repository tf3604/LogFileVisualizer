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
            this.fontNameLabel = new System.Windows.Forms.Label();
            this.fontNameComboBox = new System.Windows.Forms.ComboBox();
            this.fontSizeLabel = new System.Windows.Forms.Label();
            this.fontSizeNumeric = new System.Windows.Forms.NumericUpDown();
            this.fontColorLabel = new System.Windows.Forms.Label();
            this.fontColorComboBox = new System.Windows.Forms.ColorComboBox();
            this.fontColorCustomButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fontSizeNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // activeVlfLabel
            // 
            this.activeVlfLabel.AutoSize = true;
            this.activeVlfLabel.Location = new System.Drawing.Point(4, 7);
            this.activeVlfLabel.Name = "activeVlfLabel";
            this.activeVlfLabel.Size = new System.Drawing.Size(88, 13);
            this.activeVlfLabel.TabIndex = 0;
            this.activeVlfLabel.Text = "Active VLF color:";
            // 
            // activeVlfColorComboBox
            // 
            this.activeVlfColorComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.activeVlfColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.activeVlfColorComboBox.FormattingEnabled = true;
            this.activeVlfColorComboBox.Location = new System.Drawing.Point(125, 4);
            this.activeVlfColorComboBox.Name = "activeVlfColorComboBox";
            this.activeVlfColorComboBox.Size = new System.Drawing.Size(144, 21);
            this.activeVlfColorComboBox.TabIndex = 1;
            this.activeVlfColorComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // activeVlfCustomButton
            // 
            this.activeVlfCustomButton.Location = new System.Drawing.Point(276, 2);
            this.activeVlfCustomButton.Name = "activeVlfCustomButton";
            this.activeVlfCustomButton.Size = new System.Drawing.Size(75, 23);
            this.activeVlfCustomButton.TabIndex = 2;
            this.activeVlfCustomButton.Text = "Custom ...";
            this.activeVlfCustomButton.UseVisualStyleBackColor = true;
            this.activeVlfCustomButton.Click += new System.EventHandler(this.CustomButton_Click);
            // 
            // currentVlfCustomButton
            // 
            this.currentVlfCustomButton.Location = new System.Drawing.Point(276, 32);
            this.currentVlfCustomButton.Name = "currentVlfCustomButton";
            this.currentVlfCustomButton.Size = new System.Drawing.Size(75, 23);
            this.currentVlfCustomButton.TabIndex = 5;
            this.currentVlfCustomButton.Text = "Custom ...";
            this.currentVlfCustomButton.UseVisualStyleBackColor = true;
            this.currentVlfCustomButton.Click += new System.EventHandler(this.CustomButton_Click);
            // 
            // currentVlfColorComboBox
            // 
            this.currentVlfColorComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.currentVlfColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currentVlfColorComboBox.FormattingEnabled = true;
            this.currentVlfColorComboBox.Location = new System.Drawing.Point(125, 34);
            this.currentVlfColorComboBox.Name = "currentVlfColorComboBox";
            this.currentVlfColorComboBox.Size = new System.Drawing.Size(144, 21);
            this.currentVlfColorComboBox.TabIndex = 4;
            this.currentVlfColorComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // currentVlfLabel
            // 
            this.currentVlfLabel.AutoSize = true;
            this.currentVlfLabel.Location = new System.Drawing.Point(4, 37);
            this.currentVlfLabel.Name = "currentVlfLabel";
            this.currentVlfLabel.Size = new System.Drawing.Size(92, 13);
            this.currentVlfLabel.TabIndex = 3;
            this.currentVlfLabel.Text = "Current VLF color:";
            // 
            // inactiveVlfCustomButton
            // 
            this.inactiveVlfCustomButton.Location = new System.Drawing.Point(276, 62);
            this.inactiveVlfCustomButton.Name = "inactiveVlfCustomButton";
            this.inactiveVlfCustomButton.Size = new System.Drawing.Size(75, 23);
            this.inactiveVlfCustomButton.TabIndex = 8;
            this.inactiveVlfCustomButton.Text = "Custom ...";
            this.inactiveVlfCustomButton.UseVisualStyleBackColor = true;
            this.inactiveVlfCustomButton.Click += new System.EventHandler(this.CustomButton_Click);
            // 
            // inactiveVlfColorComboBox
            // 
            this.inactiveVlfColorComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.inactiveVlfColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inactiveVlfColorComboBox.FormattingEnabled = true;
            this.inactiveVlfColorComboBox.Location = new System.Drawing.Point(125, 64);
            this.inactiveVlfColorComboBox.Name = "inactiveVlfColorComboBox";
            this.inactiveVlfColorComboBox.Size = new System.Drawing.Size(144, 21);
            this.inactiveVlfColorComboBox.TabIndex = 7;
            this.inactiveVlfColorComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // inactiveVlfLabel
            // 
            this.inactiveVlfLabel.AutoSize = true;
            this.inactiveVlfLabel.Location = new System.Drawing.Point(4, 67);
            this.inactiveVlfLabel.Name = "inactiveVlfLabel";
            this.inactiveVlfLabel.Size = new System.Drawing.Size(96, 13);
            this.inactiveVlfLabel.TabIndex = 6;
            this.inactiveVlfLabel.Text = "Inactive VLF color:";
            // 
            // fontNameLabel
            // 
            this.fontNameLabel.AutoSize = true;
            this.fontNameLabel.Location = new System.Drawing.Point(4, 97);
            this.fontNameLabel.Name = "fontNameLabel";
            this.fontNameLabel.Size = new System.Drawing.Size(60, 13);
            this.fontNameLabel.TabIndex = 9;
            this.fontNameLabel.Text = "Font name:";
            // 
            // fontNameComboBox
            // 
            this.fontNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontNameComboBox.FormattingEnabled = true;
            this.fontNameComboBox.Location = new System.Drawing.Point(125, 94);
            this.fontNameComboBox.Name = "fontNameComboBox";
            this.fontNameComboBox.Size = new System.Drawing.Size(225, 21);
            this.fontNameComboBox.TabIndex = 10;
            this.fontNameComboBox.SelectedIndexChanged += new System.EventHandler(this.FontNameComboBox_SelectedIndexChanged);
            // 
            // fontSizeLabel
            // 
            this.fontSizeLabel.AutoSize = true;
            this.fontSizeLabel.Location = new System.Drawing.Point(4, 127);
            this.fontSizeLabel.Name = "fontSizeLabel";
            this.fontSizeLabel.Size = new System.Drawing.Size(52, 13);
            this.fontSizeLabel.TabIndex = 11;
            this.fontSizeLabel.Text = "Font size:";
            // 
            // fontSizeNumeric
            // 
            this.fontSizeNumeric.DecimalPlaces = 2;
            this.fontSizeNumeric.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.fontSizeNumeric.Location = new System.Drawing.Point(125, 124);
            this.fontSizeNumeric.Maximum = new decimal(new int[] {
            144,
            0,
            0,
            0});
            this.fontSizeNumeric.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.fontSizeNumeric.Name = "fontSizeNumeric";
            this.fontSizeNumeric.Size = new System.Drawing.Size(60, 20);
            this.fontSizeNumeric.TabIndex = 12;
            this.fontSizeNumeric.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.fontSizeNumeric.ValueChanged += new System.EventHandler(this.FontSizeNumeric_ValueChanged);
            // 
            // fontColorLabel
            // 
            this.fontColorLabel.AutoSize = true;
            this.fontColorLabel.Location = new System.Drawing.Point(4, 157);
            this.fontColorLabel.Name = "fontColorLabel";
            this.fontColorLabel.Size = new System.Drawing.Size(57, 13);
            this.fontColorLabel.TabIndex = 13;
            this.fontColorLabel.Text = "Font color:";
            // 
            // fontColorComboBox
            // 
            this.fontColorComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.fontColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontColorComboBox.FormattingEnabled = true;
            this.fontColorComboBox.Location = new System.Drawing.Point(125, 154);
            this.fontColorComboBox.Name = "fontColorComboBox";
            this.fontColorComboBox.Size = new System.Drawing.Size(143, 21);
            this.fontColorComboBox.TabIndex = 14;
            this.fontColorComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // fontColorCustomButton
            // 
            this.fontColorCustomButton.Location = new System.Drawing.Point(276, 152);
            this.fontColorCustomButton.Name = "fontColorCustomButton";
            this.fontColorCustomButton.Size = new System.Drawing.Size(75, 23);
            this.fontColorCustomButton.TabIndex = 15;
            this.fontColorCustomButton.Text = "Custom ...";
            this.fontColorCustomButton.UseVisualStyleBackColor = true;
            this.fontColorCustomButton.Click += new System.EventHandler(this.CustomButton_Click);
            // 
            // DisplayUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fontColorCustomButton);
            this.Controls.Add(this.fontColorComboBox);
            this.Controls.Add(this.fontColorLabel);
            this.Controls.Add(this.fontSizeNumeric);
            this.Controls.Add(this.fontSizeLabel);
            this.Controls.Add(this.fontNameComboBox);
            this.Controls.Add(this.fontNameLabel);
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
            this.Load += new System.EventHandler(this.DisplayUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fontSizeNumeric)).EndInit();
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
        private System.Windows.Forms.Label fontNameLabel;
        private System.Windows.Forms.ComboBox fontNameComboBox;
        private System.Windows.Forms.Label fontSizeLabel;
        private System.Windows.Forms.NumericUpDown fontSizeNumeric;
        private System.Windows.Forms.Label fontColorLabel;
        private System.Windows.Forms.ColorComboBox fontColorComboBox;
        private System.Windows.Forms.Button fontColorCustomButton;
    }
}
