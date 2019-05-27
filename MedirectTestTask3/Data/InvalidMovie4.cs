using System;

namespace Data
{
    /// <summary>
    /// utilizes strings instead of integers
    /// </summary>
    public class InvalidMovie4 : Movie
    {
        public new string id { get; set; }
        public new string name { get; set; }
        public new string genre { get; set; }
        public new string rating { get; set; }
    }
}
