namespace FR.Core
{
    public class MSplitTableConfig
    {
        /// <summary>
        /// 类型
        /// </summary>
        public ESplitTableType Type { get; set; }

        /// <summary>
        /// 例如：yyyyMM , yyyy, yyyyMMdd
        /// </summary>
        public string DateTimeConfig { get; set; }

        /// <summary>
        /// 例如：8 （第一次设置，不可更改）
        /// </summary>
        public int HashValueConfig { get; set; }
    }
}
