using AutoGenerator.ApiFolder;

namespace AutoGenerator.TM
{

    public class TmValidators
    {


        public static string GetTmConditionChecker(string classNameServiceTM, TmOptions options = null)
        {
            return @$"
 public class ConditionChecker :BaseConditionChecker, IConditionChecker
    {{
        private readonly ITFactoryInjector _injector;

        public ITFactoryInjector Injector => _injector;
        public ConditionChecker(ITFactoryInjector injector) : base()
        {{
        }}

        // الدوال السابقة تبقى كما هي

     
    }}

";



        }


        public static string GetTmIConditionChecker(string classNameServiceTM, TmOptions options = null)
        {
            return @$"
    public interface IConditionChecker: IBaseConditionChecker
    {{
   

        public ITFactoryInjector Injector {{ get; }}



    }}

";



        }


        public static string GetTmIValidatorContext(string classNameServiceTM, TmOptions options = null)
        {
            return @$"
      public abstract class ValidatorContext<TContext, EValidator> : BaseValidatorContext<TContext, EValidator>, ITValidator
       where TContext : class
       where EValidator : Enum
       
    {{
        protected readonly ITFactoryInjector _injector;


        public ValidatorContext(IConditionChecker checker) : base(checker)
        {{
            _injector= checker.Injector;
        }}
         
        
        protected virtual async Task<TContext?>  FinModel(string? id)
        {{
            

            var _model = await _injector.Context.Set<TContext>().FindAsync(id);
            return _model;



        }}


        protected override Task<TContext?> GetModel(string? id)
        {{
            
            return FinModel(id);
        }}


         
    }}




";



        }
        public static string GetTmITFactoryInjector(string classNameServiceTM, TmOptions options = null)
        {
            return @$"
     public interface ITFactoryInjector: ITBaseFactoryInjector
    {{

  
    public  {ApiFolderInfo.TypeContext.Name} Context {{ get; }}


    }}
";



        }


        public static string GetTmTFactoryInjector(string classNameServiceTM, TmOptions options = null)
        {
            return @$"
    public class TFactoryInjector : TBaseFactoryInjector, ITFactoryInjector
   {{
    
       private readonly {ApiFolderInfo.TypeContext.Name} _context;

       public TFactoryInjector(IMapper mapper, IAutoNotifier notifier,{ApiFolderInfo.TypeContext.Name} context) : base(mapper, notifier)
       {{
           _context = context;
       }}

       public {ApiFolderInfo.TypeContext.Name} Context => _context;
       // يمكنك حقن اي طبقة

   }}

";



        }


        public static string GetTmConfigValidator(string classNameServiceTM, TmOptions options = null)
        {
            return @$"
     public  static class ConfigValidator
    {{
        public static void AddAutoValidator(this IServiceCollection serviceCollection)
        {{


            Assembly? assembly =Assembly.GetExecutingAssembly();

            serviceCollection.AddScoped<ITFactoryInjector, TFactoryInjector>();
            serviceCollection.AddScoped<IConditionChecker, ConditionChecker>(pro =>
            {{
                var injctor = pro.GetRequiredService<ITFactoryInjector>();

                var checker= new ConditionChecker(injctor);


                BaseConfigValidator.Register(checker, assembly);

                return checker;

            }});




        }}
     
    }}

";



        }

        public static string GetTmValidator(string classNameValidatorTM, TmOptions options = null)
        {
            return @$"
    

    public class {classNameValidatorTM}Validator : BaseValidator<{classNameValidatorTM}ResponseFilterDso, {classNameValidatorTM}ValidatorStates>, ITValidator
    {{

    
        
        public {classNameValidatorTM}Validator(IConditionChecker checker) : base(checker)
        {{

           
        }}
        protected override void InitializeConditions()
        {{
            _provider.Register(
                {classNameValidatorTM}ValidatorStates.IsActive,
                new LambdaCondition<{classNameValidatorTM}ResponseFilterDso>(
                    nameof({classNameValidatorTM}ValidatorStates.IsActive),

                    context => IsActive(context),
                    ""{classNameValidatorTM} is not active""
                )
            );



            
        





        }}



        private bool IsActive({classNameValidatorTM}ResponseFilterDso context)
        {{
            if (context!=null){{
                return true;
            }}
            return false;
        }}

      

    }}

      //
       
     //  Base
     public enum {classNameValidatorTM}ValidatorStates //
    {{
        IsActive,
        IsFull,
        IsValid,
        
    //
    }}

 

";

  

        }



        public static string GetTmValidatorContext(string classNameValidatorTM, TmOptions options = null)
        {
            return @$"
    

    public class {classNameValidatorTM}Validator : BaseValidator<{classNameValidatorTM}ResponseFilterDso, {classNameValidatorTM}ValidatorStates>, ITValidator
    {{

    
        
        public {classNameValidatorTM}Validator(IConditionChecker checker) : base(checker)
        {{

           
        }}
        protected override void InitializeConditions()
        {{
            _provider.Register(
                {classNameValidatorTM}ValidatorStates.IsActive,
                new LambdaCondition<{classNameValidatorTM}ResponseFilterDso>(
                    nameof({classNameValidatorTM}ValidatorStates.IsActive),

                    context => IsActive(context),
                    ""{classNameValidatorTM} is not active""
                )
            );



            
        





        }}



        private bool IsActive({classNameValidatorTM}ResponseFilterDso context)
        {{
            if (context!=null){{
                return true;
            }}
            return false;
        }}

      

    }}

      //
       
     //  Base
     public enum {classNameValidatorTM}ValidatorStates //
    {{
        IsActive,
        IsFull,
        IsValid,
        
    //
    }}

 

";



        }


    }

}