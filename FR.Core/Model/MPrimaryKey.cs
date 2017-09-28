namespace FR.Core
{
    public class MPrimaryKey
    {
        /// <summary>
        /// 主键名称
        /// </summary>
        public string KeyName { get; set; }
        /// <summary>
        /// 是否自增(true = 是, false = 否)
        /// </summary>
        public bool IsIncrement { get; set; }
    }
}
