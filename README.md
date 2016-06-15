# Wombat - simle C# converter
Wombat is a small library providing simple interface to create and to use custom object's converters in your application.

## Build status
[![Build Status](https://travis-ci.org/ExaurovNM/Wombat.svg?branch=master)](https://travis-ci.org/ExaurovNM/Wombat)

## Installation
You can install this library using NuGet into your project. Currently Wombat works with .NET 4.0 and higher.

    Install-Package WombatConverter

After the installing nuget package is complete, you should add `using Wombat.Core;` to your source code and you are ready to use it.
	
    using Wombat.Core;

## How to use
You can use it with default initialization:
```c#
static void Main(string[] args)
{
    IWombatConverter converter = WombatConverter.CreateInitialized();

    int number = converter.Convert<string, int>("923");
    string numbetToString = converter.Convert(number, string.Empty);
}
```

And you can create your own convertation rules for any types:

```c#
using Wombat.Core;

class Program
{
	static void Main(string[] args)
	{
            IWombatConverter converter = WombatConverter.CreateEmpty();

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
        }

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
}
```

## Questions, comments or additions?

If you have a feature request or bug report, leave an issue on the [issues page](https://github.com/ExaurovNM/Wombat/issues) or send a [pull request](https://github.com/ExaurovNM/Wombat/pulls).
