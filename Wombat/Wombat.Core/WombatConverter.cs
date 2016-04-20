using System;
using System.Collections.Generic;

namespace Wombat.Core
{
    public class WombatConverter : IWombatConverter
    {
        private readonly Dictionary<Type, Dictionary<Type, Func<object, object>>> converters;

        public WombatConverter()
        {
            this.converters = new Dictionary<Type, Dictionary<Type, Func<object, object>>>();

            this.InitilizeBaseConvertors();
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

        public void Register<TSource, TResult>(Func<TSource, TResult> converter)
        {
            var typeTResult = typeof(TResult);
            var typeTSource = typeof(TSource);

            if (this.IsSupported(typeof(TResult), typeof(TSource)))
            {
                throw new ArgumentException($"Pair {typeTSource.FullName} to {typeTResult.FullName} is already registered");
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

        private void TryCreateSourceNode(Type typeTSource)
        {
            if (!this.converters.ContainsKey(typeTSource))
            {
                this.converters[typeTSource] = new Dictionary<Type, Func<object, object>>();
            }
        }

        private void InitilizeBaseConvertors()
        {
            this.InitializeBaseStringConvertors();
            this.InitializeBaseIntegerConvertors();
        }

        private void InitializeBaseStringConvertors()
        {
            //// String -> Int
            this.Register<string, int>(int.Parse);

            //// String -> Long
            this.Register<string, long>(long.Parse);

            //// String -> Float
            this.Register<string, float>(float.Parse);

            //// String -> Double
            this.Register<string, double>(double.Parse);

            //// String -> Decimal
            this.Register<string, decimal>(decimal.Parse);
        }

        private void InitializeBaseIntegerConvertors()
        {
            //// Int -> String
            this.Register<int, string>(integerValue => integerValue.ToString());
        }
    }
}
