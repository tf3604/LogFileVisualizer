using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogFileVisualizerLib;

namespace LogFileVisualizer
{
    internal static class LogRenderer
    {
        public static void Render(LogStatsDal dal, PictureBox pictureBox)
        {
            List<DbccLogInfoItem> vlfs = dal.ReadDbccLogInfo(null, true);
        }
    }
}
