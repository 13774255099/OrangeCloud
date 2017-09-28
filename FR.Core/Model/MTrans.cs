namespace FR.Core
{
    public class MTrans
    {
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sql { get; set; }

        public EType SqlType { get; set; }

        public bool IsIncrement { get; set; }

        public int? Id { get; set; }

        public string ParaName { get; set; }
    }

    public enum EType
    {
        插入 = 1,
        修改 = 2,
        删除 = 3
    }
}
