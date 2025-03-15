using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Service.Interfaces;

namespace WebApp.Service
{
    public class StudentService:IStudentService
    {
        private readonly AppDbContext _context;
      
        private readonly DbSet<Student> entities;
        public StudentService(AppDbContext context)
        {
            _context = context;
            entities = context.Set<Student>();
        }


        public async Task<List<Student>> GetAllStudents()
        {
            var result=await entities.ToListAsync();
            return result;
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            var student= await entities.Where(m=>m.Id==id).FirstOrDefaultAsync();
            return student;
        }
        public async Task CreateStudentAsync(Student student)
        {
            if (student is null) throw new ArgumentNullException();

            await entities.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            if (student is null) throw new ArgumentNullException();

            entities.Update(student);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {

            var entity= await entities.Where(m=>m.Id==id).FirstOrDefaultAsync();
            if (entity is null) throw new ArgumentNullException();

            //entity.SoftDelete = true;
            entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
