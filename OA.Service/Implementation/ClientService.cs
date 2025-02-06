using OA.Data.Entities;
using OA.Data.Interface;
using OA.Service.Interface;

namespace OA.Service.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Client> ListClients()
        {
            try
            {

                return _unitOfWork.Client.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<Client> GetClientById(int id)
        {
            try
            {
                var employee = await _unitOfWork.Client.GetByIdAsync(id);
                return employee;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public async Task AddClient(Client newUser)
        {

            try
            {
                _unitOfWork.Client.Insert(newUser);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public async Task EditClient(Client  client)
        {
            try
            {
                client.DateModified = DateTime.Now;
                _unitOfWork.Client.Update(client);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        } 
        public async Task DeleteClient(Client  client)
        {
            try
            {
               
                _unitOfWork.Client.Delete(client);
             
                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

    }
}
