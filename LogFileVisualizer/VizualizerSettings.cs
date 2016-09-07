//  Copyright(c) 2016 Brian Hansen.

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
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogFileVisualizerLib;

namespace LogFileVisualizer
{
    [DataContract]
    class VisualizerSettings
    {
        private const int _maxRecentSqlServers = 20;
        private const int _maxRecentQueries = 10;
        private static string _xmlFileName = GetXmlFileName();
        private static string _tempFileName = Path.Combine(
            Path.GetDirectoryName(_xmlFileName),
            Path.GetFileNameWithoutExtension(_xmlFileName) + ".tmp");
        private static bool _userSettingsOverwriteDecline = false;

        private static VisualizerSettings _instance;
        private static object _instanceLocker = new object();

        private static VisualizerSettings _clone;
        private static object _cloneLocker = new object();

        [DataMember]
        private List<string> _mostRecentSqlServers;

        private VisualizerSettings()
        {
            _mostRecentSqlServers = new List<string>();
        }

        public static VisualizerSettings Instance
        {
            get
            {
                lock(_instanceLocker)
                {
                    if (_instance == null)
                    {
                        _instance = Load();
                    }
                    return _instance;
                }
            }
        }

        public static VisualizerSettings Clone
        {
            get
            {
                lock(_cloneLocker)
                {
                    if (_clone == null)
                    {
                        _clone = GetClone(Instance);
                    }
                    return _clone;
                }
            }
        }

        [DataMember]
        public bool UserAgreesToDisclaimer
        {
            get;
            set;
        }

        public ReadOnlyCollection<string> MostRecentSqlServers
        {
            get
            {
                return new ReadOnlyCollection<string>(_mostRecentSqlServers);
            }
        }

        [DataMember]
        public bool TrackOptimizerInfo
        {
            get;
            set;
        }

        [DataMember]
        public bool TrackTransformationStats
        {
            get;
            set;
        }

        [DataMember]
        public LiveViewOptions LiveViewOptions
        {
            get;
            set;
        }

        [DataMember]
        public Color? ActiveVlfColor
        {
            get;
            set;
        }

        [DataMember]
        public Color? CurrentVlfColor
        {
            get;
            set;
        }

        [DataMember]
        public Color? InactiveVlfColor
        {
            get;
            set;
        }

        [DataMember]
        public Color? VlfFontColor
        {
            get;
            set;
        }

        [DataMember]
        public string VlfFontName
        {
            get;
            set;
        }

        [DataMember]
        public float? VlfFontSize
        {
            get;
            set;
        }

        public void Save()
        {
            if (_userSettingsOverwriteDecline == true)
            {
                return;
            }

            using (FileStream stream = new FileStream(_tempFileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(VisualizerSettings));
                serializer.WriteObject(stream, this);
            }
            if (File.Exists(_xmlFileName))
            {
                File.Delete(_xmlFileName);
            }
            File.Move(_tempFileName, _xmlFileName);
        }

        public void AddMostRecentSqlServer(string serverName)
        {
            if (string.IsNullOrEmpty(serverName))
            {
                throw new ArgumentNullException(nameof(serverName));
            }

            // If already in the list, move it to the top
            if (_mostRecentSqlServers.Contains(serverName))
            {
                _mostRecentSqlServers.Remove(serverName);
                _mostRecentSqlServers.Insert(0, serverName);
            }
            else
            {
                _mostRecentSqlServers.Insert(0, serverName);
            }

            if (_mostRecentSqlServers.Count > _maxRecentSqlServers)
            {
                _mostRecentSqlServers = _mostRecentSqlServers.Take(_maxRecentSqlServers).ToList();
            }
        }

        public static void CancelClone()
        {
            lock(_cloneLocker)
            {
                _clone = null;
            }
        }

        public static void PromoteClone()
        {
            lock(_instanceLocker)
            {
                _instance = Clone;
                lock (_cloneLocker)
                {
                    _clone = null;
                }
            }
        }

        private static VisualizerSettings GetClone(VisualizerSettings instance)
        {
            lock(_instanceLocker)
            {
                return Utilities.CreateClone(_instance);
            }
        }

        [OnDeserialized]
        private void PostDeserialize(StreamingContext context)
        {
            if (ActiveVlfColor == null)
            {
                ActiveVlfColor = Color.Red;
            }
            if (CurrentVlfColor == null)
            {
                CurrentVlfColor = Color.Yellow;
            }
            if (InactiveVlfColor == null)
            {
                InactiveVlfColor = Color.Green;
            }
            if (VlfFontColor == null)
            {
                VlfFontColor = Color.Black;
            }
            if (VlfFontName == null)
            {
                VlfFontName = "Times New Roman";
            }
            if (VlfFontSize == null)
            {
                VlfFontSize = 10;
            }
        }

        private static VisualizerSettings Load()
        {
            if (File.Exists(_xmlFileName) == false ||
                _userSettingsOverwriteDecline == true)
            {
                return new VisualizerSettings();
            }

            try
            {
                using (FileStream stream = new FileStream(_xmlFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(VisualizerSettings));
                    VisualizerSettings settings = serializer.ReadObject(stream) as VisualizerSettings;
                    return settings;
                }
            }
            catch (Exception ex)
            {
                string lineSpacing = Environment.NewLine + Environment.NewLine;
                string message = string.Format(
                    "The system was not able to load the current settings.  The specific error is: {0}." + lineSpacing +
                        "Do you want to revert to default settings?" + lineSpacing +
                        "Select Yes if you want to revert to the default settings.  You will likely lose any custom settings if you choose this option." + lineSpacing +
                        "Select No to end the program and manually correct the problem.",
                    ex.Message);
                DialogResult result = MessageBox.Show(
                    message,
                    "Unable to load settings",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    _userSettingsOverwriteDecline = true;
                    Application.Exit();
                }

                return new VisualizerSettings();
            }
        }

        private static string GetXmlFileName()
        {
            string path = Properties.Settings.Default.SettingsXmlFileName;
            if (Path.GetDirectoryName(path) == string.Empty)
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    path);
            }
            return path;
        }
    }
}
