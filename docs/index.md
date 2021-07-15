# <a name="top"></a>BPS - Basic Persistence System

BPS is a key-value persistence system that provides a data structure that allows to save and load from disk.


## <a name="menu"></a>Contents

- [Introduction](#introduction)
  * [Features](#features)
  * [Example](#example)
- [Getting Started](#getting-started)
  * [Download & Installation](#download)
- [Documentation](docs/documentation/index.md)
- [License](#license)


## <a name="introduction"></a>Introduction

### <a name="features"></a>Features

- In-code key-value data structure;
- Key-value data persistence;
- Allows store multi-dimensional arrays.


### <a name="example"></a>Example

Here are most common operations in BPS.

#### Data Structure Common Operations

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

#### Disk Operations
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


## <a name="getting-started"></a>Getting Started

Follow the steps here to get started with BPS.

### <a name="download"></a>Download & Installation

BPS x.x.x is the latest stable release. It can be downloaded [here]().

#### <a name="installation"></a>Installation

TODO: write installation steps


## <a name="license"></a>License

BPS is an open source project released under the [MIT](../LICENSE) license.

<br>

---

[Back top](#top)

<br>
