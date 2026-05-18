using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LAP2
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Tự động quản lý ID của MongoDB
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} - Lớp: {Grade} - Tuổi: {Age} - ĐC: {Address}";
        }
    }
}