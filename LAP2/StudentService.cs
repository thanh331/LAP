namespace LAP2
{
    public class StudentService
    {
        private readonly StudentRepository _repo;

        public StudentService(StudentRepository repo) => _repo = repo;

        public Task<List<Student>> GetList() => _repo.GetAllAsync();
        public Task Create(Student s) => _repo.AddAsync(s);
        public Task Remove(string id) => _repo.DeleteAsync(id);
        public Task Edit(string id, Student s) => _repo.UpdateAsync(id, s);
        public Task<List<Student>> Find(string key) => _repo.SearchAsync(key);
    }
}