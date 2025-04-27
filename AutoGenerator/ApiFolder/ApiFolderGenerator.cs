using AutoGenerator.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.PortableExecutable;
namespace AutoGenerator.ApiFolder;


internal  class ApiFolderInfo
{


    public static FolderNode? ROOT { get; set; }
    public static string? AbsolutePath;

    public static Type? TypeContext { get; set; }

    public static Type? TypeIdentityUser { get; set; }


    public static  Assembly? AssemblyShare { get;  set; }

    public static Assembly? AssemblyModels { get; set; }




}
public class ApiFolderGenerator
{









    public static void Build(string projectPath, string nameRoot = "Api")
    {

        if (string.IsNullOrEmpty(projectPath))
        {
            projectPath = Directory.GetCurrentDirectory().Split("bin")[0];
        }
        string jsonFilePath = Path.Combine(projectPath, projectPath);

        FolderStructureReader folderReader = new FolderStructureReader();
        folderReader.FolderCreated += OnCreateFolders;

        folderReader.FileCreating += OnCreateFiles;
        folderReader.LoadFromJson(jsonFilePath);

        var root = folderReader.BuildFolderTree(nameRoot);

        ApiFolderInfo.ROOT = root;
        ApiFolderInfo.AbsolutePath = projectPath;


        folderReader.PrintFolderTree(root);
        folderReader.CreateFolders(projectPath, root);
     

        folderReader.OnAfterCreatedFolders(projectPath, root);

        Console.WriteLine("✅ All folders have been created successfully!");


        

    }

    private static void OnCreateFiles(object? sender, FileEventArgs e)
    {



       
    }

    private static void OnCreateFolders(object? sender, FolderEventArgs e)
    {
        if (e.Node.Name == "Dto")
        {


            DtoGenerator.GeneratWithFolder(e);


        }
        else if (e.Node.Name == "Dso")
        {

            DsoGenerator.GeneratWithFolder(e);
        }

        else if (e.Node.Name == "Repositories")
        {
            RepositoryGenerator.GeneratWithFolder(e);
        }

        else if (e.Node.Name == "Services")
        {
            ServiceGenerator.GeneratWithFolder(e);
        }
        else if (e.Node.Name == "Controllers")
        {
            ControllerGenerator.GeneratWithFolder(e);
        }
        else if (e.Node.Name == "VM")
        {
            VMGenerator.GeneratWithFolder(e);
        }
        else if (e.Node.Name == "Validators")
        {

            ValidatorGenerator.GeneratWithFolder(e);
        }

        else if(e.Node.Name == "Schedulers")
        {
            SchedulerGenerator.GeneratWithFolder(e);

        }
    }


}

   
