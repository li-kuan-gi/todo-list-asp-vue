<script setup>
import { onMounted, ref, watch } from 'vue';

const todos = ref([]);
const newTodoName = ref("");
const isUpdating = ref(false);

onMounted(() => {
  fetch("/api/Todo").then(async res => {
    let todosJson = await res.json();
    todos.value = todosJson;
  })
});

watch(isUpdating, (newValue, oldValue) => {
  if (oldValue && !newValue) {
    fetch("/api/Todo").then(async res => {
      let todosJson = await res.json();
      todos.value = todosJson;
    })
  }
});

const addTodo = async () => {
  isUpdating.value = true;

  await fetch("/api/Todo/create", {
    method: "POST",
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify({
      name: newTodoName.value,
    }),
  });

  isUpdating.value = false;

  newTodoName.value = "";
}

const removeTodo = async id => {
  isUpdating.value = true;

  await fetch(`/api/Todo/delete/${id}`, { method: "POST" });

  isUpdating.value = false;
}

const toggleCompetion = async id => {
  const todo = todos.value.find(todo => todo.id === id);

  isUpdating.value = true;

  await fetch(`/api/Todo/update/${id}`, {
    method: "POST",
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify({
      isComplete: !todo.isComplete,
    })
  })

  isUpdating.value = false;
}

</script>

<template>
  <div class="container mt-5">
    <div class="card shadow-sm">
      <div class="card-body">
        <h1 class="text-center">Todo List</h1>

        <div class="input-group mb-3">
          <input type="text" class="form-control" placeholder="新增待辦事項" aria-label="Todo input" v-model="newTodoName">
          <button class="btn btn-primary" @click="addTodo">新增</button>
        </div>

        <ul class="list-group">
          <li class="list-group-item d-flex justify-content-between align-items-center" v-for="todo in todos"
            :id="todo.id">
            <div class="form-check">
              <input class="form-check-input" type="checkbox" :checked="todo.isComplete"
                @change="toggleCompetion(todo.id)">
              <label class="form-check-label">
                {{ todo.name }}
              </label>
            </div>
            <button class="btn btn-danger btn-sm" @click="removeTodo(todo.id)">刪除</button>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>