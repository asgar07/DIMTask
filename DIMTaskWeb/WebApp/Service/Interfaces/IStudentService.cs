using WebApp.Models;

namespace WebApp.Service.Interfaces
{
    public interface IStudentService
    {
        Task DeleteStudentAsync(int id);
        Task UpdateStudentAsync(Student student);
        Task<Student> GetStudentAsync(int id);
        Task<List<Student>> GetAllStudents();
        Task CreateStudentAsync(Student student);
    }
}
