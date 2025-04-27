using AutoGenerator.ApiFolder;

namespace AutoGenerator.TM
{

    public  class TmBaseRepository
    {

        public static string GetTmBaseRepository(string nameShareTM, TmOptions options = null)
        {
            return @$"
 /// <summary>
 /// {nameShareTM} class for ShareRepository.
 /// </summary>
        public sealed class BaseRepository<T> : TBaseRepository<ApplicationUser, IdentityRole, string, T>, IBaseRepository<T> where T : class
    {{
        public BaseRepository({ApiFolderInfo.TypeContext.Name} db, ILogger logger) : base(db, logger)
        {{
        }}
    }}

";
        }


        public static string GetTmBaseBuilderRepository(string nameShareTM, TmOptions options = null)
        {
            return @$"
 /// <summary>
 /// {nameShareTM} class for ShareRepository.
 /// </summary>
  public abstract class BaseBuilderRepository<TModel, TBuildRequestDto, TBuildResponseDto> : TBaseBuilderRepository<TModel, TBuildRequestDto, TBuildResponseDto>, IBaseBuilderRepository<TBuildRequestDto, TBuildResponseDto>, ITBuildRepository
    where TModel : class
    where TBuildRequestDto : class
    where TBuildResponseDto : class

 {{
     public BaseBuilderRepository({ApiFolderInfo.TypeContext.Name} context, IMapper mapper, ILogger logger) : base(new BaseRepository<TModel>(context, logger), mapper, logger)
     {{
     }}





 }}

";
        }

    }
}