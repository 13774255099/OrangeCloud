using System;
using System.Collections.Generic;
using System.Text;

namespace FR.Core
{
    public abstract class IFillEntity
    {
        public abstract void SetInsertSysCols<T>(T entity);

        public abstract void SetUpdateSysCols<T>(T entity);

        public abstract void SetDeleteSysCols<T>(T entity);
    }
}
