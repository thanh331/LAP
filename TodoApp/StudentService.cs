namespace StudentApp
{
    public class StudentService
    {
        private readonly StudentRepository _repo = new();

        public List<Student> GetList() => _repo.GetAll();
        public void Create(Student s) => _repo.Add(s);
        public bool Remove(int id) => _repo.Delete(id);
        public void Edit(Student s) => _repo.Update(s);
        public List<Student> Find(string key) => _repo.Search(key);
    }
}