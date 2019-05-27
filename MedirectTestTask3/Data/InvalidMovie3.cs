using System;

namespace Data
{
    /// <summary>
    /// contains excess fields
    /// </summary>
    public class InvalidMovie3 : Movie
    {
        public new int id { get; set; }
        public new string name { get; set; }
        public new string genre { get; set; }
        public new int rating { get; set; }
        public string gibberish { get; set; }
    }
}
