using ServiceHub.Services.Interfaces;

namespace ServiceHub.Services.SourceWatchers
{
    public class FileWatcher : ISourceWatcher
    {
        private DateTime _lastSourceChangedTime = DateTime.MinValue;
        private long _lastSourceSize = long.MinValue;
        public bool IsSourceExisted => File.Exists(Source);
        public string Source { get; private set; }

        public FileWatcher()
        {
            Source = string.Empty;
        }

        public async Task<string> GetData(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            CheckIsSourceExists();

            string data = await File.ReadAllTextAsync(Source, cancellationToken).ConfigureAwait(false);

            _lastSourceChangedTime = File.GetLastWriteTime(Source);
            var sourceInfo = new FileInfo(Source);
            _lastSourceSize = sourceInfo.Length;

            return data;
        }

        public void SetSource(string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentException("File name can not be empty", nameof(source));

            Source = source;

            if (IsSourceExisted)
            {
                _lastSourceChangedTime = File.GetLastWriteTime(Source);

                var sourceInfo = new FileInfo(Source);
                _lastSourceSize = sourceInfo.Length;
            }
            else
            {
                _lastSourceChangedTime = DateTime.MinValue;
                _lastSourceSize = long.MinValue;
            }
        }

        public bool HasChanges()
        {
            if (!IsSourceExisted)
                return false;

            DateTime changedTime = File.GetLastWriteTime(Source);
            if (changedTime > _lastSourceChangedTime)
                return true;

            var sourceInfo = new FileInfo(Source);
            if (sourceInfo.Length > _lastSourceSize)
                return true;

            return false;
        }

        private void CheckIsSourceExists()
        {
            if (!IsSourceExisted)
                throw new InvalidOperationException($"Inspected file does not exist. File name is {Source}");
        }
    }
}
