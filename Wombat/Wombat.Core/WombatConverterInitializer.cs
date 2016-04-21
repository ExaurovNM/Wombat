namespace Wombat.Core
{
    internal static class WombatConverterInitializer
    {
        public static void InitilizeBaseConvertors(WombatConverter converter)
        {
            InitializeBaseStringConvertors(converter);
            InitializeBaseIntegerConvertors(converter);
        }

        private static void InitializeBaseStringConvertors(WombatConverter converter)
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

        private static void InitializeBaseIntegerConvertors(WombatConverter converter)
        {
            //// Int -> String
            converter.Register<int, string>(integerValue => integerValue.ToString());
        }
    }
}