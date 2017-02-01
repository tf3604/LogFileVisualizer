//  Copyright(c) 2016-2017 Brian Hansen.

//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//  documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//  the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
//  and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

//  The above copyright notice and this permission notice shall be included in all copies or substantial portions 
//  of the Software.

//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//  DEALINGS IN THE SOFTWARE.

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
