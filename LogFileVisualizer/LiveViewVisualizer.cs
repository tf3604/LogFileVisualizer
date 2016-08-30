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
        private bool _isCancelled;
        private LogStatsDal _dal;

        public LiveViewVisualizer(LiveViewOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options;
            _isCancelled = false;
            _dal = new LogStatsDal(options.Connection);
        }

        public void Start()
        {
            DateTime startTime;
            DateTime nextRefreshTime;

            do
            {
                LogRenderer.Render(_dal, _options);
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
