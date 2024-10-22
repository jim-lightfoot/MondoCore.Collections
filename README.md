# MondoCore.Collections
A set of collections and collection related extensions

## Table of Contents
- [Extensions](#extensions)
    - [Object Extensions](#object_extensions)
        - [ToDictionary](#todictionary)
        - [ToStringDictionary](#tostringdictionary)
        - [ToReadOnlyDictionary](#torodictionary)
        - [ToReadOnlyStringDictionary](#torostringdictionary)
        - [GetProperties](#getproperties)
    - [Dictionary Extensions](#dictionary_extensions)
        - [Append](#append)
        - [Append (readonly)](#appendro)
        - [AppendStrings](#appendstrings)
        - [Merge](#appendstrings)
- [Collections](#collections)
    - [ConcurrentList](#concurrentlist)
    - [ConcurrentKeyedQueue](#concurrentkeyedqueue)

<a name="extensions" />

## Extensions

<a name="object_extensions" />

### Object Extensions

***

<a name="todictionary" />

#### IDictionary<string, object> ToDictionary(this object obj)

>Converts a POCO, anonymous object or a different kind of dictionary into a string/object dictionary

***
<a name="tostringdictionary" />

#### IDictionary<string, string> ToStringDictionary(this object obj)
>Converts a POCO, anonymous object or a different kind of dictionary into a string/string dictionary


***
<a name="torodictionary" />

#### IReadOnlyDictionary<string, object> ToReadOnlyDictionary(this object obj)
>Converts a POCO, anonymous object or a different kind of dictionary into a string/object readonly dictionary


***
<a name="torostringdictionary" />

#### IReadOnlyDictionary<string, string> ToReadOnlyStringDictionary(this object obj)
>Converts a POCO, anonymous object or a different kind of dictionary into a string/string readonly dictionary

***
<a name="getproperties" />

#### IEnumerable<(string Name, object Value)>GetProperties(this object obj)
>Retrieves the properties and their values from a POCO or anonymous object

<a name="dictionary_extensions" />

### Dictionary Extensions

***
<a name="append" />

#### IDictionary<K, V>? Append<K, V>(this IDictionary<K, V> dict1, IDictionary<K, V>? dict2)
>Adds all the keyvalue pairs from the second dictionary into the first (the first dictionary will be returned)
 
***
<a name="appendro" />

#### IDictionary<K, V>? Append<K, V>(this IDictionary<K, V> dict1, IReadOnlyDictionary<K, V>? dict2)
>Adds all the keyvalue pairs from the second dictionary into the first (the first dictionary will be returned)
 
***
<a name="append_string" />

#### IDictionary<string, string>? AppendStrings(this IDictionary<string, string> dict1, IReadOnlyDictionary<string, object>? dict2, bool childrenAsJson = false)
>Adds all the keyvalue pairs from the second dictionary into the first as strings. If the value being added is an object then it will be either added as serialized json or dotted values (e.g. Address.ZipCode)
 
***
<a name="merge" />

#### IDictionary<K, V> Merge<K, V>(this IDictionary<K, V> dict1, IDictionary<K, V> dict2)
>Adds all the keyvalue pairs from the second dictionary into the first (or returns the second dictionary if the first is empty)

<a name="collections" />

## Collections

***
<a name="concurrentlist" />

### class ConcurrentList
> An implementation of IList that is thread-safe

***
<a name="concurrentkeyedqueue" />

### class ConcurrentKeyedQueue
> A thread safe queue that can also be accessed via a key (like a dictionary)

