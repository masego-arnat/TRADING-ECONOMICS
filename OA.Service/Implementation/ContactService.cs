using OA.Data.Entities;
using OA.Data.Interface;
using OA.Service.Interface;

namespace OA.Service.Implementation
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Contact> ListContact()
        {
            try
            {

                return _unitOfWork.Contact.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<Contact> GetContactById(int id)
        {
            try
            {
                var employee = await _unitOfWork.Contact.GetByIdAsync(id);
                return employee;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public async Task AddContact(Contact newUser)
        {

            try
            {
                _unitOfWork.Contact.Insert(newUser);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public async Task EditContact(Contact id)
        {
            try
            {
                _unitOfWork.Contact.Update(id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task DeleteContact(Contact  contact)
        {
            try
            {

                _unitOfWork.Contact.Delete(contact);

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
