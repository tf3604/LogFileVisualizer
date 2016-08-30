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
        private LiveViewOptions _options;

        public LiveViewOptionsForm(ApplicationSqlConnection connection)
        {
            InitializeComponent();

            _options = VisualizerSettings.Instance.LiveViewOptions;
            if (_options == null)
            {
                _options = new LiveViewOptions();
            }

            if (connection != null)
            {
                _options.Connection = connection;
            }
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
            refreshIntervalSeconds.Value = _options.RefreshIntervalSeconds;
            if (_options.Layout == LiveViewOptions.LayoutStyle.Physical)
            {
                physicalLayoutRadioButton.Checked = true;
            }
            else
            {
                logicalLayoutRadioButton.Checked = true;
            }
            showVlfNumberCheckBox.Checked = _options.ShowVlfNumbers;
        }

        private void SetConnectionInfo()
        {
            connectToTextBox.Text = _options?.Connection?.InstanceName ?? _options?.InstanceName ?? string.Empty;                    
            databaseTextBox.Text = _options?.Connection?.DatabaseName ?? _options?.DatabaseName ?? string.Empty;
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _options.InstanceName = connectToTextBox.Text;
            _options.DatabaseName = databaseTextBox.Text;
            _options.RefreshIntervalSeconds = (int)refreshIntervalSeconds.Value;
            _options.Layout = physicalLayoutRadioButton.Checked ? LiveViewOptions.LayoutStyle.Physical : LiveViewOptions.LayoutStyle.Logical;
            _options.ShowVlfNumbers = showVlfNumberCheckBox.Checked;

            VisualizerSettings.Instance.LiveViewOptions = _options;
            VisualizerSettings.Instance.Save();

            if (_options.Connection == null &&
                _options.InstanceName != null &&
                _options.DatabaseName != null)
            {
                _options.Connection = new ApplicationSqlConnection(_options.InstanceName, _options.DatabaseName);
            }

            DialogResult = DialogResult.OK;
        }
    }
}
