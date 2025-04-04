using System.ComponentModel.DataAnnotations;
using MinhTienHairSalon.Models;

namespace MinhTienHairSalon.ViewModels
{
    public class HomeViewModel
    {
        // public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}