using ServiceHub.Services.SourceWatchers;

namespace ServiceHub.Services.Tests.SourceWatchers
{
    public class FileWatcherTests : IDisposable
    {
        private string _filePath;

        public FileWatcherTests()
        {
            _filePath = Path.GetRandomFileName();
            File.WriteAllText(_filePath, "First line");
        }

        public void Dispose()
        {
            File.Delete(_filePath);
        }

        [Fact]
        public async Task FileChangesDetectedTest()
        {
            FileWatcher testObject = new FileWatcher();

            testObject.SetSource( _filePath );
            bool fileChanged = testObject.HasChanges();

            Assert.False(fileChanged, "File initially changed");

            await File.AppendAllTextAsync(_filePath, "Test");
            fileChanged = testObject.HasChanges();

            Assert.True(fileChanged, "File check returns unchangend after change");
        }
    }
}