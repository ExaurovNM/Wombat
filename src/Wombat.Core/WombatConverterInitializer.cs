namespace Wombat.Core
{
    internal static class WombatConverterInitializer
    {
        public static void InitilizeBaseConvertors(IWombatConverter converter)
        {
            InitializeBaseStringConvertors(converter);
            InitializeBaseIntegerConvertors(converter);
        }

        private static void InitializeBaseStringConvertors(IWombatConverter converter)
        {
            //// String -> Int
            converter.Register<string, int>(int.Parse);

            //// String -> Long
            converter.Register<string, long>(long.Parse);

            //// String -> Float
            converter.Register<string, float>(float.Parse);

            //// String -> Double
            converter.Register<string, double>(double.Parse);

            //// String -> Decimal
            converter.Register<string, decimal>(decimal.Parse);
        }

        private static void InitializeBaseIntegerConvertors(IWombatConverter converter)
        {
            //// Int -> String
            converter.Register<int, string>(integerValue => integerValue.ToString());
        }
    }
}