using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TaskService
    {
        private List<Task> _tasks; //Lista que almacena todas las tareas.

        public TaskService() //Constructor: Crea una lista vacía (Por default) de tareas. 
        {
            _tasks = new List<Task>();
        }

        public void AddTask(Task task) //Método: Agrega una tarea a la lista.
        {
            _tasks.Add(task);
        }

        public List<Task> GetTasks() //Devuelve la lista de tareas. 
        {
            return _tasks;
        }

        public void MarkTaskAsCompleted(int taskId) //Marca la tarea como completada, estableciendo su propiedad "IsCompleted" como (true).
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                task.IsCompleted = true;
            }
        }

        public void DeleteTask(int taskId) //Elimina una tarea de la lista. Se identifica mediante la ID.
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }
    }
}
