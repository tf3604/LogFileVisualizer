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
    public partial class DisplayUserControl : UserControl
    {
        private Dictionary<Button, ColorComboBox> _buttonComboBoxMapping;
        private Dictionary<Button, string> _buttonSettingsPropertyMapping;

        public DisplayUserControl()
        {
            InitializeComponent();

            _buttonComboBoxMapping = new Dictionary<Button, ColorComboBox>()
            {
                { activeVlfCustomButton, activeVlfColorComboBox },
                { currentVlfCustomButton, currentVlfColorComboBox },
                { inactiveVlfCustomButton, inactiveVlfColorComboBox }
            };

            _buttonSettingsPropertyMapping = new Dictionary<Button, string>()
            {
                { activeVlfCustomButton, "ActiveVlfColor" },
                { currentVlfCustomButton, "CurrentVlfColor" },
                { inactiveVlfCustomButton, "InactiveVlfColor" }
            };
        }
    }
}
