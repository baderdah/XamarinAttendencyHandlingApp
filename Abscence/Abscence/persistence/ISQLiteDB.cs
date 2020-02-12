using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abscence.persistence
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
