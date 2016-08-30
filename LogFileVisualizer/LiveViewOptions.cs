using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogFileVisualizerLib;

namespace LogFileVisualizer
{
    [DataContract]
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

        [DataMember]
        public string InstanceName
        {
            get;
            set;
        }

        [DataMember]
        public string DatabaseName
        {
            get;
            set;
        }

        [DataMember]
        public int RefreshIntervalSeconds
        {
            get;
            set;
        }

        [DataMember]
        public LayoutStyle Layout
        {
            get;
            set;
        }

        [DataMember]
        public bool ShowVlfNumbers
        {
            get;
            set;
        }

        public PictureBox DisplaySurface
        {
            get;
            set;
        }

        private string _connectionString
        {
            get
            {
                return Connection?.Connection?.ConnectionString;
            }
            set
            {
                if (value != null)
                {
                    Connection = new ApplicationSqlConnection(value);
                }
            }
        }

        [DataContract]
        public enum LayoutStyle
        {
            [EnumMember]
            Physical,

            [EnumMember]
            Logical
        }
    }
}
