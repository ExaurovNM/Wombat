using System;
using System.Collections.Generic;

namespace Wombat.Core
{
    public class WombatConverter
    {
        private readonly Dictionary<Type, Dictionary<Type, Func<object, object>>> converters;

        public WombatConverter()
        {
            this.converters = new Dictionary<Type, Dictionary<Type, Func<object, object>>>();

            this.Register<int, string>(integerValue => integerValue.ToString());
        }

        public bool IsSupported(Type sourceType, Type resultType)
        {
            return 
                this.converters.ContainsKey(sourceType) 
                && this.converters[sourceType].ContainsKey(resultType);
        }

        public TResult Convert<TSource, TResult>(TSource value)
        {
            var typeTResult = typeof(TResult);
            var typeTSource = typeof(TSource);

            if (!this.IsSupported(typeTSource, typeTResult))
            {
                throw new ArgumentException($"Pair {typeTSource.FullName} to {typeTResult.FullName} is not supported");
            }

            return (TResult)this.converters[typeTSource][typeTResult](value);
        }

        private void TryCreateSourceNode(Type typeTSource)
        {
            if (!this.converters.ContainsKey(typeTSource))
            {
                this.converters[typeTSource] = new Dictionary<Type, Func<object, object>>();
            }
        }

        public void Register<TSource, TResult>(Func<TSource, TResult> converter)
        {
            var typeTResult = typeof(TResult);
            var typeTSource = typeof(TSource);

            if (this.IsSupported(typeof(TResult), typeof(TSource)))
            {
                throw new ArgumentException($"Pair {typeTSource.FullName} to {typeTResult.FullName} is alredy registered");
            }

            this.TryCreateSourceNode(typeTSource);

            this.converters[typeTSource][typeTResult] = this.ConvertFunctionToBase(converter);
        }

        public void Ovveride<TSource, TResult>(Func<TSource, TResult> converter)
        {
            var typeTResult = typeof(TResult);
            var typeTSource = typeof(TSource);

            if (!this.IsSupported(typeTSource, typeTResult))
            {
                throw new ArgumentException($"Pair {typeTSource.FullName} to {typeTResult.FullName} is not supported");
            }

            this.TryCreateSourceNode(typeTSource);

            this.converters[typeTResult][typeTResult] = this.ConvertFunctionToBase(converter);
        }

        private Func<object, object> ConvertFunctionToBase<TSource, TResult>(Func<TSource, TResult> converter)
        {
            return value => converter((TSource)value);
        }
    }
}
