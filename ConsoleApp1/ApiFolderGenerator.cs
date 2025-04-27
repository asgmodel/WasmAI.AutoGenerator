using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace ttt;
public class FolderStructureReader
{
    // تمثيل العقدة في شجرة المجلدات
    public class FolderNode
    {
        public string Name { get; set; }
        public List<FolderNode> Children { get; set; } = new List<FolderNode>();

        public FolderNode(string name)
        {
            Name = name;
        }
    }

    private dynamic folderStructure; // استخدام dynamic لقراءة الـ JSON بشكل ديناميكي

    // تحميل الهيكلية من ملف JSON
    public void LoadFromJson(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                folderStructure = JsonConvert.DeserializeObject<dynamic>(json);
                Console.WriteLine("تم تحميل الهيكلية من الملف.");
            }
            else
            {
                Console.WriteLine($"❌ الملف {filePath} غير موجود.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ حدث خطأ أثناء تحميل الهيكلية: {ex.Message}");
        }
    }

    // تحويل الهيكلية إلى شجرة FolderNode
    public FolderNode BuildFolderTree(string folderName, dynamic structure = null)
    {
        structure = structure ?? folderStructure;
        FolderNode node = new FolderNode(folderName);

        if (structure is JObject)
        {
            foreach (var property in ((JObject)structure).Properties())
            {
                FolderNode childNode = BuildFolderTree(property.Name, property.Value);
                node.Children.Add(childNode);
            }
        }
        else if (structure is JArray)
        {
            foreach (var item in (JArray)structure)
            {
                if (item.Type == JTokenType.String)
                {
                    FolderNode childNode = new FolderNode(item.ToString());
                    node.Children.Add(childNode);
                }
                else if (item is JObject)
                {
                    foreach (var property in ((JObject)item).Properties())
                    {
                        FolderNode childNode = BuildFolderTree(property.Name, property.Value);
                        node.Children.Add(childNode);
                    }
                }
            }
        }
        else if (structure is JValue)
        {
            // في حالة كانت القيمة مجرد نص
            node.Children.Add(new FolderNode(structure.ToString()));
        }

        return node;
    }

    // طباعة الشجرة البيانية (للتأكد من الهيكلية)
    public void PrintFolderTree(FolderNode node, string indent = "")
    {
        Console.WriteLine(indent + node.Name);
        foreach (var child in node.Children)
        {
            PrintFolderTree(child, indent + "  ");
        }
    }

    // إنشاء المجلدات على النظام بناءً على شجرة المجلدات
    public void CreateFolders(string basePath, FolderNode node)
    {
        try
        {
            string folderPath = Path.Combine(basePath, node.Name);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine($"تم إنشاء المجلد: {folderPath}");
            }

            foreach (var child in node.Children)
            {
                CreateFolders(folderPath, child);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ حدث خطأ أثناء إنشاء المجلدات: {ex.Message}");
        }
    }
}

public class ApiFolderGenerator
{
    public static void Main()
    {
        // الحصول على مسار المشروع (قبل bin)
        string projectPath = Directory.GetCurrentDirectory().Split("bin")[0];
        string jsonFilePath = Path.Combine(projectPath, "folderStructure.json");

        // إنشاء كائن FolderStructureReader وتحميل الـ JSON
        FolderStructureReader folderReader = new FolderStructureReader();
        folderReader.LoadFromJson(jsonFilePath);

        // بناء شجرة المجلدات بدءًا من "Root"
        var root = folderReader.BuildFolderTree("Api");

        // طباعة الشجرة للتأكد من صحتها
        folderReader.PrintFolderTree(root);

        // إنشاء المجلدات على النظام بناءً على الشجرة
        folderReader.CreateFolders(projectPath, root);

        Console.WriteLine("✅ تم إنشاء جميع المجلدات بنجاح!");
    }
}
