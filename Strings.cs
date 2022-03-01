// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System.Collections.Generic;
using UnityEngine;

namespace AudioTag {
    internal class Strings {
        private static Dictionary<int, string> backing = new Dictionary<int, string>();

        public static void Reset() => backing.Clear();

        public static int Add(string str) {
            if (!string.IsNullOrEmpty(str)) {
                int id = str.GetHashCode();
                backing.TryAdd(id, str);
                return id;
            }
            return 0;
        }

        public static string Get(int id) {
            backing.TryGetValue(id, out string str);
            return str;
        }
    }
}