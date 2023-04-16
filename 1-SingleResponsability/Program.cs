using SingleResponsability;

StudentRepository studentRepository = new();
ExportHelper<Student> exportHelper = new();
exportHelper.ExportStudents(studentRepository.GetAll());
Console.WriteLine("Proceso Completado");