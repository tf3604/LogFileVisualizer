﻿//  Copyright(c) 2016-2017 Brian Hansen.

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
        private LayoutStyle _currentLayoutStyle = LayoutStyle.Physical;

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
                    VisualizerSettings.Instance.LiveViewOptions.StatusLabel = statusLabel;
                    VisualizerSettings.Instance.LiveViewOptions.ForceDbccLoginfo = false;
                    if (VisualizerSettings.Instance.AlwaysUseDbccLoginfo != null)
                    {
                        VisualizerSettings.Instance.LiveViewOptions.ForceDbccLoginfo = VisualizerSettings.Instance.AlwaysUseDbccLoginfo.Value;
                    }

                    _displayMode = DisplayMode.LiveView;

                    InitializeDisplay();

                    _liveViewVisualizer = new LiveViewVisualizer(VisualizerSettings.Instance.LiveViewOptions, _currentLayoutStyle);

                    Thread liveViewThread = new Thread(_liveViewVisualizer.Start);
                    liveViewThread.IsBackground = true;
                    liveViewThread.Start();
                }
            }
        }

        private void InitializeDisplay()
        {
            liveViewMenuItem.Enabled = false;
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

            liveViewMenuItem.Enabled = true;
            stopButton.Enabled = false;
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OptionsForm form = new OptionsForm())
            {
                form.ShowDialog();
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm form = new AboutForm())
            {
                form.ShowDialog();
            }
        }

        private void VisualizerForm_Load(object sender, EventArgs e)
        {
            if (VisualizerSettings.Instance.UserAgreesToDisclaimer == false)
            {
                using (DisclaimerForm disclaimer = new DisclaimerForm())
                {
                    if (disclaimer.ShowDialog() == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                    else
                    {
                        VisualizerSettings.Instance.UserAgreesToDisclaimer = true;
                        VisualizerSettings.Instance.Save();
                    }
                }
            }
        }

        private void PhysicalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentLayoutStyle = LayoutStyle.Physical;
            physicalToolStripMenuItem.Checked = true;
            logicalToolStripMenuItem.Checked = false;

            if (_liveViewVisualizer != null)
            {
                _liveViewVisualizer.LayoutStyle = _currentLayoutStyle;
            }
        }

        private void LogicalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentLayoutStyle = LayoutStyle.Logical;
            physicalToolStripMenuItem.Checked = false;
            logicalToolStripMenuItem.Checked = true;

            if (_liveViewVisualizer != null)
            {
                _liveViewVisualizer.LayoutStyle = _currentLayoutStyle;
            }
        }
    }
}
