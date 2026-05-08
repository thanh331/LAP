using System.IO;

namespace StudentApp
{
    public class StudentRepository
    {
        private List<Student> _students = new();
        private int _nextId = 1;
        private readonly string _path = "students.txt";

        public StudentRepository() { Load(); }

        public List<Student> GetAll() => _students;

        public void Add(Student s)
        {
            s.Id = _nextId++;
            _students.Add(s);
            Save();
        }

        public bool Delete(int id)
        {
            var s = _students.FirstOrDefault(x => x.Id == id);
            if (s == null) return false;
            _students.Remove(s);
            Save();
            return true;
        }

        public void Update(Student updated)
        {
            var s = _students.FirstOrDefault(x => x.Id == updated.Id);
            if (s != null)
            {
                s.Name = updated.Name; s.Email = updated.Email;
                s.Address = updated.Address; s.Age = updated.Age;
                s.Grade = updated.Grade;
                Save();
            }
        }

        // Chức năng Tìm kiếm theo yêu cầu
        public List<Student> Search(string keyword)
        {
            return _students.Where(s =>
                s.Id.ToString() == keyword ||
                s.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                s.Address.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                s.Grade.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        private void Save() => File.WriteAllLines(_path, _students.Select(s => s.ToFileString()));
        private void Load()
        {
            if (!File.Exists(_path)) return;
            _students = File.ReadAllLines(_path).Select(Student.FromFileString).ToList();
            if (_students.Any()) _nextId = _students.Max(s => s.Id) + 1;
        }
    }
}