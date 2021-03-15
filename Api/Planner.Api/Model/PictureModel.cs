using System.ComponentModel.DataAnnotations;

namespace Planner.Api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PictureModel
    {
        /// <summary>
        /// file content in byte array
        /// </summary>
        [Required]
        public byte[] Bytes { get; set; }

        /// <summary>
        /// file size
        /// </summary>
        [Required]
        public long Size { get; set; }

        /// <summary>
        /// file name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// file type
        /// </summary>
        [Required]
        public string Type { get; set; }

    }
}
