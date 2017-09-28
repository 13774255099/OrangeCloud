using System.Runtime.Serialization;

namespace FR.Core
{
    [DataContract]
    public enum ETimeMode
    {
        [EnumMember]
        Minutes = 1,

        [EnumMember]
        Second = 2
    }
}
