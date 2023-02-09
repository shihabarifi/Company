using Company.Data;
using Company.IRepository;
using Company.Models;

namespace Company.Repositories
{
    public class ServiceRepo:IRepository<Service>
    {
        private readonly CompanyDbContext _context;

        public ServiceRepo(CompanyDbContext context)
        {
           _context = context;
        }

        public void Add(Service entity)
        {
            _context.Services.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var service = Find(id);
            _context.Services.Remove(service);
            _context.SaveChanges();
        }

        public Service Find(int id)
        {
            var service = _context.Services.SingleOrDefault(p => p.Id == id);
            return service;
        }

        public IList<Service> List()
        {
            var service = _context.Services.ToList();
            return service;
        }

        public void Update(Service entity)
        {
            _context.Services.Update(entity);
            _context.SaveChanges();
        }
    }
}
