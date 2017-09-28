using System.Runtime.Serialization;

namespace FR.Core
{
    [DataContract]
    public enum ECacheMode
    {
        [EnumMember]
        Absolute = 1,

        [EnumMember]
        Sliding = 2
    }
}
