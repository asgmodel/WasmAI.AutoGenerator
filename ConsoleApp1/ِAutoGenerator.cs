using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Reflection;


namespace AutoGenerator;

// واجهة لتوليد الكود
public interface ITGenerator
{
    string Generate(GenerationOptions options);
    void SaveToFile(string filePath);
}

public static class CodeGeneratorUtils
{
    public static string GetPropertyTypeName(Type propertyType)
    {
        if (propertyType.IsGenericType)
        {
            var genericArguments = propertyType.GetGenericArguments();
            return $"{propertyType.Name.Substring(0, propertyType.Name.IndexOf('`'))}<{string.Join(", ", genericArguments.Select(GetPropertyTypeName))}>";
        }
        else if (propertyType.IsNullableType())
        {
            var underlyingType = Nullable.GetUnderlyingType(propertyType);
            return underlyingType != null ? underlyingType.Name : propertyType.Name;
        }
        else
        {
            return propertyType.Name;
        }
    }

    public static bool IsNullableType(this Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    // تطبيق القالب على النص
    public static string ApplyTemplate(string template, Dictionary<string, string> replacements)
    {
        foreach (var replacement in replacements)
        {
            template = template.Replace($"{{{replacement.Key}}}", replacement.Value);
        }
        return template;
    }
}

public class GenerationOptions
{
    public string ClassName { get; set; }
    public Type SourceType { get; set; }
    public string NamespaceName { get; set; } = "GeneratedClasses";
    public string AdditionalCode { get; set; } = "";
    public List<Type> Interfaces { get; set; } = new List<Type>();

    public List<string> Usings { get; set; } = new List<string>();
    public Type BaseClass { get; set; } = null;
    public string Template { get; set; } = @"
        public class {ClassName} {BaseClass} {Interfaces}
        {
            {Properties}
            {AdditionalCode}
        }
    ";
}

// فئة مولد الكود التي تنفذ الواجهة ITGenerator
public class GenericClassGenerator : ITGenerator
{
    private string generatedCode; // متغير لتخزين الكود المتولد

    public string Generate(GenerationOptions options)
    {
        var properties = options.SourceType.GetProperties();
        var propertyDeclarations = new List<string>();

        foreach (var prop in properties)
        {
            propertyDeclarations.Add($@"
                public {CodeGeneratorUtils.GetPropertyTypeName(prop.PropertyType)}{(prop.PropertyType.IsNullableType() ? "" : "")} {prop.Name} {{ get; set; }}
            ");
        }

        var baseClass = options.BaseClass != null ? $": {options.BaseClass.Name}" : "";
        if (options.BaseClass != null && options.Interfaces.Any())
        {
            baseClass += ", ";
        }
        else if (options.BaseClass == null && options.Interfaces.Any())
        {
            baseClass = ": ";
        }
        var interfaces = options.Interfaces.Any() ? $" {string.Join(", ", options.Interfaces.Select(i => i.Name))}" : "";

        var replacements = new Dictionary<string, string>
        {
            { "ClassName", options.ClassName },
            { "Properties", string.Join("//", propertyDeclarations) },
            { "AdditionalCode", options.AdditionalCode },
            { "Interfaces", interfaces },
            { "BaseClass", baseClass }
        };

        generatedCode = CodeGeneratorUtils.ApplyTemplate(options.Template, replacements);

        var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(options.NamespaceName))
            .AddMembers(SyntaxFactory.ParseMemberDeclaration(generatedCode));

        options.Usings.Add("System");
        List<UsingDirectiveSyntax> usingDirectives = new List<UsingDirectiveSyntax>();

        foreach (var ns in options.Usings)
        {

            usingDirectives.Add(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(ns)));
        }

        var compilationUnit = SyntaxFactory.CompilationUnit()
            .AddUsings(usingDirectives.ToArray())
            .AddMembers(namespaceDeclaration)
            .NormalizeWhitespace();

        generatedCode = compilationUnit.ToFullString(); // تحديث الكود المتولد

