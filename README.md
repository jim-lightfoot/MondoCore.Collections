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

#### <small>IDictionary<string, object></small> ToDictionary<small>(this object obj)</small>

>Converts a POCO, anonymous object or a different kind of dictionary into a string/object dictionary

***
<a name="tostringdictionary" />

#### <small>IDictionary<string, string></small> ToStringDictionary<small>(this object obj)</small>
>Converts a POCO, anonymous object or a different kind of dictionary into a string/string dictionary


***
<a name="torodictionary" />

#### <small>IReadOnlyDictionary<string, object></small> ToReadOnlyDictionary<small>(this object obj)</small>
>Converts a POCO, anonymous object or a different kind of dictionary into a string/object readonly dictionary


***
<a name="torostringdictionary" />

#### <small>IReadOnlyDictionary<string, string></small> ToReadOnlyStringDictionary<small>(this object obj)</small>
>Converts a POCO, anonymous object or a different kind of dictionary into a string/string readonly dictionary

***
<a name="getproperties" />

#### <small>IEnumerable<(string Name, object Value)></small> GetProperties<small>(this object obj)</small>
>Retrieves the properties and their values from a POCO or anonymous object

<a name="dictionary_extensions" />

### Dictionary Extensions

***
<a name="append" />

#### <small>IDictionary<K, V>?</small> Append<K, V><small>(this IDictionary<K, V> dict1, IDictionary<K, V>? dict2)</small>
>Adds all the keyvalue pairs from the second dictionary into the first (the first dictionary will be returned)
 
***
<a name="appendro" />

#### <small>IDictionary<K, V>?</small> Append<K, V><small>(this IDictionary<K, V> dict1, IReadOnlyDictionary<K, V>? dict2)</small>
>Adds all the keyvalue pairs from the second dictionary into the first (the first dictionary will be returned)
 
***
<a name="append_string" />

#### <small>IDictionary<string, string>?</small> AppendStrings<small>(this IDictionary<string, string> dict1, IReadOnlyDictionary<string, object>? dict2, bool childrenAsJson = false)</small>
>Adds all the keyvalue pairs from the second dictionary into the first as strings. If the value being added is an object then it will be either added as serialized json or dotted values (e.g. Address.ZipCode)
 
***
<a name="merge" />

#### <small>IDictionary<K, V></small> Merge<K, V><small>(this IDictionary<K, V> dict1, IDictionary<K, V> dict2)</small>
>Adds all the keyvalue pairs from the second dictionary into the first (or returns the second dictionary if the first is empty)

<a name="collections" />

## Collections

***
<a name="concurrentlist" />

#### class ConcurrentList
> An implementation of IList that is thread-safe

***
<a name="concurrentkeyedqueue" />

#### class ConcurrentKeyedQueue
> A thread safe queue that can also be accessed via a key (like a dictionary)

