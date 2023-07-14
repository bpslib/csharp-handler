# BPS Handler

BPS is a key-value data storing structure. This handler provides a BPS data structure for C#.


## Guides and Documentation

The documentation of BPS and this handler can be found [here](https://bps-lib.github.io/). It contains all guides and detailed documentation.


## BPS Handler Operations

### BPS Class

#### Parsing operations

The `BPS` class has two methods to transform data. The method `Parse()` will parse a string containing data in BPS notation. The method `Plain()` will parse a `Dictionary<string, object>` in a string containing the data in BPS notation.

```csharp
public void Foo()
{
    string bpsNotationData = "bar:255;";

    // Parsing a string in a Dictionary<string, object>
    Dictionary<string, object> file = BPS.Parse(bpsNotationData);
    
    // Writing in the console a string representation of a Dictionary<string, object>
    Console.WriteLine(BPS.Plain(file));
}
```
