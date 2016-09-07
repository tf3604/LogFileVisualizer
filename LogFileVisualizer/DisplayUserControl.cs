using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
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
                //{ activeVlfCustomButton, activeVlfColorComboBox },
                //{ currentVlfCustomButton, currentVlfColorComboBox },
                //{ inactiveVlfCustomButton, inactiveVlfColorComboBox }
            };

            _buttonSettingsPropertyMapping = new Dictionary<Button, string>()
            {
                { activeVlfCustomButton, "ActiveVlfColor" },
                { currentVlfCustomButton, "CurrentVlfColor" },
                { inactiveVlfCustomButton, "InactiveVlfColor" }
            };
        }

        private void CustomButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (_buttonComboBoxMapping.ContainsKey(button) == false)
            {
                return;
            }
            ColorComboBox box = _buttonComboBoxMapping[button];

            if (box.Items.Count > 0)
            {
                using (ColorDialog dialog = new ColorDialog())
                {
                    dialog.AnyColor = true;
                    dialog.FullOpen = true;
                    dialog.Color = (Color)box.Items[0];
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        box.Items[0] = dialog.Color;
                    }
                }
            }
        }

        private void DisplayUserControl_Load(object sender, EventArgs e)
        {
            foreach (Button button in _buttonComboBoxMapping.Keys)
            {
                InitializeColors(button);
            }
        }

        private void InitializeColors(Button button)
        {
            ColorComboBox box = _buttonComboBoxMapping[button];
            string propertyName = _buttonSettingsPropertyMapping[button];

            box.Items.Clear();
            Type settingsType = typeof(VisualizerSettings);
            PropertyInfo cloneProperty = settingsType.GetProperty(propertyName);
            Color currentValue = (Color)cloneProperty.GetMethod.Invoke(VisualizerSettings.Clone, null);

            Type colorType = typeof(Color);
            List<PropertyInfo> colorProperties = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public).ToList();
            colorProperties.Sort((a, b) => a.Name.CompareTo(b.Name));
            foreach (PropertyInfo property in colorProperties)
            {
                Color color = (Color)property.GetMethod.Invoke(null, null);
                box.Items.Add(color);
            }

            box.SelectedIndex = 0;
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
