namespace LogFileVisualizer
{
    partial class DataCollectionUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataCollectionUserControl));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseDBCCLOGINFO = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxUseDBCCLOGINFO);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 164);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DMV Usage";
            // 
            // checkBoxUseDBCCLOGINFO
            // 
            this.checkBoxUseDBCCLOGINFO.AutoSize = true;
            this.checkBoxUseDBCCLOGINFO.Location = new System.Drawing.Point(7, 77);
            this.checkBoxUseDBCCLOGINFO.Name = "checkBoxUseDBCCLOGINFO";
            this.checkBoxUseDBCCLOGINFO.Size = new System.Drawing.Size(161, 17);
            this.checkBoxUseDBCCLOGINFO.TabIndex = 1;
            this.checkBoxUseDBCCLOGINFO.Text = "Always use DBCC LOGINFO";
            this.checkBoxUseDBCCLOGINFO.UseVisualStyleBackColor = true;
            this.checkBoxUseDBCCLOGINFO.CheckedChanged += new System.EventHandler(this.CheckBoxUseDBCCLOGINFO_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(420, 53);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // DataCollectionUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DataCollectionUserControl";
            this.Size = new System.Drawing.Size(450, 285);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxUseDBCCLOGINFO;
        private System.Windows.Forms.Label label1;
    }
}
