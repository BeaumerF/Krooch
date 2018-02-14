using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Realms;

namespace epicture
{
    class RealmImage : RealmObject
    {
        public int id { get; set; }
        public String image { get; set; }
    }
}
