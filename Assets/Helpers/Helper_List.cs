using System.Collections.Generic;

public static class Helper_List {
    public static bool IsNull<T>(this List<T> owner) => owner == null;
    public static bool IsEmpty<T>(this List<T> owner) => owner == null || owner.Count == 0;
    public static bool IsValid<T>(this List<T> owner, int index) => 0 <= index && index < owner.Count;
    public static void Copy<T>(this List<T> owner, out List<T> list) {
        list = new();
        for(int i = 0; i < owner.Count; ++i) {
            list.Add(owner[i]);
        }
    }
}
