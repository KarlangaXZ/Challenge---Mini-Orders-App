using System;
using System.Collections.Generic;
using OrdersApi.Models;


namespace OrdersApi.Repositories
{
public interface IOrderRepository
{
IEnumerable<Order> GetAll();
Order? Get(Guid id);
void Add(Order order);
bool Update(Order order);
bool Delete(Guid id);
}
}