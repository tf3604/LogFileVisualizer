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
        private static List<Color> _knownColors = null;
        private static List<string> _knownFonts = null;
        private static object _initializerLocker = new object();

        private Dictionary<Button, ColorComboBox> _buttonComboBoxMapping;
        private Dictionary<Button, string> _buttonSettingsPropertyMapping;
        private bool _isInitializing = true;

        public DisplayUserControl()
        {
            InitializeComponent();

            _buttonComboBoxMapping = new Dictionary<Button, ColorComboBox>()
            {
                { activeVlfCustomButton, activeVlfColorComboBox },
                { currentVlfCustomButton, currentVlfColorComboBox },
                { inactiveVlfCustomButton, inactiveVlfColorComboBox },
                { fontColorCustomButton, fontColorComboBox }
            };

            _buttonSettingsPropertyMapping = new Dictionary<Button, string>()
            {
                { activeVlfCustomButton, "ActiveVlfColor" },
                { currentVlfCustomButton, "CurrentVlfColor" },
                { inactiveVlfCustomButton, "InactiveVlfColor" },
                { fontColorCustomButton, "VlfFontColor" }
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
            _isInitializing = true;
            LoadFontsAndColors();

            foreach (Button button in _buttonComboBoxMapping.Keys)
            {
                InitializeColors(button);
            }

            InitializeColorComboBox(fontColorComboBox, VisualizerSettings.Clone.VlfFontColor.Value);
            fontSizeNumeric.Value = (decimal)VisualizerSettings.Clone.VlfFontSize.Value;
            InitializeFonts();
            _isInitializing = false;
        }

        private void InitializeColors(Button button)
        {
            ColorComboBox box = _buttonComboBoxMapping[button];
            string propertyName = _buttonSettingsPropertyMapping[button];

            Type settingsType = typeof(VisualizerSettings);
            PropertyInfo cloneProperty = settingsType.GetProperty(propertyName);
            Color currentValue = (Color)cloneProperty.GetMethod.Invoke(VisualizerSettings.Clone, null);

            InitializeColorComboBox(box, currentValue);
        }

        private void InitializeColorComboBox(ColorComboBox box, Color currentValue)
        {
            box.Items.Clear();
            box.Items.Add(currentValue);

            foreach (Color color in _knownColors)
            {
                box.Items.Add(color);
            }

            box.SelectedIndex = 0;
        }

        private void InitializeFonts()
        {
            fontNameComboBox.Items.Clear();

            string currentFont = VisualizerSettings.Clone.VlfFontName;
            if (_knownFonts.Contains(currentFont) == false)
            {
                fontNameComboBox.Items.Add(currentFont);
            }

            fontNameComboBox.Items.AddRange(_knownFonts.ToArray());
            fontNameComboBox.SelectedItem = currentFont;
        }

        private void LoadFontsAndColors()
        {
            lock (_initializerLocker)
            {
                if (_knownColors == null)
                {
                    _knownColors = new List<Color>();

                    Type colorType = typeof(Color);
                    List<PropertyInfo> colorProperties = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public).ToList();
                    colorProperties.Sort((a, b) => a.Name.CompareTo(b.Name));
                    foreach (PropertyInfo property in colorProperties)
                    {
                        Color color = (Color)property.GetMethod.Invoke(null, null);
                        _knownColors.Add(color);
                    }
                }

                if (_knownFonts == null)
                {
                    _knownFonts = FontFamily.Families.Select(f => f.Name).ToList();
                    _knownFonts.Sort();
                }
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing)
            {
                return;
            }

            ColorComboBox box = sender as ColorComboBox;
            KeyValuePair<Button, ColorComboBox> mapping = _buttonComboBoxMapping.FirstOrDefault(b => b.Value == box);
            if (mapping.Value == box)
            {
                Button button = mapping.Key;
                string propertyName = _buttonSettingsPropertyMapping[button];
                Type settingsType = typeof(VisualizerSettings);
                PropertyInfo cloneProperty = settingsType.GetProperty(propertyName);

                Color newColor = (Color)box.SelectedItem;
                cloneProperty.SetMethod.Invoke(VisualizerSettings.Clone, new object[1] { newColor });
            }
        }

        private void FontSizeNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (_isInitializing == false)
            {
                VisualizerSettings.Clone.VlfFontSize = (float)fontSizeNumeric.Value;
            }
        }

        private void FontNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing == false)
            {
                VisualizerSettings.Clone.VlfFontName = fontNameComboBox.Text;
            }
        }
    }
}
