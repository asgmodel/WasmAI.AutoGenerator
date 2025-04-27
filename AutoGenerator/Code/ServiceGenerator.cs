using AutoGenerator.ApiFolder;
using AutoGenerator.Helper.Translation;
using AutoGenerator.TM;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text;


namespace AutoGenerator.Code;

public class ServiceGenerator : GenericClassGenerator, ITGenerator
{

    public new string Generate(GenerationOptions options)
    {

        string generatedCode = base.Generate(options);



        return generatedCode;
    }

    public new void SaveToFile(string filePath)
    {


        base.SaveToFile(filePath);
    }

    class ActionServ
    {

        public string Name { get; set; }
        public Func<string, string> ActionM { get; set; }
    }

        public static void GenerateAll(string type, string subtype, string NamespaceName, string pathfile)
        {


        var assembly = ApiFolderInfo.AssemblyModels;


        var models = assembly.GetTypes().Where(t => typeof(ITModel).IsAssignableFrom(t) && t.IsClass).ToList();




            var root = ApiFolderInfo.ROOT.Name;

            var funcs = new List<ActionServ>() {
            new(){ActionM=createTIBS ,Name="IT"}  ,
            new(){ActionM=createTIUseS ,Name="IUse"}  ,
             new(){ActionM=createTCS ,Name=""}
             };


            NamespaceName = $"{root}.Services.{subtype}";


            foreach (var model in models)
            {
                CreateFolder(pathfile, $"{model.Name}");

                foreach (var func in funcs)
                {
                    var options = new GenerationOptions($"{model.Name}{type}", model)
                    {
                        NamespaceName = NamespaceName,
                        Template = func.ActionM(model.Name)
                                    ,
                        Usings = new List<string>
                        {
                            "AutoGenerator",
                            "AutoMapper",

                            "Microsoft.Extensions.Logging",
                            "System.Collections.Generic",

                            "AutoGenerator.Services.Base",
                            $"{root}.DyModels.Dso.Requests",
                            $"{root}.DyModels.Dso.Responses",
                            ApiFolderInfo.TypeIdentityUser.Namespace,
                            $"{root}.DyModels.Dto.Share.Requests",
                            $"{root}.DyModels.Dto.Share.Responses",


                            $"{root}.Repositories.Share",
                            "System.Linq.Expressions",
                            $"{root}.Repositories.Builder",
                            "AutoGenerator.Repositories.Base",
                            "AutoGenerator.Helper"



                        }


                    };








                    ITGenerator generator = new ServiceGenerator();
                    generator.Generate(options);

                    string jsonFile = Path.Combine(pathfile, $"{model.Name}/{func.Name}{model.Name}Service.cs");
                    generator.SaveToFile(jsonFile);

                    Console.WriteLine($"✅ {options.ClassName} has been created successfully!");
                }
            }
        }









        private static void CreateFolder(string path, string namemodel)
        {

            string folderPath = Path.Combine(path, namemodel);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

        }


        private static string createTIBS(string className)
        {


            return $@"
public interface I{className}Service<TServiceRequestDso, TServiceResponseDso> 
    where TServiceRequestDso : class 
    where TServiceResponseDso : class 
{{ 
}} 

";
        }



        private static string createTIUseS(string className)
        {


            return $@"
                public interface IUse{className}Service : I{className}Service<{className}RequestDso, {className}ResponseDso>, IBaseService 
                          //يمكنك  التزويد بكل  دوال   طبقة Builder   ببوابات  الطبقة   هذه نفسها
                         //, I{className}BuilderRepository<{className}RequestDso, {className}ResponseDso>
                         ,IBasePublicRepository<{className}RequestDso, {className}ResponseDso>
               
                {{ 
                }} 


";
        }

