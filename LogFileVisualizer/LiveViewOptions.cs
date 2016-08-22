using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogFileVisualizerLib;

namespace LogFileVisualizer
{
    public class LiveViewOptions
    {
        public LiveViewOptions()
        {
            Connection = null;
            RefreshIntervalSeconds = 60;
        }

        public ApplicationSqlConnection Connection
        {
            get;
            set;
        }

        public int RefreshIntervalSeconds
        {
            get;
            set;
        }

        public PictureBox DisplaySurface
        {
            get;
            set;
        }
    }
}
