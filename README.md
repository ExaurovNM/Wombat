# Wombat - simle C# converter
Wombat is a small library providing simple interface to create and to use custom object's converters in your application.

## Build status
[![Build Status](https://travis-ci.org/ExaurovNM/Wombat.svg?branch=master)](https://travis-ci.org/ExaurovNM/Wombat)

## Installation
You can install this library using NuGet into your project. Currently Wombat works with .NET 4.0 and higher.

    Install-Package Wombat.Core

After the installing nuget package is complete, you should add `using Wombat.Core;` to your source code and you are ready to use it.
	
    using Wombat.Core;

## How to use
```c#
[Test]
        public void Convert_ShouldConvertIntToString()
        {
            var expectedResult = "5";

            var actualResult = this.target.Convert<int, string>(5);

            Assert.AreEqual(expectedResult, actualResult);
        }
```

## Questions, comments or additions?

If you have a feature request or bug report, leave an issue on the [issues page](https://github.com/ExaurovNM/Wombat/issues) or send a [pull request](https://github.com/ExaurovNM/Wombat/pulls).
