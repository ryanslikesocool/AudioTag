// Developed with love by Ryan Boyer http://ryanjboyer.com <3

namespace AudioTag {
    public static class Extensions {
        public static int GetTagID(this string tag) => tag == null || tag == string.Empty || tag == "" ? 0 : tag.GetHashCode();
    }
}