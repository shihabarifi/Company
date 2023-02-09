using Company.Models;

namespace Company.ViewModel
{
    public class ServiceVM
    {
        public List<Service> Services { get; set; }
        public NewService NewService { get; set; }
    }
    public class NewService
    {
        public int ServiceId { get; set; }
        public string Title { get; set; }
        public string Descrption { get; set; }

        public string ImageUrl { get; set; }
    }
}
