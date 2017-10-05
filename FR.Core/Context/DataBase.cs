using System;
using System.Collections.Generic;
using System.Text;

namespace FR.Core
{
    public sealed class DataBase
    {
        public static IDataBase Instance
        {
            get { return SingletonCreator.instance; }
        }
        class SingletonCreator
        {
            internal static string assName = "OrangeCloud.Core";

            internal static readonly IDataBase instance =
                InstanceBase.CreateInstance<IDataBase>(assName, assName, Base.FR_Core_DataTable_Type);
            //InstanceBase.CreateInstance<IDataBase>(assName, assName + ".DataBase", Base.DataBase);
        }
    }
}
