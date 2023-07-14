# BPS Handler

BPS is a key-value data storing structure. This handler provides a BPS data structure for C#.


## Guides and Documentation

The documentation of BPS and this handler can be found [here](https://bps-lib.github.io/). It contains all guides and detailed documentation.


## BPS Handler Operations

### BPS Class

#### Parsing operations

The `BPS` class has two methods to transform data. The method `Parse()` will parse a string containing data in BPS notation. The method `Plain()` will parse a `BPSFile` in a string containing the data in BPS notation.

```csharp
public void Foo()
{
    string bpsNotationData = "intValue:256;";

    // Parsing a string in a BPSFile
    BPSFile file = BPS.Parse(bpsNotationData);
    
    // Writing in the console a string representation of a BPSFile
    Console.WriteLine(BPS.Plain(file));
}
```
