using Company.Data;
using Company.IRepository;
using Company.Models;

namespace Company.Repositories
{
    public class AboutRepo : IRepository<About>
    {
        private readonly CompanyDbContext _context;

        public AboutRepo(CompanyDbContext context)
        {
           _context = context;
        }

        public void Add(About entity)
        {
            _context.About.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var about = Find(id);
            _context.About.Remove(about);
            _context.SaveChanges();
        }

        public About Find(int id)
        {
            var about = _context.About.SingleOrDefault(p => p.Id == id);
            return about;
        }

        public IList<About> List()
        {
           return _context.About.ToList();
        }

        public void Update(About entity)
        {
            _context.About.Update(entity);
            _context.SaveChanges();
        }
    }
}
