namespace StudentApp
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} | Tên: {Name} | Lớp: {Grade} | Tuổi: {Age} | Email: {Email} | ĐC: {Address}";
        }

        // Dùng để lưu vào file .txt, cách nhau bởi dấu gạch đứng |
        public string ToFileString()
        {
            return $"{Id}|{Name}|{Email}|{Address}|{Age}|{Grade}";
        }

        public static Student FromFileString(string line)
        {
            var p = line.Split('|');
            return new Student
            {
                Id = int.Parse(p[0]),
                Name = p[1],
                Email = p[2],
                Address = p[3],
                Age = int.Parse(p[4]),
                Grade = p[5]
            };
        }
    }
}