# MondoCore.Collections
A set of collections and collection related extensions

## Extensions

#### ToDictionary(this object obj)

#### ToStringDictionary(this object obj)

#### ToReadOnlyDictionary(this object obj)

#### GetProperties(this object obj)

public static IDictionary<K, V>? Append<K, V>(this IDictionary<K, V> dict1, IDictionary<K, V>? dict2)

public static IDictionary<K, V>? Append<K, V>(this IDictionary<K, V> dict1, IReadOnlyDictionary<K, V>? dict2)

public static IDictionary<string, string>? AppendStrings(this IDictionary<string, string> dict1, IReadOnlyDictionary<string, object>? dict2, bool childrenAsJson = false)

public static IDictionary<K, V> Merge<K, V>(this IDictionary<K, V> dict1, IDictionary<K, V> dict2)