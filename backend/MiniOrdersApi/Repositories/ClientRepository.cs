using MiniOrdersApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniOrdersApi.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly List<Client> _clients = new();
        private readonly object _lock = new();

        public IEnumerable<Client> GetAll()
        {
            lock (_lock) { return _clients.ToList(); }
        }

        public Client? Get(Guid id)
        {
            lock (_lock) { return _clients.FirstOrDefault(c => c.Id == id); }
        }

        public void Add(Client client)
        {
            lock (_lock) { _clients.Add(client); }
        }

        public bool Update(Client client)
        {
            lock (_lock)
            {
                var index = _clients.FindIndex(c => c.Id == client.Id);
                if (index == -1) return false;
                _clients[index] = client;
                return true;
            }
        }

        public bool Delete(Guid id)
        {
            lock (_lock)
            {
                var existing = _clients.FirstOrDefault(c => c.Id == id);
                if (existing == null) return false;
                _clients.Remove(existing);
                return true;
            }
        }
    }
}
