using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using GineCore.Common;
using System.Threading.Tasks;

namespace GineCore.Repository.BaseRepository.Base
{
    public interface IBaseRepository<TEntity> where TEntity:class, new ()
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Insert(TEntity entity);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Insert<T>(T entity) where T : class, new();

        /// <summary>
        /// 添加实体集
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task<int> Insert(List<TEntity> entitys);

        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Update(TEntity entity, params string[] fileds);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> Delete(TEntity entity);

        /// <summary>
        /// 事务删除多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> Delete(List<TEntity> entities);

        /// <summary>
        /// 查找一个实体(条件)
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        Task<TEntity> Find(string where);

        /// <summary>
        /// 查询（指定model）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        Task<T> Find<T>(string str);

        /// <summary>
        /// 完整sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<T> GetBySql<T>(string sql) where T : class, new();

        /// <summary>
        /// 获取一个表的集合(条件)
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> FindList(string where);

        /// <summary>
        /// 查询数据集（指定model）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        Task<List<T>> FindList<T>(string str = "");

        /// <summary>
        /// 完整sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<T>> FindListBySql<T>(string sql);

        /// <summary>
        /// 获取数据数量
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task<int> GetCount(string str);

        /// <summary>
        /// 自定义model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<JqGridModel<T>> FindListPager<T>(string sql, JqgridParam param);

        /// <summary>
        /// 事务执行sql
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        Task<bool> TransQuery(List<string> sqls);

        /// <summary>
        /// 事务执行sql 指定链接字符串
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="connstr"></param>
        /// <returns></returns>
        Task<bool> TransQuery(List<string> sqls, string connstr);

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        Task<int> Query(string sql);

        string SqlPagerTempalte(string sql, int start, int length);

        string SqlPagerTempalte1();

        string TotalCountSqlTemplate();
    }
}
