using System;

namespace Wombat.Core
{
    public interface IWombatConverter
    {
        TResult Convert<TSource, TResult>(TSource value);

        bool IsSupported(Type sourceType, Type resultType);

        void Ovveride<TSource, TResult>(Func<TSource, TResult> converter);

        void Register<TSource, TResult>(Func<TSource, TResult> converter);
    }
}