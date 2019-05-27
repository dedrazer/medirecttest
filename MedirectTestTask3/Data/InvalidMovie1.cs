using System;

namespace Data
{
    /// <summary>
    /// utilizes a textual rating
    /// </summary>
    public class InvalidMovie1 : Movie
    {
        public new int id { get; set; }
        public new string name { get; set; }
        public new string genre { get; set; }
        public new string rating { get; set; }
    }
}
