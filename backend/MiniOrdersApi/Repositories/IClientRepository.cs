using MiniOrdersApi.Models;
using System;
using System.Collections.Generic;

namespace MiniOrdersApi.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();
        Client? Get(Guid id);
        void Add(Client client);
        bool Update(Client client);
        bool Delete(Guid id);
    }
}
