namespace TaskManagementLibrary
{
    public class TaskService
    {
        private readonly List<Task> _tasks = new List<Task>();
        private int _nextId = 1;

        public Task AddTask(string title, string description)
        {
            var task = new Task
            {
                Id = _nextId++,
                Title = title ?? "default",
                Description = description ?? "default",
                IsCompleted = false
            };
            _tasks.Add(task);
            return task;
        }

        public IEnumerable<Task> GetAllTasks() => _tasks;

        public Task GetTaskById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

        public bool UpdateTask(int id, string newTitle, string newDescription, bool isCompleted)
        {
            var task = GetTaskById(id);
            if (task == null) return false;

            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                task.Title = newTitle;
            }
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                task.Description = newDescription;
            }
            task.IsCompleted = isCompleted;
            return true;
        }

        public bool DeleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task == null) return false;
            return _tasks.Remove(task);
        }

        public bool CompleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task == null) return false;
            task.IsCompleted = true;
            return true;
        }
    }
}
