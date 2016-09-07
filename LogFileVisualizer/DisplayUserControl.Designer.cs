using System.ComponentModel;

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
            this.activeVlfColorLabel = new System.Windows.Forms.Label();
            this.activeVlfButton = new System.Windows.Forms.Button();
            this.currentVlfButton = new System.Windows.Forms.Button();
            this.currentVlfColorLabel = new System.Windows.Forms.Label();
            this.fontColorButton = new System.Windows.Forms.Button();
            this.fontColorLabel = new System.Windows.Forms.Label();
            this.inactiveVlfButton = new System.Windows.Forms.Button();
            this.inactiveVlfColorLabel = new System.Windows.Forms.Label();
            this.fontNameLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.fontSizeLabel = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // activeVlfColorLabel
            // 
            this.activeVlfColorLabel.AutoSize = true;
            this.activeVlfColorLabel.Location = new System.Drawing.Point(16, 30);
            this.activeVlfColorLabel.Name = "activeVlfColorLabel";
            this.activeVlfColorLabel.Size = new System.Drawing.Size(88, 13);
            this.activeVlfColorLabel.TabIndex = 0;
            this.activeVlfColorLabel.Text = "Active VLF color:";
            // 
            // activeVlfButton
            // 
            this.activeVlfButton.Location = new System.Drawing.Point(275, 25);
            this.activeVlfButton.Name = "activeVlfButton";
            this.activeVlfButton.Size = new System.Drawing.Size(75, 23);
            this.activeVlfButton.TabIndex = 2;
            this.activeVlfButton.Text = "Custom ...";
            this.activeVlfButton.UseVisualStyleBackColor = true;
            // 
            // currentVlfButton
            // 
            this.currentVlfButton.Location = new System.Drawing.Point(275, 63);
            this.currentVlfButton.Name = "currentVlfButton";
            this.currentVlfButton.Size = new System.Drawing.Size(75, 23);
            this.currentVlfButton.TabIndex = 5;
            this.currentVlfButton.Text = "Custom ...";
            this.currentVlfButton.UseVisualStyleBackColor = true;
            // 
            // currentVlfColorLabel
            // 
            this.currentVlfColorLabel.AutoSize = true;
            this.currentVlfColorLabel.Location = new System.Drawing.Point(16, 68);
            this.currentVlfColorLabel.Name = "currentVlfColorLabel";
            this.currentVlfColorLabel.Size = new System.Drawing.Size(92, 13);
            this.currentVlfColorLabel.TabIndex = 4;
            this.currentVlfColorLabel.Text = "Current VLF color:";
            // 
            // fontColorButton
            // 
            this.fontColorButton.Location = new System.Drawing.Point(275, 166);
            this.fontColorButton.Name = "fontColorButton";
            this.fontColorButton.Size = new System.Drawing.Size(75, 23);
            this.fontColorButton.TabIndex = 11;
            this.fontColorButton.Text = "Custom ...";
            this.fontColorButton.UseVisualStyleBackColor = true;
            // 
            // fontColorLabel
            // 
            this.fontColorLabel.AutoSize = true;
            this.fontColorLabel.Location = new System.Drawing.Point(16, 171);
            this.fontColorLabel.Name = "fontColorLabel";
            this.fontColorLabel.Size = new System.Drawing.Size(57, 13);
            this.fontColorLabel.TabIndex = 10;
            this.fontColorLabel.Text = "Font color:";
            // 
            // inactiveVlfButton
            // 
            this.inactiveVlfButton.Location = new System.Drawing.Point(275, 99);
            this.inactiveVlfButton.Name = "inactiveVlfButton";
            this.inactiveVlfButton.Size = new System.Drawing.Size(75, 23);
            this.inactiveVlfButton.TabIndex = 8;
            this.inactiveVlfButton.Text = "Custom ...";
            this.inactiveVlfButton.UseVisualStyleBackColor = true;
            // 
            // inactiveVlfColorLabel
            // 
            this.inactiveVlfColorLabel.AutoSize = true;
            this.inactiveVlfColorLabel.Location = new System.Drawing.Point(16, 104);
            this.inactiveVlfColorLabel.Name = "inactiveVlfColorLabel";
            this.inactiveVlfColorLabel.Size = new System.Drawing.Size(96, 13);
            this.inactiveVlfColorLabel.TabIndex = 7;
            this.inactiveVlfColorLabel.Text = "Inactive VLF color:";
            // 
            // fontNameLabel
            // 
            this.fontNameLabel.AutoSize = true;
            this.fontNameLabel.Location = new System.Drawing.Point(16, 137);
            this.fontNameLabel.Name = "fontNameLabel";
            this.fontNameLabel.Size = new System.Drawing.Size(60, 13);
            this.fontNameLabel.TabIndex = 13;
            this.fontNameLabel.Text = "Font name:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(148, 134);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(202, 21);
            this.comboBox1.TabIndex = 14;
            // 
            // fontSizeLabel
            // 
            this.fontSizeLabel.AutoSize = true;
            this.fontSizeLabel.Location = new System.Drawing.Point(19, 203);
            this.fontSizeLabel.Name = "fontSizeLabel";
            this.fontSizeLabel.Size = new System.Drawing.Size(52, 13);
            this.fontSizeLabel.TabIndex = 15;
            this.fontSizeLabel.Text = "Font size:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(148, 203);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown1.TabIndex = 16;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // DisplayUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "DisplayUserControl";
            this.Size = new System.Drawing.Size(450, 285);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label activeVlfColorLabel;
        private System.Windows.Forms.Button activeVlfButton;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private System.Windows.Forms.ColorComboBox activeVlfColorComboBox;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private System.Windows.Forms.ColorComboBox currentVlfColorComboBox;
        private System.Windows.Forms.Button currentVlfButton;
        private System.Windows.Forms.Label currentVlfColorLabel;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private System.Windows.Forms.ColorComboBox fontColorComboBox;
        private System.Windows.Forms.Button fontColorButton;
        private System.Windows.Forms.Label fontColorLabel;
        private System.Windows.Forms.ColorComboBox inactiveVlfColorComboBox;
        private System.Windows.Forms.Button inactiveVlfButton;
        private System.Windows.Forms.Label inactiveVlfColorLabel;
        private System.Windows.Forms.Label fontNameLabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label fontSizeLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}
