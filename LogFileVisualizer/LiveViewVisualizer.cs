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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogFileVisualizerLib;

namespace LogFileVisualizer
{
    internal class LiveViewVisualizer : IDisposable
    {
        private LiveViewOptions _options;
        private LayoutStyle _layoutStyle;
        private bool _isCancelled;
        private LogStatsDal _dal;

        public LiveViewVisualizer(LiveViewOptions options, LayoutStyle layoutStyle)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options;
            _layoutStyle = layoutStyle;
            _isCancelled = false;
            _dal = new LogStatsDal(options.Connection);
        }

        public void Start()
        {
            DateTime startTime;
            DateTime nextRefreshTime;

            do
            {
                LogRenderer.Render(_dal, _options, _layoutStyle);
                startTime = DateTime.Now;
                nextRefreshTime = startTime.AddSeconds(_options.RefreshIntervalSeconds);
                do
                {
                    Thread.Sleep(100);
                }
                while (_isCancelled == false &&
                    DateTime.Now < nextRefreshTime);
            }
            while (_isCancelled == false);
        }

        public LayoutStyle LayoutStyle
        {
            get
            {
                return _layoutStyle;
            }
            set
            {
                _layoutStyle = value;
            }
        }

        public void Cancel()
        {
            _isCancelled = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dal != null)
                {
                    _dal.Dispose();
                    _dal = null;
                }
            }
        }
    }
}
