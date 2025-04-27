using AutoGenerator.ApiFolder;
using System.Text;

namespace AutoGenerator.TM
{

    public class TmController
    {


        public static string GetTemplateController(List<string> usings, string namespaceName, string className)
        {
            // Initialize StringBuilder to accumulate the using statements dynamically.
            StringBuilder usingStatements = new StringBuilder();
            var root = ApiFolderInfo.ROOT.Name;
            // Add using statements to the StringBuilder if provided.
            if (usings != null)
            {
                foreach (var u in usings)
                {
                    usingStatements.AppendLine($"using {u};");
                }
            }
            var nameObj = className.ToLower();

            // Generate and return the controller template by replacing the namespace and className variables.
            return $@"
{usingStatements.ToString()}
    //[ApiExplorerSettings(GroupName = ""{root}"")]
    [Route(""api/{root}/{namespaceName}/[controller]"")]
    [ApiController]
    public class {className}Controller : ControllerBase
    {{
        private readonly IUse{className}Service _{nameObj}Service;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public {className}Controller(IUse{className}Service {nameObj}Service, IMapper mapper, ILoggerFactory logger)
        {{
            _{nameObj}Service = {nameObj}Service;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof({className}Controller).FullName);
        }}

        // Get all {className}s.
        [HttpGet(Name = ""Get{className}s"")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<{className}OutputVM>>> GetAll()
        {{
            try
            {{
                _logger.LogInformation(""Fetching all {className}s..."");
                var result = await _{nameObj}Service.GetAllAsync();
                var items = _mapper.Map<List<{className}OutputVM>>(result);
                return Ok(items);
            }}
            catch (Exception ex)
            {{
                _logger.LogError(ex, ""Error while fetching all {className}s"");
                return StatusCode(500, ""Internal Server Error"");
            }}
        }}

        // Get a {className} by ID.
        [HttpGet(""{{id}}"", Name = ""Get{className}"")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<{className}InfoVM>> GetById(string? id)
        {{
            if (string.IsNullOrWhiteSpace(id))
            {{
                _logger.LogWarning(""Invalid {className} ID received."");
                return BadRequest(""Invalid {className} ID."");
            }}

            try
            {{
                _logger.LogInformation(""Fetching {className} with ID: {{id}}"", id);
                var entity = await _{nameObj}Service.GetByIdAsync(id);
                if (entity == null)
                {{
                    _logger.LogWarning(""{className} not found with ID: {{id}}"", id);
                    return NotFound();
                }}
                var item = _mapper.Map<{className}InfoVM>(entity);
                return Ok(item);
            }}
            catch (Exception ex)
            {{
                _logger.LogError(ex, ""Error while fetching {className} with ID: {{id}}"", id);
                return StatusCode(500, ""Internal Server Error"");
            }}
        }}
        // // Get a {className} by Lg.
        [HttpGet(""Get{className}ByLanguage"",Name = ""Get{className}ByLg"")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<{className}OutputVM>> Get{className}ByLg({className}FilterVM model)
        {{
             var id=model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {{
                _logger.LogWarning(""Invalid {className} ID received."");
                return BadRequest(""Invalid {className} ID."");
            }}

            try
            {{
                _logger.LogInformation(""Fetching {className} with ID: {{id}}"", id);
                var entity = await _{nameObj}Service.GetByIdAsync(id);
                if (entity == null)
                {{
                    _logger.LogWarning(""{className} not found with ID: {{id}}"", id);
                    return NotFound();
                }}
                var item = _mapper.Map<{className}OutputVM>(entity,opt=>opt.Items.Add(HelperTranslation.KEYLG,model.Lg));
                return Ok(item);
            }}
            catch (Exception ex)
            {{
                _logger.LogError(ex, ""Error while fetching {className} with ID: {{id}}"", id);
                return StatusCode(500, ""Internal Server Error"");
            }}
        }}

        
         // // Get a {className}s by Lg.
        [HttpGet(""Get{className}sByLanguage"",Name = ""Get{className}sByLg"")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<{className}OutputVM>>> Get{className}sByLg(string? lg)
        {{
            
            if (string.IsNullOrWhiteSpace(lg))
            {{
                _logger.LogWarning(""Invalid {className} lg received."");
                return BadRequest(""Invalid {className} lg null "");
            }}

            try
            {{
               
                var {nameObj}s = await _{nameObj}Service.GetAllAsync();
                if ({nameObj}s == null)
                {{
                    _logger.LogWarning(""{className}s not found  by  "");
                    return NotFound();
                }}
                var items = _mapper.Map<IEnumerable<{className}OutputVM>>({nameObj}s,opt=>opt.Items.Add(HelperTranslation.KEYLG,lg));
                return Ok(items);
            }}
            catch (Exception ex)
            {{
                _logger.LogError(ex, ""Error while fetching {className}s with Lg: {{lg}}"", lg);
                return StatusCode(500, ""Internal Server Error"");
            }}
        }}


          
        // Create a new {className}.
        [HttpPost(Name = ""Create{className}"")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<{className}OutputVM>> Create([FromBody] {className}CreateVM model)
        {{
            if (model == null)
            {{
                _logger.LogWarning(""{className} data is null in Create."");
                return BadRequest(""{className} data is required."");
            }}
            if (!ModelState.IsValid)
            {{
                _logger.LogWarning(""Invalid model state in Create: {{ModelState}}"", ModelState);
                return BadRequest(ModelState);
            }}

            try
            {{
                _logger.LogInformation(""Creating new {className} with data: {{@model}}"", model);
                var item = _mapper.Map<{className}RequestDso>(model);
                var createdEntity = await _{nameObj}Service.CreateAsync(item);
                var createdItem = _mapper.Map<{className}OutputVM>(createdEntity);
                return Ok(createdItem);
            }}
            catch (Exception ex)
            {{
                _logger.LogError(ex, ""Error while creating a new {className}"");
                return StatusCode(500, ""Internal Server Error"");
            }}
        }}

        // Create multiple {className}s.
        [HttpPost(""createRange"")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<{className}OutputVM>>> CreateRange([FromBody] IEnumerable<{className}CreateVM> models)
        {{
            if (models == null)
            {{
                _logger.LogWarning(""Data is null in CreateRange."");
                return BadRequest(""Data is required."");
            }}
            if (!ModelState.IsValid)
            {{
                _logger.LogWarning(""Invalid model state in CreateRange: {{ModelState}}"", ModelState);
                return BadRequest(ModelState);
            }}

            try
            {{
                _logger.LogInformation(""Creating multiple {className}s."");
                var items = _mapper.Map<List<{className}RequestDso>>(models);
                var createdEntities = await _{nameObj}Service.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<{className}OutputVM>>(createdEntities);
                return Ok(createdItems);
            }}
            catch (Exception ex)
            {{
                _logger.LogError(ex, ""Error while creating multiple {className}s"");
                return StatusCode(500, ""Internal Server Error"");
            }}
        }}

        // Update an existing {className}.
        [HttpPut(Name = ""Update{className}"")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<{className}OutputVM>> Update([FromBody] {className}UpdateVM model)
        {{
            if (model == null)
            {{
                _logger.LogWarning(""Invalid data in Update."");
                return BadRequest(""Invalid data."");
            }}
            if (!ModelState.IsValid)
            {{
                _logger.LogWarning(""Invalid model state in Update: {{ModelState}}"", ModelState);
                return BadRequest(ModelState);
            }}

            try
            {{
                _logger.LogInformation(""Updating {className} with ID: {{id}}"", model?.Id);
                var item = _mapper.Map<{className}RequestDso>(model);
                var updatedEntity = await _{nameObj}Service.UpdateAsync(item);
                if (updatedEntity == null)
                {{
                    _logger.LogWarning(""{className} not found for update with ID: {{id}}"", model?.Id);
                    return NotFound();
                }}
                var updatedItem = _mapper.Map<{className}OutputVM>(updatedEntity);
                return Ok(updatedItem);
            }}
            catch (Exception ex)
            {{
                _logger.LogError(ex, ""Error while updating {className} with ID: {{id}}"", model?.Id);
                return StatusCode(500, ""Internal Server Error"");
            }}
        }}

        // Delete a {className}.
        [HttpDelete(""{{id}}"", Name = ""Delete{className}"")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {{
            if (string.IsNullOrWhiteSpace(id))
            {{
                _logger.LogWarning(""Invalid {className} ID received in Delete."");
                return BadRequest(""Invalid {className} ID."");
            }}

            try
            {{
                _logger.LogInformation(""Deleting {className} with ID: {{id}}"", id);
                await _{nameObj}Service.DeleteAsync(id);
                return NoContent();
            }}
            catch (Exception ex)
            {{
                _logger.LogError(ex, ""Error while deleting {className} with ID: {{id}}"", id);
                return StatusCode(500, ""Internal Server Error"");
            }}
        }}

        // Get count of {className}s.
        [HttpGet(""Count{className}"")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {{
            try
            {{
                _logger.LogInformation(""Counting {className}s..."");
                var count = await _{nameObj}Service.CountAsync();
                return Ok(count);
            }}
            catch (Exception ex)
            {{
                _logger.LogError(ex, ""Error while counting {className}s"");
                return StatusCode(500, ""Internal Server Error"");
            }}
        }}
    }}
";
        }




    }
}