        private static string createTCS(string className)
        {
            return TmService.GetTmService(className);

        return $@"
public class {className}Service : BaseService<{className}RequestDso, 
{className}ResponseDso>, IUse{className}Service
{{
    private readonly I{className}ShareRepository _builder;
    private readonly ILogger _logger;

    public {className}Service(I{className}ShareRepository {className.ToLower()}ShareRepository, 
                              IMapper mapper, 
                              ILoggerFactory logger) : base(mapper, logger)
    {{
        _builder = {className.ToLower()}ShareRepository;
        _logger = logger.CreateLogger(typeof({className}Service).FullName);
    }}

    public override Task<int> CountAsync()
    {{
        try
        {{
            _logger.LogInformation(""Counting {className} entities..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, ""Error in CountAsync for {className} entities."");
            return Task.FromResult(0);
        }}
    }}

    public override async Task<{className}ResponseDso> CreateAsync({className}RequestDso entity)
    {{
        try
        {{
            _logger.LogInformation(""Creating new {className} entity..."");
            var result = await _builder.CreateAsync(entity);
            var output = ({className}ResponseDso)result;
            _logger.LogInformation(""Created {className} entity successfully."");
            return output;
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, ""Error while creating {className} entity."");
            return null;
        }}
    }}

    public override Task<IEnumerable<{className}ResponseDso>> CreateRangeAsync(IEnumerable<{className}RequestDso> entities)
    {{
        try
        {{
            _logger.LogInformation(""Creating a range of {className} entities..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, ""Error in CreateRangeAsync for {className} entities."");
            return Task.FromResult<IEnumerable<{className}ResponseDso>>(null);
        }}
    }}

    public override Task DeleteAsync(string id)
    {{
        try
        {{
            _logger.LogInformation($""Deleting {className} entity with ID: {{id}}..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, $""Error while deleting {className} entity with ID: {{id}}."");
            return Task.CompletedTask;
        }}
    }}

    public override  Task DeleteRangeAsync(Expression<Func<{className}ResponseDso, bool>> predicate)
    {{
        try
        {{
            _logger.LogInformation(""Deleting a range of {className} entities based on condition..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, ""Error in DeleteRangeAsync for {className} entities."");
            return Task.CompletedTask;
        }}
    }}

    public override  Task<bool> ExistsAsync(Expression<Func<{className}ResponseDso, bool>> predicate)
    {{
        try
        {{
            _logger.LogInformation(""Checking existence of {className} entity based on condition..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, ""Error in ExistsAsync for {className} entity."");
            return Task.FromResult(false);
        }}
    }}

    public override Task<{className}ResponseDso?> FindAsync(Expression<Func<{className}ResponseDso, bool>> predicate)
    {{
        try
        {{
            _logger.LogInformation(""Finding {className} entity based on condition..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, ""Error in FindAsync for {className} entity."");
            return Task.FromResult<{className}ResponseDso>(null);
        }}
    }}

    public override Task<IEnumerable<{className}ResponseDso>> GetAllAsync()
    {{
        try
        {{
            _logger.LogInformation(""Retrieving all {className} entities..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, ""Error in GetAllAsync for {className} entities."");
            return Task.FromResult<IEnumerable<{className}ResponseDso>>(null);
        }}
    }}

    public override Task<{className}ResponseDso?> GetByIdAsync(string id)
    {{
        try
        {{
            _logger.LogInformation($""Retrieving {className} entity with ID: {{id}}..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, $""Error in GetByIdAsync for {className} entity with ID: {{id}}."");
            return Task.FromResult<{className}ResponseDso>(null);
        }}
    }}

    public  Task<{className}ResponseDso> getData(int id)
    {{
        try
        {{
            _logger.LogInformation($""Getting data for {className} entity with numeric ID: {{id}}..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, $""Error in getData for {className} entity with numeric ID: {{id}}."");
            return Task.FromResult<{className}ResponseDso>(null);
        }}
    }}

    public override IQueryable<{className}ResponseDso> GetQueryable()
    {{
        try
        {{
            _logger.LogInformation(""Retrieving IQueryable<{className}ResponseDso> for {className} entities..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, ""Error in GetQueryable for {className} entities."");
            return null;
        }}
    }}

    public  Task SaveChangesAsync()
    {{
        try
        {{
            _logger.LogInformation(""Saving changes to the database for {className} entities..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, ""Error in SaveChangesAsync for {className} entities."");
            return Task.CompletedTask;
        }}
    }}

    public override  Task<{className}ResponseDso> UpdateAsync({className}RequestDso entity)
    {{
        try
        {{
            _logger.LogInformation(""Updating {className} entity..."");
            throw new NotImplementedException();
        }}
        catch(Exception ex)
        {{
            _logger.LogError(ex, ""Error in UpdateAsync for {className} entity."");
            return Task.FromResult<{className}ResponseDso>(null);
        }}
    }}
}}";
        }




        private static string[] UseRepositorys = new string[] { "Builder", "Share" };
        public static void GeneratWithFolder(FolderEventArgs e)
        {

            GenerateAll(e.Node.Name, e.Node.Name, e.Node.Name, e.FullPath);

            //GenerateAll(e.Node.Name, node.Name, node.Name, e.FullPath);




        }



    }


