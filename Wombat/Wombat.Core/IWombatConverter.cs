namespace Wombat.Core
{
    using System;

    public interface IWombatConverter
    {
        TResult Convert<TSource, TResult>(TSource value);

        TResult Convert<TSource, TResult>(TSource value, TResult defaultValue);

        bool IsSupported(Type sourceType, Type resultType);

        void Ovveride<TSource, TResult>(Func<TSource, TResult> converter);

        void Register<TSource, TResult>(Func<TSource, TResult> converter);
    }
}