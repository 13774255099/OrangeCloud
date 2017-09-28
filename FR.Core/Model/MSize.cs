using System.Runtime.Serialization;

namespace FR.Core
{
    [DataContract]
    public class MSize
    {
        [DataMember]
        public string name { set; get; }

        [DataMember]
        public int width { set; get; }

        [DataMember]
        public int height { set; get; }
    }
}
