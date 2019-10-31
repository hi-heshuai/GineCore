using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GineCore.Common;
using GineCore.Entity;
using GineCore.EntityFrameworkCore.DbExtensions;
using GineCore.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using GineCore.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GineCore.Repository.BaseRepository.Base
{
    public class BaseRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly IDBOperate dbHelper = DbHelper.InitDBOperate(typeof(TEntity).Assembly.GetName().Name);
        private readonly string dataBaseType = EntityToSql.DataBaseType<TEntity>();

        #region 
        public async Task<int> Insert(TEntity entity)
        {
            string sql = EntityToSql.AddSql(entity);
            var result = Convert.ToInt32(await dbHelper.GetSingle(sql));
            return result;
        }

        public async Task<int> Insert<T>(T entity) where T : class, new()
        {
            string sql = EntityToSql.AddSql<T>(entity);
            var result = await dbHelper.ExecuteSql(sql);
            return result;
        }

        public async Task<int> Insert(List<TEntity> entitys)
        {
            var sqls = new List<string>();

            foreach (var entity in entitys)
            {
                string sql = EntityToSql.AddSql(entity);
                sqls.Add(sql);
            }

            await dbHelper.ExecuteSqlTran(sqls);

            return sqls.Count;
        }

        public async Task<int> Update(TEntity entity, params string[] fileds)
        {
            var sql = EntityToSql.UpdateSql(entity, fileds);
            var result = await dbHelper.ExecuteSql(sql);
            return result;
        }

        public async Task<int> Delete(TEntity entity)
        {
            var sql = EntityToSql.DeleteTemplate(entity);
            var result = await dbHelper.ExecuteSql(sql);
            return result;
        }

        public async Task<int> Delete(List<TEntity> entities)
        {
            List<string> sqls = new List<string>();
            foreach (var entity in entities)
            {
                sqls.Add(EntityToSql.DeleteTemplate(entity));
            }
            var result = await dbHelper.ExecuteSqlTran(sqls);
            return result;
        }

        public async Task<TEntity> Find(string where)
        {
            var sql = EntityToSql.SelectSql<TEntity>(where);
            var dataSet = await dbHelper.Query(sql);
            var dataTable = dataSet.Tables[0];
            var model = DataTableToEntityHelper.ToEntity<TEntity>(dataTable);
            return model;
        }

        public async Task<T> Find<T>(string where)
        {
            var sql = EntityToSql.SelectSql<TEntity>(where);
            var dataSet = await dbHelper.Query(sql);
            var dataTable = dataSet.Tables[0];
            var model = DataTableToEntityHelper.ToEntity<T>(dataTable);
            return model;
        }

        public async Task<T> GetBySql<T>(string sql) where T : class, new()
        {
            var dataSet = await dbHelper.Query(sql);
            var dataTable = dataSet.Tables[0];
            var model = DataTableToEntityHelper.ToEntity<T>(dataTable);
            return model;
        }

        public async Task<List<TEntity>> FindList(string where)
        {
            var sql = EntityToSql.SelectSql<TEntity>(where);
            var dataSet =await dbHelper.Query(sql);
            var dataTable =dataSet.Tables[0];
            var model = DataTableToEntityHelper.ToEntityList<TEntity>(dataTable);
            return model;
        }

        public async Task<List<T>> FindList<T>(string where)
        {
            var sql = EntityToSql.SelectSql<TEntity>(where);
            var dataSet = await dbHelper.Query(sql);
            var dataTable = dataSet.Tables[0];
            var model = DataTableToEntityHelper.ToEntityList<T>(dataTable);
            return model;
        }

        public async Task<List<T>> FindListBySql<T>(string sql)
        {
            var dataSet = await dbHelper.Query(sql);
            var dataTable = dataSet.Tables[0];
            var model = DataTableToEntityHelper.ToEntityList<T>(dataTable);
            return model;
        }

        public async Task<int> GetCount(string str)
        {
            string sql = "";
            if (str.Contains("select"))
            {
                sql = str;
            }
            else
            {
                sql = EntityToSql.SelectSql<TEntity>(str);
            }

            return Convert.ToInt32(await dbHelper.GetSingle(sql));
        }

        public async Task<JqGridModel<T>> FindListPager<T>(string str, JqgridParam param)
        {
            JqGridModel<T> resultModel = new JqGridModel<T>();

            var skip = (param.page - 1) * param.rows;

            string sql = string.Format(SqlPagerTempalte(str, skip, param.rows));
            var ds = await dbHelper.Query(sql);
            var dt = ds.Tables[0];
            var entities = DataTableToEntityHelper.ToEntityList<T>(dt);

            string countSql = string.Format(TotalCountSqlTemplate(), str);

            resultModel.records = int.Parse((await dbHelper.GetSingle(countSql)).ToString());
            resultModel.total = resultModel.records % param.rows == 0 ? resultModel.records / param.rows : resultModel.records / param.rows + 1;
            resultModel.rows = entities.ToList();
            resultModel.page = param.page;

            return resultModel;
        }

        public async Task<bool> TransQuery(List<string> sqls)
        {
            try
            {
                await dbHelper.ExecuteSqlTran(sqls);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> TransQuery(List<string> sqls, string connstr)
        {
            try
            {
                await dbHelper.ExecuteSqlTran(sqls, connstr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        public async Task<int> Query(string sql)
        {
            return await dbHelper.ExecuteSql(sql);
        }
        #endregion

        /// <summary>
        /// 分页sql模板
        /// </summary>
        /// <returns></returns>
        public string SqlPagerTempalte(string sql, int start, int length)
        {
            string str = string.Empty;
            switch (dataBaseType)
            {
                case "sqlserver":
                    str = string.Format("select * from (" +
                "select  row_number()over(order by tempcolumn)Rownumber,* from (" +
                "select top {0} tempcolumn=0 , * from (" +
                "{1})temp1)temp2 " +
                ")temp3 " +
                "where Rownumber > {2}", start + length, sql, start);
                    break;
                case "mysql":
                    str = string.Format("{0} limit {1}, {2}", sql, start, length);
                    break;
            }
            return str;
        }

        /// <summary>
        /// 分页sql模板
        /// </summary>
        /// <returns></returns>
        public string SqlPagerTempalte1()
        {
            return "select * from (" +
                "select  row_number()over(order by tempcolumn1)Rownumber1,* from (" +
                "select top {0} tempcolumn1=0 , * from (" +
                "{1})temp4)temp5 " +
                ")temp6 " +
                "where Rownumber1 > {2}";
        }

        /// <summary>
        /// 获取总条目数sql语句模板
        /// </summary>
        /// <returns></returns>
        public string TotalCountSqlTemplate()
        {
            return "select count(*)totalCount from (" +
                 "{0}" +
                ")a";
        }
    }
}
