namespace AutoGenerator.Conditions
{

    public interface IConditionProvider<TEnum> where TEnum : Enum
    {
        void Register(TEnum type, ICondition condition);
        ICondition? Get(TEnum type);

        IEnumerable<ICondition> GetConditions(object context);

        IEnumerable<TEnum> GetConditionTypes();
        IEnumerable<ICondition> GetAllConditions();
        IEnumerable<TEnum> GetConditionTypes(ICondition condition);
        IEnumerable<ICondition> GetConditions(TEnum type);
        IEnumerable<ICondition> GetConditions(TEnum type, object context);
        IEnumerable<ICondition> GetConditions(TEnum type, object context, Func<ICondition, bool> predicate);
        IEnumerable<ICondition> GetConditions(TEnum type, object context, Func<ICondition, bool> predicate, bool includeInactive);

        IEnumerable<ICondition> Where(Func<ICondition, bool> predicate);

        IEnumerable<ConditionResult>  AnyPass(object context);

        IEnumerable<ConditionResult> AnyPass(object[] contexts);


        Task<ConditionResult> Check(TEnum type, object context) ;


        




    }

    public class ConditionProvider<TEnum> : IConditionProvider<TEnum> where TEnum : Enum
    {
        private readonly Dictionary<TEnum, List<ICondition>> _conditions = new();

        public void Register(TEnum type, ICondition condition)
        {
            if (!_conditions.ContainsKey(type))
                _conditions[type] = new List<ICondition>();

            _conditions[type].Add(condition);
        }


        public async Task<ConditionResult> Check(TEnum type, object context)
        {
            if (_conditions.TryGetValue(type, out var list) && list.Count > 0)
            {
                foreach (var condition in list)
                {
                    var result = await condition.Evaluate(context);
                    if (result.Success == true)
                    {
                        return result;
                    }
                }
            }
            return new ConditionResult(false, null, $"No {type} conditions passed");
        }

        public IEnumerable<ConditionResult> AnyPass(object context)
        {
            foreach (var kvp in _conditions)
            {
                foreach (var condition in kvp.Value)
                {
                    var result = condition.Evaluate(context).Result;
                    if (result.Success == true)
                    {
                        yield return result;
                    }
                }
            }
        }
        public IEnumerable<ConditionResult> AnyPass(object[] contexts)
        {
            foreach (var context in contexts)
            {
                foreach (var kvp in _conditions)
                {
                    foreach (var condition in kvp.Value)
                    {
                        var result = condition.Evaluate(context).Result;
                        if (result.Success == true)
                        {
                            yield return result;
                        }
                    }
                }
            }
        }
        public ICondition? Get(TEnum type)
        {
            if (_conditions.TryGetValue(type, out var list) && list.Count > 0)
                return list[0];

            return null;
        }

        public IEnumerable<ICondition> GetConditions(object context)
        {
            foreach (var kvp in _conditions)
            {
                foreach (var condition in kvp.Value)
                {
                    yield return condition;
                }
            }
        }

        public IEnumerable<ICondition> Where(Func<ICondition, bool> predicate)
        {
            return _conditions.Values.SelectMany(x => x).Where(predicate);
        }
        public IEnumerable<TEnum> GetConditionTypes()
        {
            return _conditions.Keys;
        }

        public IEnumerable<ICondition> GetAllConditions()
        {
            return _conditions.Values.SelectMany(x => x);
        }

        public IEnumerable<TEnum> GetConditionTypes(ICondition condition)
        {
            return _conditions
                .Where(kvp => kvp.Value.Contains(condition))
                .Select(kvp => kvp.Key);
        }

        public IEnumerable<ICondition> GetConditions(TEnum type)
        {
            return _conditions.TryGetValue(type, out var list)
                ? list
                : Enumerable.Empty<ICondition>();
        }

        public IEnumerable<ICondition> GetConditions(TEnum type, object context)
        {
            // بإمكانك تعديل هذا لاحقاً للتصفية حسب السياق
            return GetConditions(type);
        }

        public IEnumerable<ICondition> GetConditions(TEnum type, object context, Func<ICondition, bool> predicate)
        {
            return GetConditions(type, context).Where(predicate);
        }

        public IEnumerable<ICondition> GetConditions(TEnum type, object context, Func<ICondition, bool> predicate, bool includeInactive)
        {
            var filtered = GetConditions(type, context).Where(predicate);

            if (!includeInactive)
            {
                // ممكن تضيف فلتر خاص مثل IsActive لو متوفر
                filtered = filtered.Where(c => true); // مؤقتاً الكل
            }

            return filtered;
        }
    }


}
