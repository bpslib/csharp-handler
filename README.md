# BPS Handler

BPS is a key-value data storing structure. This handler provides a BPS data structure for C#.


## Guides and Documentation

The documentation of BPS and this handler can be found [here](https://bps-lib.github.io/). It contains all guides and detailed documentation.


## BPS Handler Operations

### BPSFile class

The class `BPSFile` represents a structure of a BPS file.

#### Adding and removing data

To add or remove a value we use the function `Add()` passing the key and value. To remove, we use the function `Remove()` passing just the key.
Example:

```csharp
public void Foo()
{
    BPSFile file = new BPSFile();

    // Adding values
    file.Add("key", "value");
    // Left param is the key and Right param is the value
    file.Add("int", 10);
    file.Add("float", 1.5);
    file.Add("bool", true);
    file.Add("arr", new List<object> { 0, 1, 2 });

    // Removing values
    file.Remove("key");
    // We just need to pass the key to remove
    file.Remove("arr");
}
```

#### Finding and checking if data exists

To retrieve a value we use the function `Find()` passing the key we want to retrieve. To check if a value exists in the file, we use the function `Contains()` passing the just key too.
We do not need to check if a value exists before retrieve that, once we try to retrieve a no existing value, we will receive a null return.

```csharp
public void Foo(BPSFile file)
{
    // Checking if a data exists in a BPSFile
    bool existsKey = file.Contains("key");
    // Retrieving the value
    // The return will be an object
    var value = file.Find("key");

    if (existsKey)
    {
        Console.WriteLine("The key \"key\" exists.");
    }
    else
    {
        Console.WriteLine("The key \"key\" do not exists.");
    }

    return value;
}
```

### BPS Class

#### Disk operations

To save the file on disk, we use the function `Save()`. There are two ways to use this function. The first one is using the class `BPS`, where we call the `Save()` function passing the `BPSFile` object and the path.

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

#### Parsing operations

The `BPS` class has two methods to transform data. The method `Parse()` will parse a string containing data in BPS notation. The method `Plain()` will parse a `BPSFile` in a string containing the data in BPS notation.

```csharp
public void Foo()
{
    string bpsNotationData = "intValue:256;";

    // Parsing a string in a BPSFile
    BPSFile file = BPS.Parse(bpsNotationData);
    
    // Writing in the console a string representation of a BPSFile
    Console.WriteLine(file);
}
```
