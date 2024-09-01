using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args) //Menu de tareas
        {
            var taskService = new TaskService();

            while (true)
            {
                Console.WriteLine("================█ █==================");
                Console.WriteLine("      Registrador de Tareas          ");
                Console.WriteLine("================█ █==================");
                Console.WriteLine("|                                   |");
                Console.WriteLine("|  1. Añadir Tarea                  |");
                Console.WriteLine("|  2. Listar Tareas                 |");
                Console.WriteLine("|  3. Marcar Tarea Como Completada  |");
                Console.WriteLine("|  4. Borrar Tarea                  |");
                Console.WriteLine("|  5. Salir                         |");
                Console.WriteLine("|                                   |");
                Console.WriteLine("================█ █==================");

                Console.Write("Elige una opción: ");
                string input = Console.ReadLine();

                try //Ciclo para encapsular valores erroneos. Evita que el programa se rompa. 
                {
                    int option = Convert.ToInt32(input);

                    switch (option)
                    {
                        case 1:
                            AddTask(taskService);
                            break;
                        case 2:
                            ListTasks(taskService);
                            break;
                        case 3:
                            MarkTaskAsCompleted(taskService);
                            break;
                        case 4:
                            DeleteTask(taskService);
                            break;
                        case 5:
                            return;
                        default: //En caso de ingresarse un número inferior a 0 o superior a 5, avisa al usuario y vuelve a la pantalla principal. 
                            Console.WriteLine("Opción Inválida, elige nuevamente.");
                            Console.WriteLine("Presiona cualquier tecla para continuar...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                catch (FormatException) //Comprueba si se ha ingresado un caracter no numérico. 
                {
                    Console.WriteLine("Ha ingresado un caracter inválido, solo se permiten números, intente nuevamente.");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void AddTask(TaskService taskService) //Ciclo para registrar tareas
        {
            Console.Write("¿Cuál tarea deseas registrar?: ");
            string description;
            while (true)
            {
                description = Console.ReadLine();
                if (description.All(char.IsLetter) || description.Contains(" "))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; //Color identificador de ERROR. 
                    Console.Write("Error: solo se permiten letras y espacios. ");
                    Console.WriteLine("Presiona cualquier tecla para volver a intentarlo...");
                    Console.ReadKey();
                    Console.Clear();
                    Console.ResetColor();
                    return;
                }
            }

            Console.Write("Fecha límite (Formato: día/mes/año): ");
            string deadlineString = Console.ReadLine();
            DateTime? deadline = null;
            if (!string.IsNullOrEmpty(deadlineString))
            {
                deadline = DateTime.Parse(deadlineString);
            }

            var task = new Models.Task(description, deadline);
            taskService.AddTask(task);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tarea registrada!");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear(); //Vuelve a la pantalla de inicio.
        }

        static void ListTasks(TaskService taskService) //Ciclo para mostrar las tareas registradas
        {
            var tasks = taskService.GetTasks();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine("****────────────────****");
            Console.WriteLine("        [Tareas]        ");
            Console.WriteLine("****────────────────****");
            Console.ResetColor();
            foreach (var task in tasks)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"| Id: {task.Id} | , Descripción: {task.Description} , Fecha límite: {task.Deadline?.ToString("dd/MM/yyyy")} , Completada: {task.IsCompleted} ");
                Console.WriteLine();
                Console.ResetColor();
            }
        }

        static void MarkTaskAsCompleted(TaskService taskService) //Ciclo para marcar tareas como completadas
        {
            Console.Write("Ingresa la ID de la tarea que deseas marcar como completada: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            taskService.MarkTaskAsCompleted(taskId);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tarea marcada como completada.");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear(); //Vuelve a la pantalla de inicio.
        }

        static void DeleteTask(TaskService taskService) //Ciclo para borrar tareas
        {
            Console.Write("Ingresa la ID de la tarea a borrar: ");
            int taskId = Convert.ToInt32(Console.ReadLine());

            taskService.DeleteTask(taskId);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tarea borrada!");
            Console.WriteLine("Presiona cualquier tecla para volver al menú principal.");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear(); //Vuelve a la pantalla de inicio.
        }
    }
}

