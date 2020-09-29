# Prox.IO
A .Net Standard library wrapping System.IO classes in interfaces and proxy classes to allow for better DI and test capability when working with the file system.

Some method overloads present in the full framework libraries are found here, but it should be assumed that types will follow the .Net Core api for accessing the filesystem.

## The struggle is real
The need to write files exists, and unfortunately that code can sometimes be more complex than you might be comfortable with, given that much of the file system api has been centered around easy to use, hard to test **static** methods (ie. `File.Create("c:/readme.md")`). If you aren't a fan of using Static Ave. to access the file system, there are non-static classes offering the same functionality.

`File.Create("c:/readme.md");` = `new FileInfo("c:/readme.md").Create()`

But using the `FileInfo` instance does very little for us as developers when it comes to abstracting the file system away from unit tests, because we needs the environment to actually be what the app expects when dealing with files. This, to me, is not acceptable for unit testing.

## We all do it
Any developer work their salt have come to learn that these classes need interfaces to follow, so that we can abstract the file system away from the file system client. This way we can feed the client a mock that looks like whatever we want and the client doesn't know any better. (re: [Liskov Substitution Principle][liskov])

If you're anything like me, you probably wrote many of these tiny little proxies that only wrap the functionality you're using at the time. While this is a very appropriate use of the [Proxy Pattern][proxpattern], it gets tiring to rewrite all the same (or very similar) code just to proxy a few methods for the file system.

**Prox.IO** is a very simple library with classes and interfaces that proxy the non-static representation of these legendary static methods.  

## Example:
Simple file writing operations.
```
// program.cs (or whatever your composition root is)
var fileWriterFactory = new FileWriterFactory(report, azureRepository, _logFactory);

ItWritable writer;
var fi = new FileInfoProxy("c:/readme.md");
writer = fileWriterFactory.GetWriter(WriterType.Text);
writer.Write(fi);
```

```
// TextFileWriter.cs
public void Write(IFileInfoProxy fileInfo)
{
    var file = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));

    var dir = file.Directory ?? throw new ArgumentException("The file info given does not have a value in its Directory property");
    if (!dir.Exists)
    {
        dir.Create();
    }

    using (var s = file.CreateText())
    {
        if (s == null) throw new InvalidOperationException("The creation of a stream writer for the file to be written resulted in a null value.");
        s.Write("some text");
    }
}
```


[liskov]: https://en.wikipedia.org/wiki/Liskov_substitution_principle
[proxpattern]: https://en.wikipedia.org/wiki/Proxy_pattern
