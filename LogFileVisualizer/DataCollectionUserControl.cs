using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogFileVisualizer
{
    public partial class DataCollectionUserControl : UserControl
    {
        public DataCollectionUserControl()
        {
            InitializeComponent();

            checkBoxUseDBCCLOGINFO.Checked = VisualizerSettings.Clone.AlwaysUseDbccLoginfo.Value;
        }

        private void CheckBoxUseDBCCLOGINFO_CheckedChanged(object sender, EventArgs e)
        {
            VisualizerSettings.Clone.AlwaysUseDbccLoginfo = checkBoxUseDBCCLOGINFO.Checked;
        }
    }
}
