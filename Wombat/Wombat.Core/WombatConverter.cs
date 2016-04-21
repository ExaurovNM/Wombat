namespace Wombat.Core
{
    using System;
    using System.Collections.Generic;

    public class WombatConverter : IWombatConverter
    {
        private readonly Dictionary<Type, Dictionary<Type, Func<object, object>>> converters;

        private WombatConverter(Dictionary<Type, Dictionary<Type, Func<object, object>>> converters)
        {
            this.converters = converters;
        }

        public static WombatConverter CreateInitialized()
        {
            var converter = CreateEmpty();
            WombatConverterInitializer.InitilizeBaseConvertors(converter);

            return converter;
        }

        public static WombatConverter CreateEmpty()
        {
            return new WombatConverter(new Dictionary<Type, Dictionary<Type, Func<object, object>>>());
        }

        public TResult Convert<TSource, TResult>(TSource value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var typeTResult = typeof(TResult);
            var typeTSource = typeof(TSource);

            if (!this.IsSupported(typeTSource, typeTResult))
            {
                throw new ArgumentException(string.Format("Pair {0} to {1} is not supported", typeTSource.FullName, typeTResult.FullName));
            }

            return (TResult)this.converters[typeTSource][typeTResult](value);
        }

        public TResult Convert<TSource, TResult>(TSource value, TResult defaultValue)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var typeTResult = typeof(TResult);
            var typeTSource = typeof(TSource);

            if (!this.IsSupported(typeTSource, typeTResult))
            {
                throw new ArgumentException(string.Format("Pair {0} to {1} is not supported", typeTSource.FullName, typeTResult.FullName));
            }

            try
            {
                return (TResult)this.converters[typeTSource][typeTResult](value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public bool IsSupported(Type sourceType, Type resultType)
        {
            return this.converters.ContainsKey(sourceType)
                && this.converters[sourceType].ContainsKey(resultType);
        }

        public void Register<TSource, TResult>(Func<TSource, TResult> converter)
        {
            var typeTResult = typeof(TResult);
            var typeTSource = typeof(TSource);

            if (this.IsSupported(typeTSource, typeTResult))
            {
                throw new ArgumentException(string.Format("Pair {0} to {1} is already registered", typeTSource.FullName, typeTResult.FullName));
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
                throw new ArgumentException(string.Format("Pair {0} to {1} is not supported", typeTSource.FullName, typeTResult.FullName));
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
    }
}
