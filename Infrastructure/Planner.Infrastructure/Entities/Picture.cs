using System;

namespace Planner.Infrastructure.Entities
{
    public class Picture
    {
        public Guid Id { get; set; }
        public byte[] Bytes { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        public string Type { get; set; }

    }
}
