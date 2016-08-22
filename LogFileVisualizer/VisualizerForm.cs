using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogFileVisualizerLib;

namespace LogFileVisualizer
{
    public partial class VisualizerForm : Form
    {
        private ApplicationSqlConnection _connection = null;

        public VisualizerForm()
        {
            InitializeComponent();
        }

        private void LiveViewMenuItem_Click(object sender, EventArgs e)
        {
            using (ConnectSqlForm form = new ConnectSqlForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _connection = form.Connection;
                }
            }
        }
    }
}
