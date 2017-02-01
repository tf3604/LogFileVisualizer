using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizer
{
    [DataContract]
    public enum LayoutStyle
    {
        [DataMember]
        Physical,

        [DataMember]
        Logical
    }
}
