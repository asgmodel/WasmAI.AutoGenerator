using AutoGenerator.ApiFolder;
using AutoGenerator.Code;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using System.Text;

public class GenericClassGenerator : ITGenerator
{
    private string generatedCode = string.Empty; // «· √ﬂœ „‰ ⁄œ„ ÊÃÊœ ﬁÌ„… €Ì— „ÂÌ√…
    public event EventHandler<string>? OnCodeGenerated;
    public event EventHandler<string>? OnCodeSaved;

    private static  readonly HashSet<ITGenerator>  tGenerators= new HashSet<ITGenerator>();
    public bool IsEditFile { get;  set; } = false;

    private static StringBuilder BatchBuilder = new StringBuilder();

    public static  HashSet<ITGenerator> TGenerators { get { return tGenerators; } }
    public string Generate(GenerationOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options), "Generation options cannot be null.");
        if (string.IsNullOrWhiteSpace(options.ClassName))
            throw new ArgumentException("Class name cannot be null or empty.");
       
        if (options.Template == null)
            throw new ArgumentException("Template cannot be null.");

        var propertyDeclarations = new List<string>();
        if (options.Properties != null)
        {
            foreach (var prop in options.Properties)
            {
                if (prop == null || prop.PropertyType == null)
                    continue; //  ŒÿÌ «·Œ«’Ì… ≈–« ﬂ«‰  €Ì— ’«·Õ…

                propertyDeclarations.Add($@"
                public {CodeGeneratorUtils.GetPropertyTypeName(prop.PropertyType)}{(prop.PropertyType.IsNullableType() ? "" : "")} {prop.Name} {{ get; set; }}");
            }
        }

        var baseClass = string.Empty;
        if (options.BaseClass != null)
        {
            baseClass = $": {options.BaseClass}";
            if (options.Interfaces.Any())
                baseClass += ", ";
        }
        else if (options.Interfaces.Any())
        {
            baseClass = ": ";
        }

        var interfaces = options.Interfaces.Any() ? $"{string.Join(", ", options.Interfaces.Select(i => i.Name))}" : "";
        var replacements = new Dictionary<string, string>
        {
            { "ClassName", options.ClassName },
            { "Properties", string.Join("//", propertyDeclarations) },
            { "AdditionalCode", options.AdditionalCode ?? "" },
            { "Interfaces", interfaces },
            { "BaseClass", baseClass }
        };

        generatedCode = CodeGeneratorUtils.ApplyTemplate(options.Template, replacements);

        var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(options.NamespaceName))
            .AddMembers(SyntaxFactory.ParseMemberDeclaration(generatedCode));

        if (!options.Usings.Contains("System"))
            options.Usings.Add("System");


        List<UsingDirectiveSyntax> usingDirectives = options.Usings
            .Where(ns => !string.IsNullOrWhiteSpace(ns))
            .Select(ns => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(ns)))
            .ToList();

        var compilationUnit = SyntaxFactory.CompilationUnit()
          .AddUsings(usingDirectives.ToArray())
          .AddMembers(namespaceDeclaration)
          .NormalizeWhitespace();

        generatedCode = compilationUnit.ToFullString(); //  ÕœÌÀ «·ﬂÊœ «·„ Ê·œ

        OnCodeGenerated?.Invoke(this, generatedCode);
        tGenerators.Add(this);
        return generatedCode;
    }

    public void SaveToFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        if (string.IsNullOrEmpty(generatedCode))
        {
            Console.WriteLine("No generated code to save.");
            return;
        }

        try
        {


            if (!File.Exists(filePath) || IsEditFile)
            {
                File.WriteAllText(filePath, generatedCode);
                Console.WriteLine($"Generated code saved to {filePath}");
                OnCodeSaved?.Invoke(this, filePath);

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
           
        }
    }
}