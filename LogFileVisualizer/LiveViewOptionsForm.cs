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
    public partial class LiveViewOptionsForm : Form
    {
        private LiveViewOptions _options = new LiveViewOptions();

        public LiveViewOptionsForm(ApplicationSqlConnection connection)
        {
            InitializeComponent();

            _options.Connection = connection;
        }

        public LiveViewOptions Options
        {
            get
            {
                return _options;
            }
        }

        private void LiveViewOptions_Load(object sender, EventArgs e)
        {
            SetConnectionInfo();
        }

        private void SetConnectionInfo()
        {
            if (_options.Connection == null)
            {
                connectToTextBox.Text = string.Empty;
                databaseTextBox.Text = string.Empty;
            }
            else
            {
                connectToTextBox.Text = _options.Connection.InstanceName +
                    (_options.Connection.IsSqlAuthentication ? "(" + _options.Connection.UserName + ")" : string.Empty);
                databaseTextBox.Text = _options.Connection.DatabaseName;
            }
        }

        private void ChangeConnectionButton_Click(object sender, EventArgs e)
        {
            using (ConnectSqlForm form = new ConnectSqlForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _options.Connection = form.Connection;
                    SetConnectionInfo();
                }
            }
        }
    }
}
