using OA.Data.Entities;
using OA.Data.Interface;
using OA.Service.Interface;

namespace OA.Service.Implementation
{
    public class ClientContactService : IClientContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task AddClientContactAsync(int clientId, int contactId)
        {
           
            var relationshipExists = _unitOfWork.ClientContact.GetAll().Any(cc => cc.ClientId == clientId && cc.ContactId == contactId);
            if (!relationshipExists)
            {
                var clientContact = new ClientContact
                {
                    ClientId = clientId,
                    ContactId = contactId
                };

          


                var client = _unitOfWork.Client.GetById(clientId);
                var contact = _unitOfWork.Contact.GetById(contactId);

                //clientContact.Contact = contact;   
                //clientContact.Client = client;

                _unitOfWork.ClientContact.Insert(clientContact);
                await _unitOfWork.CommitAsync();
                // Increment the count for the client and contact
                client.NumbersLinked++;
                contact.NumbersLinked++;

                _unitOfWork.Client.Update(client);
                await _unitOfWork.CommitAsync();
                _unitOfWork.Contact.Update(contact);
                await _unitOfWork.CommitAsync();

            }
        }
        public async Task RemoveClientContact(int clientId, int contactId)
        {
            var clientContact = _unitOfWork.ClientContact.GetAll().Where(cc => cc.ClientId == clientId && cc.ContactId == contactId).FirstOrDefault();

            if (clientContact != null)
            {

                _unitOfWork.ClientContact.Delete(clientContact);

                // Decrement the count for the client and contact
                var client = _unitOfWork.Client.GetById(clientId);
                var contact = _unitOfWork.Contact.GetById(contactId);

                // Increment the count for the client and contact
                client.NumbersLinked--;
                contact.NumbersLinked--;

                _unitOfWork.Client.Update(client);
                _unitOfWork.Contact.Update(contact);
               await _unitOfWork.CommitAsync();
            }
        }

        public IEnumerable<ClientContact> ListClientContact()
        {
            try
            {

                return _unitOfWork.ClientContact.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        //public async Task EditClientContactAsync(ClientContact  clientContact)
        //{
        //    try
        //    {
        //        _unitOfWork.ClientContact.Update(clientContact);
        //        await _unitOfWork.CommitAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //}

    }
}
