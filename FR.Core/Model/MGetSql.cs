using System.Collections.Generic;

namespace FR.Core
{
    public class MGetSql
    {
        public IList<MLeftJoin> list { set; get; }

        public string wheres { set; get; }

        public string orderBy { set; get; }

        public int pageIndex { set; get; }

        public int pageSize { set; get; }
    }
}
