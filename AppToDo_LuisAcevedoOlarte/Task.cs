using System;

namespace ToDoList.Models
{
    public class Task
    {

        public int Id { get; set; } //ID de la tarea
        public static int _nextId = 1; //Sumador.
        public string Description { get; set; } //Descripción de la tarea
        public DateTime? Deadline { get; set; } //Fecha a establecer. 
        public bool IsCompleted { get; set; } //Bool que permite marcar la tarea como completada

        public Task(string description, DateTime? deadline = null) //Ciclo que declara una entrada (Tarea) como incompleta por default.
        {
            Id = _nextId++; //Define un número por cada nueva entrada.
            Description = description;
            Deadline = deadline;
            IsCompleted = false; //Se establece en False por defecto. Cambiará a true según la petición del usuario.
        }
    }
}

