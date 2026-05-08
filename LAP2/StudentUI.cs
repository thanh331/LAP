using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAP2
{
    public class StudentUI
    {
        private readonly StudentService _service;
        public StudentUI(StudentService service) => _service = service;

        public async Task Run()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("=== QUẢN LÝ SINH VIÊN (MONGODB) ===");

                    var list = await _service.GetList();

                    if (list.Count == 0) Console.WriteLine("Danh sách trống.");
                    else list.ForEach(Console.WriteLine);

                    Console.WriteLine("\n1. Thêm | 2. Sửa | 3. Xoá | 4. Tìm kiếm | 0. Thoát");
                    Console.Write("Chọn: ");
                    var choice = Console.ReadLine();

                    if (choice == "0") break;
                    await HandleChoice(choice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[LỖI]: {ex.Message}");
                }
                Console.WriteLine("\nNhấn Enter để tiếp tục...");
                Console.ReadLine();
            }
        }

        private async Task HandleChoice(string choice)
        {
            switch (choice)
            {
                case "1": // Thêm sinh viên [cite: 766, 770]
                var s = new Student();
                Console.Write("Tên: "); s.Name = Console.ReadLine() ?? "";
                Console.Write("Email: "); s.Email = Console.ReadLine() ?? "";
                Console.Write("Địa chỉ: "); s.Address = Console.ReadLine() ?? "";
                Console.Write("Tuổi: "); int.TryParse(Console.ReadLine(), out int age); s.Age = age;
                Console.Write("Lớp: "); s.Grade = Console.ReadLine() ?? "";
                await _service.Create(s);
                Console.WriteLine("Thêm thành công!");
                break;

                case "2": // Sửa sinh viên [cite: 770]
                    Console.Write("Nhập ID cần sửa: ");
                    string editId = Console.ReadLine() ?? "";
                    var studentEdit = new Student { Id = editId };
                    Console.Write("Tên mới: "); studentEdit.Name = Console.ReadLine() ?? "";
                    Console.Write("Lớp mới: "); studentEdit.Grade = Console.ReadLine() ?? "";
                    await _service.Edit(editId, studentEdit);
                    Console.WriteLine("Cập nhật thành công!");
                    break;

                    case "3": // Xoá sinh viên [cite: 770]
                        Console.Write("Nhập ID cần xoá: ");
                        string delId = Console.ReadLine() ?? "";
                        await _service.Remove(delId);
                        Console.WriteLine("Đã xoá!");
                        break;

                        case "4": // Tìm kiếm theo yêu cầu [cite: 771]
                            Console.Write("Nhập từ khoá: ");
                            var key = Console.ReadLine() ?? "";
                            var results = await _service.Find(key);
                            results.ForEach(Console.WriteLine);
                            break;
                        }
                    }
                }
            }