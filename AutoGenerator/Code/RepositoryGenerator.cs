using AutoGenerator.ApiFolder;
using AutoGenerator.Helper.Translation;
using AutoGenerator.TM;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Reflection;
using System.Text;

namespace AutoGenerator.Code;

public class RepositoryGenerator : GenericClassGenerator, ITGenerator
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
        public Func<string, string,string> ActionM { get; set; }

        
    }

    private static readonly string TAG = "Repositories";
    public static void GenerateBaseRep(string root,string type,string subtype,string pathfile)
    {


        //base
        ITGenerator generator = new RepositoryGenerator();

        var usings = new List<string>()
        {

            "AutoGenerator.Repositories.Base",
            ApiFolderInfo.TypeContext.Namespace,
            ApiFolderInfo.TypeIdentityUser.Namespace,
            "Microsoft.AspNetCore.Identity"

        };

        var options = new GenerationOptions("BaseRepository",typeof(RepositoryGenerator),isProperties:false)
        {

            NamespaceName = $"{root}.{type}.Base",
            Template = TmBaseRepository.GetTmBaseRepository("BaseRepository"),
            Usings = usings
        };

        var generatedCode = generator.Generate(options);

        string jsonFile = Path.Combine(pathfile, $"{subtype}/BaseRepository.cs");
        generator.SaveToFile(jsonFile);

        Console.WriteLine($"✅ {options.ClassName} has been created successfully!");

        usings = new List<string>()
        {

           
            ApiFolderInfo.TypeContext.Namespace,
            ApiFolderInfo.TypeIdentityUser.Namespace,
            "AutoMapper",
            "AutoGenerator",
            "AutoGenerator.Repositories.Builder"

        };

        var options2 = new GenerationOptions("BaseRepository", typeof(RepositoryGenerator), isProperties: false)
        {

            NamespaceName = $"{root}.{type}.Base",
            Template = TmBaseRepository.GetTmBaseBuilderRepository("BaseRepository"),
            Usings = usings
        };

         generatedCode = generator.Generate(options2);

        jsonFile = Path.Combine(pathfile, $"{subtype}/BaseBuilderRepository.cs");
        generator.SaveToFile(jsonFile);

        Console.WriteLine($"✅ {options2.ClassName} has been created successfully!");







    }
    public static void GenerateAll(string root,string type, string subtype, string NamespaceName, string pathfile)
    {


        var assembly = ApiFolderInfo.AssemblyModels;
        var nameContext = ApiFolderInfo.TypeContext.Name;

        var models = assembly.GetTypes().Where(t => typeof(ITModel).IsAssignableFrom(t) && t.IsClass).ToList();




        var funcs = subtype != "Share" ?
            new List<ActionServ>() {
             new(){ActionM=createTIRBuild,Name="I"},
             new(){ActionM=createTIRbodyBuild,Name=""}
            } :
         new List<ActionServ>() {
             new(){ActionM=createTIRShare,Name="I"},
             new(){ActionM=createTIBodyShare,Name=""}
            };

       



        NamespaceName = $"{root}.{TAG}.{subtype}";

        foreach (var model in models)
        {


            CreateFolder(pathfile, $"{subtype}/{model.Name}");
            foreach (var func in funcs)
            {
                var options = new GenerationOptions($"{model.Name}{type}", model)
                {
                    NamespaceName = NamespaceName,
                    Template =func.ActionM(model.Name,type) ,
                    Usings = new List<string>
                        {
                            
                            "AutoMapper",
                            ApiFolderInfo.TypeContext.Namespace,
                            ApiFolderInfo.TypeIdentityUser.Namespace,
                       
                           $"{root}.{type}.Base",
                           $"AutoGenerator.{TAG}.Builder",
                           $"{root}.DyModels.Dto.Build.Requests",
                           $"{root}.DyModels.Dto.Build.Responses",
                            

                        }


                };



                if (subtype == "Share")
                {

                    options.Usings.AddRange(new List<string> {



                            "AutoGenerator",
                            $"{root}.{TAG}.Builder",
                            $"AutoGenerator.{TAG}.Share",
                            "System.Linq.Expressions",
                           $"AutoGenerator.{TAG}.Base",
                            "AutoGenerator.Helper",
                           $"{root}.DyModels.Dto.Share.Requests",
                           $"{root}.DyModels.Dto.Share.Responses",
                });

                }




                    ITGenerator generator = new RepositoryGenerator();
                    generator.Generate(options);

                    string jsonFile = Path.Combine(pathfile, $"{subtype}/{model.Name}/{func.Name}{model.Name}{subtype}Repository.cs");
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


    static string createTIRBuild(string className, string type)
    {

        return $@"  /// <summary>
                 /// {className} interface property for BuilderRepository.
           /// </summary>
         public interface I{className}BuilderRepository<TBuildRequestDto,TBuildResponseDto>  
             : IBaseBuilderRepository<TBuildRequestDto,TBuildResponseDto> //
             where TBuildRequestDto : class  //
             where TBuildResponseDto : class //
         {{
             // Define methods or properties specific to the builder interface.
         }}
         ";
    }

    static string createTIRbodyBuild(string className, string type)
    {

        return $@"  /// <summary>
                 /// {className} class property for BuilderRepository.
           /// </summary>
         //
         

        public class {className}BuilderRepository   
               : BaseBuilderRepository<{className},{className}RequestBuildDto,{className}ResponseBuildDto>,  
                 I{className}BuilderRepository<{className}RequestBuildDto,{className}ResponseBuildDto>
         {{
                       /// <summary>
                         /// Constructor for {className}BuilderRepository.
                   /// </summary>


             public {className}BuilderRepository({ApiFolderInfo.TypeContext.Name} dbContext,
                                               IMapper mapper, ILogger logger) 
                 : base(dbContext, mapper, logger) // Initialize  constructor.
             {{
                 // Initialize necessary fields or call base constructor.
                ///
                /// 

       
                /// 
             }}


         //

          // Add additional methods or properties as needed.
         }}
         ";
    }
    static string createTIRShare(string className ,string type)
    {

        return $@"
                 /// <summary>
                /// {className} interface for {type}Repository.
                /// </summary>
               public interface I{className}ShareRepository 
                                : IBaseShareRepository<{className}RequestShareDto, {className}ResponseShareDto> //
                               
                               ,IBasePublicRepository<{className}RequestShareDto, {className}ResponseShareDto>

                               //  يمكنك  التزويد بكل  دوال   طبقة Builder   ببوابات  الطبقة   هذه نفسها      
                               //,I{className}BuilderRepository<{className}RequestShareDto, {className}ResponseShareDto>
                        {{
                            // Define methods or properties specific to the share repository interface.
                        }}";
            }



    static string createTIBodyShare(string className, string type)
    {

        return TmShareRepository.GetTmShareRepository(className);
    }

    private static string[] UseRepositories = new string[] { "Builder", "Share" };
    public static void GeneratWithFolder(FolderEventArgs e)
    {
        foreach (var node in e.Node.Children)
        {
            var root = ApiFolderInfo.ROOT.Name;

            if (UseRepositories.Contains(node.Name))

                GenerateAll(root, e.Node.Name, node.Name, node.Name, e.FullPath);
            else
                GenerateBaseRep(root, e.Node.Name, node.Name, e.FullPath);
            //GenerateAll(e.Node.Name, node.Name, node.Name, e.FullPath);



        }
    }



}

