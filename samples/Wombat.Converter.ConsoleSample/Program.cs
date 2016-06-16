namespace Wombat.Converter.ConsoleSample
{
    using System;

    using Wombat.Core;

    class Program
    {
        public class FirstClass
        {
            public int Age { get; set; }

            public string Name { get; set; }
        }

        public class SecondClass
        {
            public string NameAndAge { get; set; }

            public int Age { get; set; }
        }

        static void Main(string[] args)
        {
            var converter = WombatConverter.CreateInitialized();

            int number = converter.Convert<string, int>("923");
            string numbetToString = converter.Convert(number, string.Empty);

            Console.WriteLine(number);
            Console.WriteLine(numbetToString);


            converter = WombatConverter.CreateEmpty();

            converter.Register<FirstClass, SecondClass>(
                firstClass =>
                {
                    return new SecondClass
                    {
                        Age = firstClass.Age,
                        NameAndAge = string.Format("{0} are {1} years old",
                                                    firstClass.Name, firstClass.Age)
                    };
                });

            SecondClass secondClass =
                converter.Convert<FirstClass, SecondClass>(new FirstClass { Age = 11, Name = "John" });

            Console.WriteLine("Age: " + secondClass.Age);
            Console.WriteLine("NameAndAge: " + secondClass.NameAndAge);
            Console.ReadKey();
        }
    }
}