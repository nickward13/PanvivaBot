using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PanvivaBot
{
    public class PanvivaNLSSearchResponseItem
    {
        public int Id { get; set; }

        public string ResponseContent { get; set; }

        public string Category { get; set; }

        public float SearchScore { get; set; }
    }
}
