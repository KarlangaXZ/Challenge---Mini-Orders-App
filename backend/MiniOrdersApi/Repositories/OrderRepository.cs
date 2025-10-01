using System;
using System.Collections.Generic;
using System.Linq;
using OrdersApi.Models;


namespace OrdersApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new();
        private readonly object _lock = new();


        public OrderRepository()
        {
            
            _orders.Add(new Order { Id = Guid.NewGuid(), Cliente = "ACME S.A.", Fecha = DateTime.UtcNow.AddDays(-2), Total = 150.50m });
            _orders.Add(new Order { Id = Guid.NewGuid(), Cliente = "Globex Corp", Fecha = DateTime.UtcNow.AddDays(-1), Total = 299.99m });
        }


        public IEnumerable<Order> GetAll()
        {
            lock (_lock)
            {
                return _orders.ToList(); 
            }
        }


        public Order? Get(Guid id)
        {
            lock (_lock)
            {
                return _orders.FirstOrDefault(o => o.Id == id);
            }
        }


        public void Add(Order order)
        {
            lock (_lock)
            {
                _orders.Add(order);
            }
        }


        public bool Update(Order order)
        {
            lock (_lock)
            {
                var idx = _orders.FindIndex(o => o.Id == order.Id);
                if (idx == -1) return false;
                _orders[idx] = order;
                return true;
            }
        }


        public bool Delete(Guid id)
        {
            lock (_lock)
            {
                var existing = _orders.FirstOrDefault(o => o.Id == id);
                if (existing == null) return false;
                _orders.Remove(existing);
                return true;
            }
        }
    }
}