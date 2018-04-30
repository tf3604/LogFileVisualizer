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

        public ToolStripStatusLabel StatusLabel
        {
            get;
            set;
        }

        public bool ForceDbccLoginfo
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
    }
}
