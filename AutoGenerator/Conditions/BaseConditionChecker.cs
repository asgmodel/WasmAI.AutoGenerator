namespace AutoGenerator.Conditions
{



    public interface IBaseConditionChecker
    {
        bool Check<TEnum>(TEnum type, object? context=null) where TEnum : Enum;
        Task<bool> CheckAsync<TEnum>(TEnum type, object? context = null) where TEnum : Enum;

        bool CheckAll<TEnum>(object? context = null) where TEnum : Enum;
        Task<bool> CheckAllAsync<TEnum>(object? context = null) where TEnum : Enum;

        bool AreAllConditionsMet<TEnum>(object context, out Dictionary<TEnum, string> failedConditions) where TEnum : Enum;

        void RegisterProvider<TEnum>(IConditionProvider<TEnum> provider) where TEnum : Enum;

        ConditionResult CheckAndResult<TEnum>(TEnum type, object? context = null) where TEnum : Enum;
        Task<ConditionResult> CheckAndResultAsync<TEnum>(TEnum type, object? context = null) where TEnum : Enum;

        // دالة لفحص الشرط مع النتيجة ورسالة الخطأ
        bool CheckWithError<TEnum>(TEnum type, object context, out string errorMessage) where TEnum : Enum;

        // دالة لفحص جميع الشروط مع الرسائل التفصيلية
        bool CheckAllWithDetails<TEnum>(object context, out Dictionary<TEnum, string> results) where TEnum : Enum;

        // دالة لفحص الشرط مع سياقات متعددة
        bool CheckWithMultipleContexts<TEnum>(TEnum type, object[] contexts) where TEnum : Enum;
        Task<bool> CheckWithMultipleContextsAsync<TEnum>(TEnum type, object[] contexts) where TEnum : Enum;

        public Task<bool> CheckWithErrorASync<TEnum>(TEnum type, object context, out string errorMessage) where TEnum : Enum;



        Task<bool> CheckAnyAsync<TEnum>(object? context = null) where TEnum : Enum;
        bool CheckAny<TEnum>(object? context = null) where TEnum : Enum;

        void ResetConditionState<TEnum>(object context) where TEnum : Enum;

        Task<bool> CheckConditionWithTimeoutAsync<TEnum>(TEnum type, object context, TimeSpan timeout) where TEnum : Enum;

        Task<bool> EvaluateConditionWithRetryAsync<TEnum>(TEnum type, object context, int maxRetries, TimeSpan delay) where TEnum : Enum;

        Task<Dictionary<TEnum, string>> GetFailedConditionDetailsAsync<TEnum>(object context) where TEnum : Enum;
        Dictionary<TEnum, string> GetFailedConditionDetails<TEnum>(object context) where TEnum : Enum;

        Task<bool> CheckWithContextualDependenciesAsync<TEnum>(TEnum type, object[] contexts) where TEnum : Enum;

        Task<bool> CheckConditionByCustomEvaluatorAsync<TEnum>(TEnum type, object context, Func<object, Task<bool>> customEvaluator) where TEnum : Enum;

        Task<bool> AreAllConditionsMetWithRetryAsync<TEnum>(object context, int maxRetries, TimeSpan delay) where TEnum : Enum;

        Task<bool> CheckWithContextDataAsync<TEnum>(TEnum type, object context, object additionalData) where TEnum : Enum;

        Task<Dictionary<TEnum, List<ConditionResult>>> GetConditionHistoryAsync<TEnum>(object context) where TEnum : Enum;
        Dictionary<TEnum, List<ConditionResult>> GetConditionHistory<TEnum>(object context) where TEnum : Enum;
        public event EventHandler<ConditionResult> ConditionMet;
        public event EventHandler<ConditionResult> ConditionFailed;


        Task ExecuteConditionWithCallbacksAsync<TEnum>(
              TEnum conditionType,
              object context,
              Func<ConditionResult, Task>? onSuccess = null,
              Func<ConditionResult, Task>? onFailure = null)
              where TEnum : Enum;
        public IConditionProvider<TEnum>? GetProvider<TEnum>() where TEnum : Enum;


      





    }

    public class BaseConditionChecker :  IBaseConditionChecker
    {
        private readonly Dictionary<Type, object> _providers = new();
        
        // الأحداث الخاصة بحالة الشرط
        public event EventHandler<ConditionResult> ConditionMet;
        public event EventHandler<ConditionResult> ConditionFailed;



        public BaseConditionChecker()
        {
           
        }





        // دالة لحث إطلاق الحدث عند تحقق الشرط
        protected virtual void OnConditionMet(ConditionResult result)
        {
            ConditionMet?.Invoke(this, result); // تحقق من وجود مستمع للحدث ثم إطلاقه
        }

        // دالة لحث إطلاق الحدث عند فشل الشرط
        protected virtual void OnConditionFailed(ConditionResult result)
        {
            ConditionFailed?.Invoke(this, result); // تحقق من وجود مستمع للحدث ثم إطلاقه
        }


        // تسجيل مزود الشروط
        public void RegisterProvider<TEnum>(IConditionProvider<TEnum> provider) where TEnum : Enum
        {
            _providers[typeof(TEnum)] = provider;
        }

        // التحقق من حالة شرط معين - متزامن
        public  bool Check<TEnum>(TEnum type, object context) where TEnum : Enum
        {
            var res = CheckAsync<TEnum>(type, context).GetAwaiter().GetResult();
            return res;
        }
         

        public   IConditionProvider<TEnum>? GetProvider<TEnum>() where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                return rawProvider as IConditionProvider<TEnum>;
            }

            return null;
        }

        // التحقق من حالة شرط معين - غير متزامن
        public async Task<bool> CheckAsync<TEnum>(TEnum type, object context) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    var result = await condition.Evaluate(context);
                    return result?.Success ?? false;
                }
            }
            return false;
        }

        // التحقق من جميع الشروط - متزامن
        public bool CheckAll<TEnum>(object context) where TEnum : Enum
        {
           
            var res = CheckAllAsync<TEnum>(context).GetAwaiter().GetResult();
            return res;
        }

        // التحقق من جميع الشروط - غير متزامن
        public async Task<bool> CheckAllAsync<TEnum>(object context) where TEnum : Enum
        {
            var results = new Dictionary<TEnum, bool>();

            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        var result = await condition?.Evaluate(context);
                        results[type] = result?.Success ?? false;
                    }
                }
            }

            return results.All(r => r.Value);
        }

        // التحقق من الشروط وإرجاع النتيجة مع رسالة الخطأ
        public bool CheckWithError<TEnum>(TEnum type, object context, out string errorMessage) where TEnum : Enum
        {

            var result = CheckAndResultAsync(type, context).GetAwaiter().GetResult();
            if (result == null)
            {
                errorMessage = "Condition not found or provider unavailable";
                return false;
            }
            errorMessage = result?.Message ?? "Unknown error";
            
               
            return true;
            

            
        }

        // التحقق من جميع الشروط مع تفاصيل الرسائل
        public bool CheckAllWithDetails<TEnum>(object context, out Dictionary<TEnum, string> results) where TEnum : Enum
        {
            results = new Dictionary<TEnum, string>();

            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        var result = condition?.Evaluate(context).GetAwaiter().GetResult();
                        results[type] = result?.Success == true ? "Success" : result?.Message ?? "Unknown error";
                    }
                }
            }

            return results.All(r => r.Value == "Success");
        }

        // التحقق من الشرط مع سياقات متعددة - متزامن
        public bool CheckWithMultipleContexts<TEnum>(TEnum type, object[] contexts) where TEnum : Enum
        {
            
            var  res= CheckWithMultipleContextsAsync(type, contexts).GetAwaiter().GetResult();
            return res; 
        }

        // التحقق من الشرط مع سياقات متعددة - غير متزامن
        public async Task<bool> CheckWithMultipleContextsAsync<TEnum>(TEnum type, object[] contexts) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    foreach (var context in contexts)
                    {
                        var result = await condition.Evaluate(context);
                        if (result?.Success == false)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

      
       


        public ConditionResult CheckAndResult<TEnum>(TEnum type, object context) where TEnum : Enum
        {
                
            var result = CheckAndResultAsync(type, context).GetAwaiter().GetResult();
            return result;

        }

        public async Task<ConditionResult> CheckAndResultAsync<TEnum>(TEnum type, object context) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    var result = await condition.Evaluate(context);
                    return result ?? new ConditionResult(false, null, "Unknown error");
                }
            }
            return new ConditionResult(false, null, "Condition not found or provider unavailable");
        }

        

        public Task<bool> CheckWithErrorASync<TEnum>(TEnum type, object context, out string errorMessage) where TEnum : Enum
        {

            var result = CheckAndResultAsync(type, context).GetAwaiter().GetResult();
            if (result == null)
            {
                errorMessage = "Condition not found or provider unavailable";
                return Task.FromResult(false);
            }
            errorMessage = result?.Message ?? "Unknown error";
            return Task.FromResult(true);
        }

        public bool AreAllConditionsMet<TEnum>(object context, out Dictionary<TEnum, string> failedConditions) where TEnum : Enum
        {
            failedConditions = new Dictionary<TEnum, string>();

            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        var result = condition?.Evaluate(context).GetAwaiter().GetResult();
                        if (result?.Success == false)
                        {
                            failedConditions[type] = result?.Message ?? "Unknown error";
                        }
                    }
                }
            }

            return failedConditions.Count == 0;
        }




        // التحقق من شرط واحد - غير متزامن
        public async Task<bool> CheckAnyAsync<TEnum>(object context) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        var result = await condition?.Evaluate(context);
                        if (result?.Success == true)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // التحقق من شرط واحد - متزامن
        public bool CheckAny<TEnum>(object context) where TEnum : Enum
        {
            var res = CheckAnyAsync<TEnum>(context).GetAwaiter().GetResult();
            return res;
        }

       

        // التحقق من الشرط مع المهلة الزمنية
        public async Task<bool> CheckConditionWithTimeoutAsync<TEnum>(TEnum type, object context, TimeSpan timeout) where TEnum : Enum
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(timeout);

            try
            {
                return await CheckAsync<TEnum>(type, context);
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }

        // التحقق من الشرط مع إعادة المحاولة - غير متزامن
        public async Task<bool> EvaluateConditionWithRetryAsync<TEnum>(TEnum type, object context, int maxRetries, TimeSpan delay) where TEnum : Enum
        {
            var retries = 0;
            while (retries < maxRetries)
            {
                var result = await CheckAsync<TEnum>(type, context);
                if (result)
                {
                    return true;
                }
                retries++;
                await Task.Delay(delay);
            }
            return false;
        }

        // الحصول على تفاصيل الشروط الفاشلة - غير متزامن
        public async Task<Dictionary<TEnum, string>> GetFailedConditionDetailsAsync<TEnum>(object context) where TEnum : Enum
        {
            var failedConditions = new Dictionary<TEnum, string>();

            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        var result = await condition?.Evaluate(context);
                        if (result?.Success == false)
                        {
                            failedConditions[type] = result?.Message ?? "Unknown error";
                        }
                    }
                }
            }

            return failedConditions;
        }

        // الحصول على تفاصيل الشروط الفاشلة - متزامن
        public Dictionary<TEnum, string> GetFailedConditionDetails<TEnum>(object context) where TEnum : Enum
        {
            var res = GetFailedConditionDetailsAsync<TEnum>(context).GetAwaiter().GetResult();
            return res;
        }

        // التحقق من الشرط مع سياقات متعددة - غير متزامن
        public async Task<bool> CheckWithContextualDependenciesAsync<TEnum>(TEnum type, object[] contexts) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    foreach (var context in contexts)
                    {
                        var result = await condition.Evaluate(context);
                        if (result?.Success == false)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        // التحقق من الشرط باستخدام مقيّم مخصص - غير متزامن
        public async Task<bool> CheckConditionByCustomEvaluatorAsync<TEnum>(TEnum type, object context, Func<object, Task<bool>> customEvaluator) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    var result = await customEvaluator(context);
                    return result;
                }
            }
            return false;
        }

   


       

        // التحقق من الشرط مع بيانات إضافية - غير متزامن
        public async Task<bool> CheckWithContextDataAsync<TEnum>(TEnum type, object context, object additionalData) where TEnum : Enum
        {
            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                var condition = provider?.Get(type);
                if (condition != null)
                {
                    var result = await condition.Evaluate(context);
                    // يمكن استخدام `additionalData` هنا حسب الحاجة
                    return result?.Success ?? false;
                }
            }
            return false;
        }

        // الحصول على تاريخ الشروط - غير متزامن
        public async Task<Dictionary<TEnum, List<ConditionResult>>> GetConditionHistoryAsync<TEnum>(object context) where TEnum : Enum
        {
            var history = new Dictionary<TEnum, List<ConditionResult>>();

            if (_providers.TryGetValue(typeof(TEnum), out var rawProvider))
            {
                var provider = rawProvider as IConditionProvider<TEnum>;
                if (provider != null)
                {
                    foreach (TEnum type in Enum.GetValues(typeof(TEnum)))
                    {
                        var condition = provider.Get(type);
                        var result = await condition?.Evaluate(context);
                        if (result != null)
                        {
                            if (!history.ContainsKey(type))
                            {
                                history[type] = new List<ConditionResult>();
                            }
                            history[type].Add(result);
                        }
                    }
                }
            }

            return history;
        }

        // الحصول على تاريخ الشروط - متزامن
        public Dictionary<TEnum, List<ConditionResult>> GetConditionHistory<TEnum>(object context) where TEnum : Enum
        {
            var res = GetConditionHistoryAsync<TEnum>(context).GetAwaiter().GetResult();
            return res;
        }

        public void ResetConditionState<TEnum>(object context) where TEnum : Enum
        {
            throw new NotImplementedException();
        }

        public Task<bool> AreAllConditionsMetWithRetryAsync<TEnum>(object context, int maxRetries, TimeSpan delay) where TEnum : Enum
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteConditionWithCallbacksAsync<TEnum>(TEnum conditionType, object context, Func<ConditionResult, Task>? onSuccess = null, Func<ConditionResult, Task>? onFailure = null) where TEnum : Enum
        {
            
             var result =await CheckAndResultAsync(conditionType, context);
            if (result.Success==true)
            {
                OnConditionMet(result);
                if (onSuccess != null)
                {
                    await onSuccess(result);
                }
            }
            else
            {
                OnConditionFailed(result);
                if (onFailure != null)
                {
                    await onFailure(result);
                }
            }

        }
    }


}





