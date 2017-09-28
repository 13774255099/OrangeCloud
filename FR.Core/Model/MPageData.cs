using System.Collections.Generic;

namespace FR.Core
{
    public class MPageData<T>
    {
        /// <summary>
		/// 
		/// </summary>
		public int total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<T> rows { get; set; }
    }
}
