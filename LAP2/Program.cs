using LAP2;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

// Chuỗi kết nối MongoDB mặc định
string conn = "mongodb://localhost:27017";
var repo = new StudentRepository(conn, "StudentDB");
var service = new StudentService(repo);
var ui = new StudentUI(service);

await ui.Run();