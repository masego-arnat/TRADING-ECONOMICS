using OA.Data.Interface;

namespace OA.Repo.Repo
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public EFUnitOfWork(ApplicationContext context)
        {
            _context = context;
            Client = new ClientRepository(_context);
            Contact = new ContactRepository(_context);
            ClientContact = new ClientContactRepository(_context);
            // ContactConfigurations = new ContactRepository(_context);

        }
        public IClientRepository Client { get; }
        public IClientContactRepository ClientContact { get; }
        public IContactRepository Contact { get; }

        public async Task<int> CommitAsync()
        {
            //return await _context.SaveChangesAsync();
            try
            {
                return await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // You can also rethrow the exception if needed
                throw;
            }
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
        private bool _disposed;


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
