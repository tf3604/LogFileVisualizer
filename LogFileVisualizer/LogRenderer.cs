//  Copyright(c) 2016-2017 Brian Hansen.

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
        private static int _topMargin = 15;
        private static int _bottomMargin = _topMargin;
        private static int _leftMargin = 15;
        private static int _rightMargin = _leftMargin;

        private static int _verticalSpacing = 2;
        private static int _horizontalSpacing = 2;

        private static Brush _activeVlfBrush;
        private static Brush _currentVlfBrush;
        private static Brush _inactiveVlfBrush;
        private static Brush _vlfFontBrush;

        public static void Render(LogStatsDal dal, LiveViewOptions options, LayoutStyle layoutStyle)
        {
            Initialize();

            List<DbccLogInfoItem> vlfs;
            try
            {
                vlfs = dal.ReadDbccLogInfo(null, true, options.ForceDbccLoginfo);
            }
            catch (Exception ex)
            {
                try
                {
                    RenderException(options, ex);
                }
                catch
                {
                    // Intentionally swallow any exceptions.
                }
                return;
            }

            using (Bitmap bitmap = CreateImage(vlfs, options, layoutStyle))
            {
                if (options.DisplaySurface.InvokeRequired)
                {
                    options.DisplaySurface.Invoke(new Action<LiveViewOptions, Bitmap>(SetImage), options, bitmap);
                }
                else
                {
                    SetImage(options, bitmap);
                }
            }

            long totalLogSize = vlfs.Sum(v => v.FileSize);
            string displaySize = Utilities.FriendlySize(totalLogSize);

            DatabaseInfo dbInfo = dal.GetCurrentDatabaseInfo();

            string statusMessage = $"Instance: {dal.InstanceName}; Database: {dal.DatabaseName}; Recovery model: {dbInfo.RecoveryModelDescription}; Log size: {displaySize}; VLFs: {vlfs.Count}; Wait: {dbInfo.LogReuseWaitDescription}; Last refresh: {DateTime.Now:HH:mm:ss}";

            ToolStrip toolStrip = options.StatusLabel.GetCurrentParent();
            if (toolStrip != null)
            {
                if (toolStrip.InvokeRequired)
                {
                    toolStrip.Invoke(new Action<LiveViewOptions, string>(SetStatus), options, statusMessage);
                }
                else
                {
                    SetStatus(options, statusMessage);
                }
            }
        }

        public static void RenderException(LiveViewOptions options, Exception ex)
        {
            using (Bitmap bitmap = new Bitmap(options.DisplaySurface.Width, options.DisplaySurface.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(Brushes.White, 0, 0, options.DisplaySurface.Width, options.DisplaySurface.Height);
                    string fullMessage = ex.Message;
                    Font font = new Font("Times New Roman", 10);

                    List<MessageInfo> messages = SplitMessageToFitWindow(fullMessage, font, graphics, options.DisplaySurface.Width);
                    float totalHeight = messages.Sum(m => m.Height);
                    float top = (options.DisplaySurface.Height - totalHeight) / 2;

                    foreach (MessageInfo messageInfo in messages)
                    {
                        SizeF size = graphics.MeasureString(messageInfo.Message, font);
                        RectangleF rectangle = new RectangleF(
                            (options.DisplaySurface.Width - size.Width) / 2,
                            top,
                            size.Width,
                            size.Height);
                        graphics.DrawString(messageInfo.Message, font, Brushes.Black, rectangle);

                        top += messageInfo.Height;
                    }
                }

                if (options.DisplaySurface.InvokeRequired)
                {
                    options.DisplaySurface.Invoke(new Action<LiveViewOptions, Bitmap>(SetImage), options, bitmap);
                }
                else
                {
                    SetImage(options, bitmap);
                }
            }
        }

        private static List<MessageInfo> SplitMessageToFitWindow(string message, Font font, Graphics graphics, int maxWidth)
        {
            string space = @" ";
            SizeF spaceSize = graphics.MeasureString(space, font);

            List<WordInfo> words = GetWords(message, space);
            foreach (WordInfo word in words)
            {
                word.Size = graphics.MeasureString(word.Word, font);
            }

            float currentWidth = 0;
            float maxHeight = 0;
            bool isFirstOnLine = true;
            StringBuilder sb = new StringBuilder();
            List<MessageInfo> messages = new List<MessageInfo>();

            for (int idx = 0; idx < words.Count; idx++)
            {
                WordInfo word = words[idx];
                float wordWidth = (isFirstOnLine ? 0 : word.DelimitersPreceeding * spaceSize.Width) + word.Size.Width;
                if (currentWidth + wordWidth < maxWidth ||
                    isFirstOnLine)
                {
                    if (isFirstOnLine == false)
                    {
                        for (int i = 0; i < words[idx].DelimitersPreceeding; i++)
                        {
                            sb.Append(space);
                        }
                    }
                    isFirstOnLine = false;
                    currentWidth += wordWidth;
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        MessageInfo messageInfo = new MessageInfo();
                        messageInfo.Message = sb.ToString();
                        messageInfo.Height = maxHeight;
                        messages.Add(messageInfo);
                    }
                    currentWidth = 0;
                    maxHeight = 0;
                    sb.Clear();
                }

                sb.Append(word.Word);
                maxHeight = Math.Max(maxHeight, word.Size.Height);
            }

            if (sb.Length > 0)
            {
                MessageInfo messageInfo = new MessageInfo();
                messageInfo.Message = sb.ToString();
                messageInfo.Height = maxHeight;
                messages.Add(messageInfo);
            }

            return messages;
        }

        private static List<WordInfo> GetWords(string message, string delimiter)
        {
            List<WordInfo> words = new List<WordInfo>();
            int delimiterCount = 0;
            int startIndex = 0;
            StringBuilder sb = new StringBuilder();

            for (int idx = 0; idx <= message.Length - delimiter.Length; idx++)
            {
                char c = message[idx];
                if (message.Substring(idx, delimiter.Length) == delimiter)
                {
                    if (sb.Length > 0)
                    {
                        WordInfo word = new WordInfo();
                        word.Word = sb.ToString();
                        word.DelimitersPreceeding = delimiterCount;
                        word.StartIndex = startIndex;

                        words.Add(word);

                        delimiterCount = 0;
                        idx += delimiter.Length - 1;
                        sb.Clear();
                    }
                    delimiterCount++;
                }
                else
                {
                    if (sb.Length == 0)
                    {
                        startIndex = idx;
                    }
                    sb.Append(c);
                }
            }

            if (sb.Length > 0)
            {
                WordInfo word = new WordInfo();
                word.Word = sb.ToString();
                word.DelimitersPreceeding = delimiterCount;
                word.StartIndex = startIndex;

                words.Add(word);
            }

            return words;
        }

        private static void SetImage(LiveViewOptions options, Bitmap bitmap)
        {
            options.DisplaySurface.Image = bitmap.Clone() as Bitmap;
        }

        private static void SetStatus(LiveViewOptions options, string message)
        {
            options.StatusLabel.Text = message;
        }

        private static Bitmap CreateImage(List<DbccLogInfoItem> vlfs, LiveViewOptions options, LayoutStyle layoutStyle)
        {
            int width = Math.Max(options.DisplaySurface.Width, 1);
            int height = Math.Max(options.DisplaySurface.Height, 1);

            Bitmap bitmap = new Bitmap(width, height);

            if (layoutStyle == LayoutStyle.Logical)
            {
                // If logical style is requested, make a copy of the VLF list and sort by the VLF number.
                vlfs = new List<DbccLogInfoItem>(vlfs);
                vlfs.Sort(new VlfComparer());
            }

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                double scale = GenerateBlocks(vlfs, options, graphics, 1.0, true);
                GenerateBlocks(vlfs, options, graphics, scale, false);
            }

            return bitmap;
        }

        private static double GenerateBlocks(List<DbccLogInfoItem> vlfs, LiveViewOptions options, Graphics graphics, double verticalScale, bool simulateOnly)
        {
            // Current VLF is the active VLF (status = 2) with the largest VLF Number
            DbccLogInfoItem currentVlf = vlfs.Where(v => v.Status == 2).OrderByDescending(v => v.VirtualLogFileNumber).FirstOrDefault();

            // Get total VLF size;
            long totalVlfSize = vlfs.Sum(v => v.FileSize);

            int width = options.DisplaySurface.Width - _leftMargin - _rightMargin;
            int height = options.DisplaySurface.Height - _topMargin - _bottomMargin;

            int horizontalBlocks = (int)Math.Ceiling(Math.Sqrt(vlfs.Count));
            int verticalBlocks = horizontalBlocks;

            int averageBlockWidth = (width - (horizontalBlocks - 1) * _horizontalSpacing) / horizontalBlocks;
            int blockHeight = (int)((height - (verticalBlocks - 1) * _verticalSpacing) / verticalBlocks * verticalScale);

            int blockLeft = _leftMargin;
            int blockTop = _topMargin;
            int blockBottom = 0;

            Font font = null;
            if (simulateOnly == false)
            {
                font = new Font(VisualizerSettings.Instance.VlfFontName, VisualizerSettings.Instance.VlfFontSize.Value);
            }

            for (int vlfIndex = 0; vlfIndex < vlfs.Count; vlfIndex++)
            {
                DbccLogInfoItem vlf = vlfs[vlfIndex];
                int blockWidth = (int)(averageBlockWidth * (vlf.FileSize * 1.0 / totalVlfSize * vlfs.Count));

                int blockRight = blockLeft + blockWidth;
                blockBottom = blockTop + blockHeight;
                if (blockRight > options.DisplaySurface.Width - _rightMargin)
                {
                    blockLeft = _leftMargin;
                    blockTop += blockHeight + _verticalSpacing;

                    blockRight = blockLeft + blockWidth;
                    blockBottom = blockTop + blockHeight;
                }

                if (simulateOnly == false)
                {
                    Brush blockBrush = vlf.Status == 2 ? ((vlf == currentVlf) ? _currentVlfBrush : _activeVlfBrush) : _inactiveVlfBrush;
                    graphics.FillRectangle(blockBrush, blockLeft, blockTop, blockWidth, blockHeight);

                    if (options.ShowVlfNumbers &&
                        vlf.VirtualLogFileNumber > 0)
                    {
                        string vlfNumber = vlf.VirtualLogFileNumber.ToString();
                        SizeF textSize = graphics.MeasureString(vlfNumber, font);
                        if (textSize.Width < blockWidth &&
                            textSize.Height < blockHeight)
                        {
                            RectangleF textRectangle = new RectangleF(
                                blockLeft + blockWidth * 0.5f - textSize.Width * 0.5f,
                                blockTop + blockHeight * 0.5f - textSize.Height * 0.5f,
                                textSize.Width,
                                textSize.Height);
                            graphics.DrawString(vlfNumber, font, _vlfFontBrush, textRectangle);
                        }
                    }
                }

                blockLeft += blockWidth + _horizontalSpacing;
            }

            double scale = height * 1.0 / (blockBottom - _topMargin);
            return scale;
        }

        private static void Initialize()
        {
            _activeVlfBrush = new SolidBrush(VisualizerSettings.Instance.ActiveVlfColor.Value);
            _currentVlfBrush = new SolidBrush(VisualizerSettings.Instance.CurrentVlfColor.Value);
            _inactiveVlfBrush = new SolidBrush(VisualizerSettings.Instance.InactiveVlfColor.Value);
            _vlfFontBrush = new SolidBrush(VisualizerSettings.Instance.VlfFontColor.Value);
        }

        private class VlfComparer : IComparer<DbccLogInfoItem>
        {
            public int Compare(DbccLogInfoItem x, DbccLogInfoItem y)
            {
                if (x.VirtualLogFileNumber == 0 && y.VirtualLogFileNumber != 0)
                {
                    return 1;
                }

                if (x.VirtualLogFileNumber != 0 && y.VirtualLogFileNumber == 0)
                {
                    return -1;
                }

                return x.VirtualLogFileNumber.CompareTo(y.VirtualLogFileNumber);
            }
        }

        private class WordInfo
        {
            public string Word { get; set; }

            public int StartIndex { get; set; }

            public SizeF Size { get; set; }

            public int DelimitersPreceeding { get; set; }
        }

        private class MessageInfo
        {
            public string Message { get; set; }

            public float Height { get; set; }
        }
    }
}
