namespace ServiceHub.Services.Interfaces
{
    public interface ISourceWatcher
    {
        bool IsSourceExisted { get; }

        string Source { get; }

        void SetSource(string source);

        /// <exception cref="InvalidOperationException">throws if preset source does not exist</exception>
        bool HasChanges();
        Task<string> GetData(CancellationToken cancellationToken = default(CancellationToken));
    }
}
