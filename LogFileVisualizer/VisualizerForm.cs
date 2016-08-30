using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogFileVisualizerLib;

namespace LogFileVisualizer
{
    public partial class VisualizerForm : Form
    {
        private ApplicationSqlConnection _connection = null;
        private DisplayMode _displayMode = DisplayMode.NotSet;

        private LiveViewVisualizer _liveViewVisualizer = null;

        public VisualizerForm()
        {
            InitializeComponent();
        }

        private void LiveViewMenuItem_Click(object sender, EventArgs e)
        {
            using (LiveViewOptionsForm form = new LiveViewOptionsForm(_connection))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    VisualizerSettings.Instance.LiveViewOptions.DisplaySurface = displayPictureBox;
                    _displayMode = DisplayMode.LiveView;

                    InitializeDisplay();

                    _liveViewVisualizer = new LiveViewVisualizer(VisualizerSettings.Instance.LiveViewOptions);

                    Thread liveViewThread = new Thread(_liveViewVisualizer.Start);
                    liveViewThread.IsBackground = true;
                    liveViewThread.Start();
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
            }

            stopButton.Enabled = false;
        }
    }
}
