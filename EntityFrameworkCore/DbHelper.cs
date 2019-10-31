using GineCore.EntityFrameworkCore.DbExtensions;

namespace GineCore.EntityFrameworkCore
{
    /// <summary>
    /// 获取数据库操作对象
    /// </summary>
    public class DbHelper
    {
        public static readonly string userConnect = "server=118.25.83.200;database=mydb;userid=root;password=123456;charset=utf8";

        public static IDBOperate dbOperate;

        public static IDBOperate InitDBOperate(string connType)
        {
            switch (connType)
            {
                case "GineCore.Entity":
                    dbOperate = new DBOperateMySql(userConnect);
                    break;
            }

            return dbOperate;
        }
    }
}
