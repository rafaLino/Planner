using Planner.Domain.Users;

namespace Planner.Application.Results
{
    public class PictureResult
    {

        public byte[] Bytes { get; set; }

        public long Size { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public PictureResult(byte[] bytes, long size, string type, string name)
        {
            Bytes = bytes;
            Size = size;
            Type = type;
            Name = name;
        }

        public static implicit operator PictureResult(Picture picture)
        {
            PictureResult result = null;
            if (picture != null)
                result = new PictureResult(picture._bytes, picture._size, picture._type, picture._name);

            return result;
        }
    }
}
