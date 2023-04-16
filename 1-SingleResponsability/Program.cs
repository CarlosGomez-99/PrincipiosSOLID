using SingleResponsability;

StudentRepository studentRepository = new();
ExportHelper exportHelper = new ExportHelper();
exportHelper.ExportStudents(studentRepository.GetAll());
Console.WriteLine("Proceso Completado");