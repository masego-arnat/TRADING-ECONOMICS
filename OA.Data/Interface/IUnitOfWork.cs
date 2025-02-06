namespace OA.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Client { get; }
        IContactRepository Contact { get; }
        IClientContactRepository ClientContact { get; }

        Task<int> CommitAsync();

        int Commit();
    }
}
