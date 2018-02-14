using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epicture
{
    class Filter
    {
        public String search { get; set; }
        public String aspect { get; set; }
        public String size { get; set; }
        public String type { get; set; }
        public String isFree { get; set; }
        public int? nbResult { get; set; }
    }
}
