namespace AutoBuilderApiCore
{

    public class WeatherForecast
    {

        public static void CombineCSFiles(string folderPath, string outputFilePath)
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("The specified folder does not exist.");
                return;
            }

            string[] csFiles = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);

            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                foreach (string file in csFiles)
                {
                    writer.WriteLine($"// ===== File: {Path.GetFileName(file)} =====");
                    string content = File.ReadAllText(file);
                    writer.WriteLine(content);
                    writer.WriteLine(); // line break between files
                }
            }

            Console.WriteLine($"Combined {csFiles.Length} files into: {outputFilePath}");
        }
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
