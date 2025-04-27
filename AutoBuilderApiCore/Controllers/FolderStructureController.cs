using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IO;

namespace FolderStructureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderStructureController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFolderStructure([FromQuery] string path)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return BadRequest("Invalid or missing path.");
            }

            var folderStructure = GetStructure(path);
            return Ok(folderStructure);
        }
        private object GetStructure(string path)
        {
            var structure = new Dictionary<string, object>();

            try
            {
                // √Ê·«: «·„·›«  ›Ì Â–« «·„Ã·œ
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    structure[fileName] = null;
                }

                // À«‰Ì«: «·„Ã·œ«  «·›—⁄Ì…
                var directories = Directory.GetDirectories(path);
                foreach (var dir in directories)
                {
                    var dirName = Path.GetFileName(dir);
                    structure[dirName] = GetStructure(dir);
                }
            }
            catch
            {
                //  ﬁœ—   Ã«Â· «·„Ã·œ«  «··Ì „« ⁄‰œﬂ ’·«ÕÌ…  œŒ·Â«
            }

            return structure;
        }

        static Dictionary<string, List<string>> GetDirectoryFileList(string rootPath)
        {
            var result = new Dictionary<string, List<string>>();

            try
            {
                foreach (var dir in Directory.GetDirectories(rootPath))
                {
                    var dirName = Path.GetFileName(dir);
                    var files = new List<string>();

                    foreach (var file in Directory.GetFiles(dir))
                    {
                        files.Add(Path.GetFileName(file));
                    }

                    result[dirName] = files;
                }
            }
            catch (UnauthorizedAccessException)
            {
                //  ŒÿÌ «·„Ã·œ«  «· Ì ·« Ì„ﬂ‰ «·Ê’Ê· ·Â«
            }

            return result;
        }


        private JObject GetFolderStructureAsJson(string path)
        {
            JObject result = new JObject();

            try
            {
                foreach (string entry in Directory.GetFileSystemEntries(path))
                {
                    string name = Path.GetFileName(entry);

                    if (Directory.Exists(entry))
                    {
                        result[name] = GetFolderStructureAsJson(entry);
                    }
                    else
                    {
                        result[name] = new JArray();  //  „ÀÌ· «·„·› »„’›Ê›… ›«—€…
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                return new JObject(); // ›Ì Õ«· ﬂ«‰ «·„Ã·œ €Ì— ﬁ«»· ··Ê’Ê·
            }

            return result;
        }
    }
}