        return generatedCode;
    }

    

    public void SaveToFile(string filePath)
    {
        if (!string.IsNullOrEmpty(generatedCode))
        {
            File.WriteAllText(filePath, generatedCode);
            Console.WriteLine($"Generated code saved to {filePath}");
        }
        else
        {
            Console.WriteLine("No generated code to save.");
        }
    }
}


  
public class DynamicClassGenerator2
{
    public static void Main()
    {
        Type modelType = typeof(Invoice);
        var options = new GenerationOptions
        {
            ClassName = "InvoiceTT",
            SourceType = modelType,
            NamespaceName = "MyDtos",
            AdditionalCode = @"
              
                public void PrintDetails()
                {
                    Console.WriteLine($""Id: {Id}, CustomerId: {CustomerId}, Status: {Status}"");
                }
            ",
            Interfaces = new List<Type> { typeof(IDto) },
            BaseClass = typeof(BaseDto),
            Usings = new List<string> { "Microsoft.CodeAnalysis" }
        };

        ITGenerator generator = new GenericClassGenerator();
        generator.Generate(options);

        // الحصول على مسار المشروع (قبل bin)
        string projectPath = Directory.GetCurrentDirectory().Split("bin")[0];
        string jsonFile = Path.Combine(projectPath, $"{options.ClassName}.cs");

        generator.SaveToFile(jsonFile);
    }


}
public class DtoGenerator : GenericClassGenerator, ITGenerator
{
    
     
    public new string Generate(GenerationOptions options)
    {
       
        
        string generatedCode = base.Generate(options);

      

        return generatedCode;
    }

    // تجاوز دالة SaveToFile لتخصيص حفظ الملف
    public new void SaveToFile(string filePath)
    {
        // يمكنك إضافة منطق خاص لحفظ ملف DTO هنا
        // على سبيل المثال، إضافة رأس ملف خاص لـ DTO

        base.SaveToFile(filePath);
    }

    // دالة جديدة خاصة بتوليد خصائص DTO مع تعليقات توضيحية
    public  List<string> GenerateDtoProperties(PropertyInfo[] properties)
    {
        var propertyDeclarations = new List<string>();

        foreach (var prop in properties)
        {
            propertyDeclarations.Add($@"
                /// <summary>
                /// {prop.Name} property for DTO.
                /// </summary>
                public {CodeGeneratorUtils.GetPropertyTypeName(prop.PropertyType)}{(prop.PropertyType.IsNullableType() ? "?" : "")} {prop.Name} {{ get; set; }}
            ");
        }

        return propertyDeclarations;
    }
}


public class GeneratorManager
{
    private Dictionary<string, ITGenerator> generators = new Dictionary<string, ITGenerator>();

    public void RegisterGenerator(string generatorName, ITGenerator generator)
    {
        generators[generatorName] = generator;
    }

    public ITGenerator GetGenerator(string generatorName)
    {
        if (generators.TryGetValue(generatorName, out ITGenerator generator))
        {
            return generator;
        }
        else
        {
            return null; // أو يمكنك طرح استثناء
        }
    }

    public string GenerateCode(string generatorName, GenerationOptions options)
    {
        ITGenerator generator = GetGenerator(generatorName);
        if (generator != null)
        {
            return generator.Generate(options);
        }
        else
        {
            return null; // أو يمكنك طرح استثناء
        }
    }

    public void SaveCodeToFile(string generatorName, string filePath, GenerationOptions options)
    {
        ITGenerator generator = GetGenerator(generatorName);
        if (generator != null)
        {
            string generatedCode = generator.Generate(options);
            if (!string.IsNullOrEmpty(generatedCode))
            {
                File.WriteAllText(filePath, generatedCode);
                Console.WriteLine($"Generated code saved to {filePath}");
            }
            else
            {
                Console.WriteLine("No generated code to save.");
            }
        }
    }
}
public interface IDto { }
public class BaseDto { }