# BPS Handler

BPS is a key-value data storing structure. This handler provides a BPS data structure for C#.


## Guides and Documentation

The documentation of BPS and this handler can be found [here](https://bps-lib.github.io/). It contains all guides and detailed documentation.


## Handler Common Operations

#### Adding and removing data

```csharp
public void Foo()
{
    BPSFile file = new BPSFile();

    // Adding values
    file.Add("key", "value");
    file.Add("int", 10);
    file.Add("float", 1.5);
    file.Add("bool", true);
    file.Add("arr", new List<object> { 0, 1, 2 });

    // Removing values
    file.Remove("key");
    file.Remove("arr");
}
```

#### Disk operations

```csharp
public void Foo(string path)
{
    // Loading a BPS file from disk
    BPSFile file = BPS.Load(path);
    // Adding a value
    file.Add("string", "example");
    // Saving new file
    BPS.Save(file, path);
}
```
