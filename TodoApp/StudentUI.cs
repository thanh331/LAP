namespace StudentApp
{
    public class StudentUI
    {
        private readonly StudentService _service = new();

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== QUẢN LÝ SINH VIÊN ===");
                var list = _service.GetList();
                if (!list.Any()) Console.WriteLine("Danh sách trống.");
                else list.ForEach(Console.WriteLine);

                Console.WriteLine("\n1. Thêm | 2. Sửa | 3. Xoá | 4. Tìm kiếm | 0. Thoát");
                Console.Write("Chọn: ");
                var mode = Console.ReadLine();

                switch (mode)
                {
                    case "1": InputAdd(); break;
                    case "2": InputEdit(); break;
                    case "3": InputDelete(); break;
                    case "4": InputSearch(); break;
                    case "0": return;
                }
                Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
            }
        }

        private void InputAdd()
        {
            var s = new Student();
            Console.Write("Tên: "); s.Name = Console.ReadLine();
            Console.Write("Email: "); s.Email = Console.ReadLine();
            Console.Write("Địa chỉ: "); s.Address = Console.ReadLine();
            Console.Write("Tuổi: "); s.Age = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Lớp: "); s.Grade = Console.ReadLine();
            _service.Create(s);
        }

        private void InputSearch()
        {
            Console.Write("Nhập từ khoá (Id/Tên/Địa chỉ/Lớp): ");
            var key = Console.ReadLine();
            var results = _service.Find(key);
            Console.WriteLine("--- Kết quả tìm kiếm ---");
            results.ForEach(Console.WriteLine);
        }

        private void InputDelete()
        {
            Console.Write("Nhập ID cần xoá: ");
            int.TryParse(Console.ReadLine(), out int id);
            _service.Remove(id);
        }

        private void InputEdit()
        {
            Console.Write("Nhập ID sinh viên cần sửa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var s = new Student { Id = id };
                Console.Write("Tên mới: "); s.Name = Console.ReadLine();
                Console.Write("Email mới: "); s.Email = Console.ReadLine();
                Console.Write("Địa chỉ mới: "); s.Address = Console.ReadLine();
                Console.Write("Tuổi mới: "); s.Age = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Lớp mới: "); s.Grade = Console.ReadLine();
                _service.Edit(s);
            }
        }
    }
}