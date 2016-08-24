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
        private LiveViewOptions _liveViewOptions = null;
        private DisplayMode _displayMode = DisplayMode.NotSet;

        private LiveViewVisualizer _liveViewVisualizer = null;

        public VisualizerForm()
        {
            InitializeComponent();
        }

        private void LiveViewMenuItem_Click(object sender, EventArgs e)
        {
            using (LiveViewOptionsForm form = new LiveViewOptionsForm(_liveViewOptions?.Connection ?? _connection))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _liveViewOptions = form.Options;
                    _liveViewOptions.DisplaySurface = displayPictureBox;
                    _displayMode = DisplayMode.LiveView;

                    InitializeDisplay();
                }
            }
        }

        private void InitializeDisplay()
        {
            stopButton.Enabled = true;
        }

        private enum DisplayMode
        {
            NotSet,
            LiveView,
            ReplayFromTable,
            ReplayFromFile
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (_liveViewVisualizer != null)
            {
                _liveViewVisualizer.Cancel();
                // TODO: What to do with the _liveViewVisualizer object?  Dispose immediately?
            }
        }
    }
}
