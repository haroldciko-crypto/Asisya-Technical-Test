using Asisya.Domain.Entities;

namespace Asisya.Application.Interfaces.Repositories;

public interface ISupplierRepository
{
    Task<List<Supplier>> GetAllAsync();
}