using TaskManagementLibrary;

class Program
{
    static bool Confirme(string accion)
    {
        Console.WriteLine("Confirme " + accion + " s/n");
        return Console.ReadLine() == "s";
    }

    static void Main(string[] args)
    {
        var taskService = new TaskService();

        while (true)
        {
            try
            {
                Console.WriteLine("1. Agregar tarea");
                Console.WriteLine("2. Ver tareas");
                Console.WriteLine("3. Actualizar tarea");
                Console.WriteLine("4. Eliminar tarea");
                Console.WriteLine("5. Completar tarea");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Titulo: ");
                        var title = Console.ReadLine()?.Trim();
                        Console.Write("Descripcion: ");
                        var description = Console.ReadLine()?.Trim();

                        title = string.IsNullOrWhiteSpace(title) ? "default" : title;
                        description = string.IsNullOrWhiteSpace(description) ? "default" : description;

                        var task = taskService.AddTask(title, description);
                        Console.WriteLine($"Agregar tarea con el Id: {task.Id}");
                        break;
                    case "2":
                        var tasks = taskService.GetAllTasks();
                        Console.WriteLine("-------------------------------------------------");
                        foreach (var t in tasks)
                        {
                            Console.WriteLine($"ID: {t.Id}, Titulo: {t.Title}, Descripcion: {t.Description}, Completada: {t.IsCompleted}");
                        }
                        Console.WriteLine("-------------------------------------------------");
                        break;
                    case "3":
                        Console.Write("Actualizar la tarea con el siguiente Id: ");
                        if (!int.TryParse(Console.ReadLine(), out var updateId))
                        {
                            Console.WriteLine("Id inválido. Por favor, ingrese un número válido.");
                            break;
                        }

                        task = taskService.GetTaskById(updateId);
                        if (task != null)
                        {
                            Console.WriteLine($"Título actual: {task.Title}");
                            Console.Write("-> Nuevo titulo: ");
                            var newTitle = Console.ReadLine();
                            Console.WriteLine($"Descripción actual: {task.Description}");
                            Console.Write("-> Nueva Descripcion: ");
                            var newDescription = Console.ReadLine();
                            Console.Write("Completada (true/false): ");
                            if (!bool.TryParse(Console.ReadLine(), out var isCompleted))
                            {
                                Console.WriteLine("Valor de completado inválido. Por favor, ingrese 'true' o 'false'.");
                                break;

                            }

                            if (taskService.UpdateTask(updateId, newTitle, newDescription, isCompleted))
                            {
                                Console.WriteLine("Tarea actualizada exitosamente.");
                            }
                            else
                            {
                                Console.WriteLine("Tarea no encontrada.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Tarea no encontrada.");
                        }
                        break;
                    case "4":
                        Console.Write("Introduzca el Id de la tarea a eliminar: ");
                        if (!int.TryParse(Console.ReadLine(), out var deleteId))
                        {
                            Console.WriteLine("Id inválido. Por favor, ingrese un número válido.");
                            break;
                        }

                        task = taskService.GetTaskById(deleteId);
                        if (task != null)
                        {
                            Console.WriteLine("Tarea:");
                            Console.Write("     - ");
                            Console.WriteLine(task.Title);

                            if (Confirme("eliminar"))
                            {
                                if (taskService.DeleteTask(deleteId))
                                {
                                    Console.WriteLine("Tarea eliminada exitosamente.");
                                }
                                else
                                {
                                    Console.WriteLine("Tarea no encontrada.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Tarea no encontrada.");
                        }
                        break;
                    case "5":
                        Console.Write("Introduzca el Id de la tarea a completar: ");
                        if (!int.TryParse(Console.ReadLine(), out var completeId))
                        {
                            Console.WriteLine("Id inválido. Por favor, ingrese un número válido.");
                            break;
                        }

                        task = taskService.GetTaskById(completeId);
                        if (task != null)
                        {
                            Console.WriteLine($"Tarea: {task.Title}");

                            if (taskService.CompleteTask(completeId))
                            {
                                Console.WriteLine("Tarea completada exitosamente.");
                            }
                            else
                            {
                                Console.WriteLine("No se pudo completar la tarea.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Tarea no encontrada.");
                        }
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Opción inválida, intente de nuevo.");
                        break;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error de formato: {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: Un valor requerido es nulo. {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operación inválida: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se ha producido un error inesperado: {ex.Message}");
            }
        }
    }
}
