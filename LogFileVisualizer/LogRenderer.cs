﻿using System;
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

        public static void Render(LogStatsDal dal, LiveViewOptions options)
        {
            try
            {
                List<DbccLogInfoItem> vlfs = dal.ReadDbccLogInfo(null, true);

                using (Bitmap bitmap = CreateImage(vlfs, options))
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
            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }

        private static void SetImage(LiveViewOptions options, Bitmap bitmap)
        {
            options.DisplaySurface.Image = bitmap.Clone() as Bitmap;
        }

        private static Bitmap CreateImage(List<DbccLogInfoItem> vlfs, LiveViewOptions options)
        {
            Bitmap bitmap = new Bitmap(options.DisplaySurface.Width, options.DisplaySurface.Height);

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
                font = new Font("Times New Roman", 10);
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
                    Brush blockBrush = vlf.Status == 2 ? ((vlf == currentVlf) ? Brushes.Yellow : Brushes.Red) : Brushes.Green;
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
                            graphics.DrawString(vlfNumber, font, Brushes.Black, textRectangle);
                        }
                    }
                }

                blockLeft += blockWidth + _horizontalSpacing;
            }

            double scale = height * 1.0 / (blockBottom - _topMargin);
            return scale;
        }
    }
}