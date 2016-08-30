using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogFileVisualizerLib;

namespace LsnConverter
{
    public partial class LsnConverterForm : Form
    {
        private LogSequenceNumber _lsnValue;
        private Dictionary<TextBox, LogSequenceNumber.LsnStringType> _inputTextBoxConverter;
        private Dictionary<TextBox, Label> _labelMapping;
        private Font _baseTextBoxFont;
        private Size _baseTextBoxSize;
        private Size _baseFormSize;
        private Font _baseLabelFont;
        private int _bottomMargin;
        private int _rightMargin;

        private int _spaceBetweenTextBoxes = 14;

        public LsnConverterForm()
        {
            InitializeComponent();

            _inputTextBoxConverter = new Dictionary<TextBox, LogSequenceNumber.LsnStringType>()
            {
                { decimalTextBox, LogSequenceNumber.LsnStringType.Decimal },
                { hexadecimalSeparatedTextBox, LogSequenceNumber.LsnStringType.HexidecimalSeparated },
                { hexadecimalTextBox, LogSequenceNumber.LsnStringType.Hexadecimal },
                { decimalSeparatedTextBox, LogSequenceNumber.LsnStringType.DecimalSeparated }
            };

            _labelMapping = new Dictionary<TextBox, Label>()
            {
                { decimalTextBox, decimalLabel },
                { hexadecimalSeparatedTextBox, hexadecimalSeparatedLabel },
                { hexadecimalTextBox, hexadecimalLabel },
                { decimalSeparatedTextBox, decimalSeparatedLabel }
            };

            _baseTextBoxFont = decimalTextBox.Font;
            _baseTextBoxSize = decimalTextBox.Size;
            _baseFormSize = this.Size;
            _baseLabelFont = decimalLabel.Font;
            _bottomMargin = this.Height - _inputTextBoxConverter.Keys.Max(t => t.Bottom);
            _rightMargin = this.Width - _inputTextBoxConverter.Keys.Max(t => t.Right);
        }

        private void LsnConverterForm_Load(object sender, EventArgs e)
        {
            zoomComboBox.Items.Clear();

            zoomComboBox.Items.Add("50%");
            zoomComboBox.Items.Add("75%");
            zoomComboBox.Items.Add("100%");
            zoomComboBox.Items.Add("150%");
            zoomComboBox.Items.Add("200%");
            zoomComboBox.Items.Add("400%");

            zoomComboBox.Text = "100%";
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            Update(sender);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Update(sender);
            }
        }

        private void Update(object sender)
        {
            TextBox textBox = sender as TextBox;
            LoadValueAndConvert(textBox);
            //textBox.Focus();
            textBox.SelectAll();
        }

        private void LoadValueAndConvert(TextBox master)
        {
            if (string.IsNullOrEmpty(master.Text))
            {
                return;
            }

            try
            {
                _lsnValue = new LogSequenceNumber(master.Text, _inputTextBoxConverter[master]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error Parsing LSN Value", MessageBoxButtons.OK);
                return;
            }

            foreach (TextBox textBox in _inputTextBoxConverter.Keys)
            {
                if (master == textBox)
                {
                    textBox.BackColor = Color.LightYellow;
                }
                else
                {
                    textBox.BackColor = Color.White;
                    textBox.Text = _lsnValue.ToString(_inputTextBoxConverter[textBox]);
                }
            }
        }

        private void ZoomComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string zoomString = zoomComboBox.Text;
            if (string.IsNullOrEmpty(zoomString))
            {
                return;
            }

            if (zoomString.EndsWith("%"))
            {
                zoomString = zoomString.Substring(0, zoomString.Length - 1);
            }

            float zoom;
            float.TryParse(zoomString, out zoom);

            zoom /= 100;

            Font textBoxFont = new Font(_baseTextBoxFont.FontFamily, _baseTextBoxFont.SizeInPoints * zoom);
            Font labelFont = new Font(_baseLabelFont.FontFamily, _baseLabelFont.SizeInPoints * zoom);

            foreach (TextBox textBox in _inputTextBoxConverter.Keys)
            {
                textBox.Font = textBoxFont;
                _labelMapping[textBox].Font = labelFont;
            }

            // Fixup spacing
            List<TextBox> boxes = _inputTextBoxConverter.Keys.ToList();
            boxes.Sort((x, y) => x.Top.CompareTo(y.Top));
            int maxBottom = 0;

            for (int index = 1; index < boxes.Count; index++)
            {
                boxes[index].Top = boxes[index - 1].Top + boxes[index - 1].Height + _spaceBetweenTextBoxes;
                _labelMapping[boxes[index]].Top = boxes[index].Top + 3;
                int bottom = boxes[index].Bottom;
                maxBottom = Math.Max(bottom, maxBottom);
            }

            int maxLabelWidth = _labelMapping.Values.Max(l => l.Width);
            int textBoxWidth = (int)(_baseTextBoxSize.Width * zoom);
            foreach (TextBox box in _labelMapping.Keys)
            {
                box.Left = maxLabelWidth + 40;
                box.Width = textBoxWidth;
            }

            int maxTextBoxWidth = _labelMapping.Keys.Max(t => t.Width);

            this.Height = maxBottom + _bottomMargin;
            this.Width = Math.Max(maxLabelWidth + 40 + maxTextBoxWidth + _rightMargin, _baseFormSize.Width);
        }
    }
}
