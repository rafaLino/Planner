namespace Planner.Domain.Users
{
    public sealed class Picture
    {
        public byte[] _bytes;

        public long _size;

        public string _type;

        public string _name;


        private Picture() { }

        public static Picture Create(byte[] bytes, long size, string type, string name)
        {
            Picture picture = new Picture();
            picture._bytes = bytes;
            picture._size = size;
            picture._type = type;
            picture._name = name;
            return picture;
        }

        public static Picture Load(byte[] bytes, long size, string type, string name)
        {
            Picture picture = new Picture();
            picture._bytes = bytes;
            picture._size = size;
            picture._type = type;
            picture._name = name;
            return picture;
        }
    }
}
