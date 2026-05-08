using MongoDB.Driver;
using LAP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAP2 // Đã đổi tên namespace cho khớp với Project LAP2 của bạn
{
    public class StudentRepository
    {
        private readonly IMongoCollection<Student> _collection;

        public StudentRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Student>("Students");
        }

        public async Task<List<Student>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task AddAsync(Student student) =>
            await _collection.InsertOneAsync(student);

        public async Task UpdateAsync(string id, Student student) =>
            await _collection.ReplaceOneAsync(s => s.Id == id, student);

        public async Task DeleteAsync(string id) =>
            await _collection.DeleteOneAsync(s => s.Id == id);

        // Chức năng Tìm kiếm theo yêu cầu
        public async Task<List<Student>> SearchAsync(string keyword)
        {
            // Kiểm tra nếu keyword rỗng thì trả về tất cả
            if (string.IsNullOrWhiteSpace(keyword)) return await GetAllAsync();

            return await _collection.Find(s =>
                s.Id.Contains(keyword) ||
                s.Name.Contains(keyword) ||
                s.Address.Contains(keyword) ||
                s.Grade.Contains(keyword)
            ).ToListAsync();
        }
    }
}