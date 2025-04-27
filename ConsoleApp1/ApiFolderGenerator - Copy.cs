using System;
using System.Linq;
using System.Reflection;
using System.Text;

public static class ModelCodeGenerator
{
    public static void GenerateCodeForModel(Type modelType)
    {
        // توليد الواجهة الخاصة بالموديل
        string interfaceCode = GenerateInterfaceForModel(modelType);
        Console.WriteLine("----- Interface for Model -----");
        Console.WriteLine(interfaceCode);
        Console.WriteLine();

        // توليد كلاس التطبيق (Model) الذي ينفذ الواجهة
        string classCode = GenerateClassForModel(modelType);
        Console.WriteLine("----- Model Class -----");
        Console.WriteLine(classCode);
        Console.WriteLine();

        // توليد قوالب للـ BuildDto، ShareDto، وVM اعتماداً على الموديل
        string buildDtoCode = GenerateDtoCode(modelType, "Build");
        Console.WriteLine("----- Build DTO -----");
        Console.WriteLine(buildDtoCode);
        Console.WriteLine();

        string shareDtoCode = GenerateDtoCode(modelType, "Share");
        Console.WriteLine("----- Share DTO -----");
        Console.WriteLine(shareDtoCode);
        Console.WriteLine();

        string vmCode = GenerateVMCode(modelType);
        Console.WriteLine("----- ViewModel -----");
        Console.WriteLine(vmCode);
        Console.WriteLine();
    }

    private static string GenerateInterfaceForModel(Type modelType)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"public interface I{modelType.Name} : ITModel");
        sb.AppendLine("{");
        foreach (var prop in modelType.GetProperties())
        {
            sb.AppendLine($"    {prop.PropertyType.Name} {prop.Name} {{ get; set; }}");
        }
        sb.AppendLine("}");
        return sb.ToString();
    }

    private static string GenerateClassForModel(Type modelType)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"public class {modelType.Name} : I{modelType.Name}");
        sb.AppendLine("{");
        foreach (var prop in modelType.GetProperties())
        {
            sb.AppendLine($"    public {prop.PropertyType.Name} {prop.Name} {{ get; set; }}");
        }
        sb.AppendLine("}");
        return sb.ToString();
    }

    private static string GenerateDtoCode(Type modelType, string dtoType)
    {
        // dtoType: "Build" أو "Share"
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"public interface I{modelType.Name}{dtoType}Dto : IT{dtoType}Dto");
        sb.AppendLine("{");
        // لنفترض أننا نستخدم جميع الخصائص من الموديل
        foreach (var prop in modelType.GetProperties())
        {
            // إذا كان dto من نوع Build فقد نحتاج كل الخصائص
            sb.AppendLine($"    {prop.PropertyType.Name} {prop.Name} {{ get; set; }}");
        }
        sb.AppendLine("}");
        sb.AppendLine();
        sb.AppendLine($"public class {modelType.Name}{dtoType}Dto : I{modelType.Name}{dtoType}Dto");
        sb.AppendLine("{");
        foreach (var prop in modelType.GetProperties())
        {
            sb.AppendLine($"    public {prop.PropertyType.Name} {prop.Name} {{ get; set; }}");
        }
        sb.AppendLine("}");
        return sb.ToString();
    }

    private static string GenerateVMCode(Type modelType)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"public interface I{modelType.Name}VM : ITVM");
        sb.AppendLine("{");
        foreach (var prop in modelType.GetProperties())
        {
            sb.AppendLine($"    {prop.PropertyType.Name} {prop.Name} {{ get; set; }}");
        }
        sb.AppendLine("}");
        sb.AppendLine();
        sb.AppendLine($"public class {modelType.Name}VM : I{modelType.Name}VM");
        sb.AppendLine("{");
        foreach (var prop in modelType.GetProperties())
        {
            sb.AppendLine($"    public {prop.PropertyType.Name} {prop.Name} {{ get; set; }}");
        }
        sb.AppendLine("}");
        return sb.ToString();
    }
}

// مثال على استخدام الكود، حيث تقوم بإعطاء الموديل (مثلاً Invoice)
public class Invoice
{
    public string Id { get; set; }
    public int Itd { get; set; }
    public string CustomerId { get; set; }
    public string Status { get; set; }
    public string Url { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public string Description { get; set; }
}

public interface ITModel { }
public interface ITBuildDto { }
public interface ITShareDto { }
public interface ITVM { }
public interface ITTransient { }
public interface ITSingleton { }
public interface ITScope { }

public class DynamicClassGenerator
{
    public static void Main()
    {
        // استخدم الموديل الذي تزوّده (في هذا المثال Invoice)
        ModelCodeGenerator.GenerateCodeForModel(typeof(Invoice));
    }
}
