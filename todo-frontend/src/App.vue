<script setup>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';

  const API_URL = "http://localhost:5099/api/Todo";
  const todos = ref([]);
  const newTask = ref('');

  // Các biến phục vụ việc sửa
  const editingId = ref(null);
  const editingTitle = ref('');

  const fetchTodos = async () => {
    const res = await axios.get(API_URL);
    todos.value = res.data;
  };

  const addTask = async () => {
    if (!newTask.value) return;
    await axios.post(API_URL, { title: newTask.value, isCompleted: false });
    newTask.value = '';
    fetchTodos();
  };

  // Hàm bật chế độ sửa
  const startEdit = (todo) => {
    editingId.ref = todo.id;
    editingTitle.value = todo.title;
    editingId.value = todo.id; // Ghi nhớ đang sửa dòng nào
  };

  // Hàm hủy sửa
  const cancelEdit = () => {
    editingId.value = null;
    editingTitle.value = '';
  };

  // Hàm lưu dữ liệu sau khi sửa (Gọi API PUT)
  const saveEdit = async (todo) => {
    if (!editingTitle.value) return;
    await axios.put(`${API_URL}/${todo.id}`, {
      id: todo.id,
      title: editingTitle.value,
      isCompleted: todo.isCompleted
    });
    editingId.value = null; // Tắt chế độ sửa
    fetchTodos(); // Tải lại danh sách
  };

  const toggleTask = async (todo) => {
    await axios.put(`${API_URL}/${todo.id}`, { ...todo, isCompleted: !todo.isCompleted });
    fetchTodos();
  };

  const deleteTask = async (id) => {
    if (confirm("Bạn có chắc muốn xóa?")) {
      await axios.delete(`${API_URL}/${id}`);
      fetchTodos();
    }
  };

  onMounted(fetchTodos);
</script>

<template>
  <div style="max-width: 600px; margin: 50px auto; font-family: Arial; padding: 20px; border: 1px solid #ddd; border-radius: 8px;">
    <h1 style="text-align: center; color: #42b883;">DNU Todo App</h1>

    <div style="display: flex; gap: 10px; margin-bottom: 20px;">
      <input v-model="newTask" @keyup.enter="addTask" placeholder="Thêm việc mới..." style="flex: 1; padding: 8px;" />
      <button @click="addTask" style="padding: 8px 20px; background: #42b883; color: white; border: none; cursor: pointer;">Thêm</button>
    </div>

    <ul style="list-style: none; padding: 0;">
      <li v-for="item in todos" :key="item.id" style="display: flex; align-items: center; justify-content: space-between; padding: 10px; border-bottom: 1px solid #eee;">

        <div v-if="editingId !== item.id" style="flex: 1;">
          <span :style="{ textDecoration: item.isCompleted ? 'line-through' : 'none', cursor: 'pointer' }" @click="toggleTask(item)">
            {{ item.title }}
          </span>
        </div>

        <div v-else style="flex: 1; display: flex; gap: 5px;">
          <input v-model="editingTitle" style="flex: 1; padding: 5px;" @keyup.enter="saveEdit(item)" />
          <button @click="saveEdit(item)" style="background: #28a745; color: white; border: none; padding: 5px 10px; cursor: pointer;">Lưu</button>
          <button @click="cancelEdit" style="background: #6c757d; color: white; border: none; padding: 5px 10px; cursor: pointer;">Hủy</button>
        </div>

        <div v-if="editingId !== item.id">
          <button @click="startEdit(item)" style="margin-left: 10px; color: blue; border: 1px solid blue; background: white; cursor: pointer; padding: 3px 8px;">Sửa</button>
          <button @click="deleteTask(item.id)" style="margin-left: 5px; color: red; border: 1px solid red; background: white; cursor: pointer; padding: 3px 8px;">Xóa</button>
        </div>
      </li>
    </ul>
  </div>
</template>
