using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GineCore.Common;
using GineCore.Repository.BaseRepository.Base;
using System.Threading;

namespace GineCore.Service
{
    public interface IBaseService<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        Task<int> Create(TEntity Entity);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<TEntity> Get(int Id);

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<T> Get<T>(int Id) where T : class, new();

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntities(List<int> ids);

        /// <summary>
        /// 条件获取
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<T> Get<T>(string where) where T : class, new();

        /// <summary>
        /// 条件获取
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<List<T>> GetEntities<T>(string where) where T : class, new();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        Task<int> Delete(TEntity Entity);

        /// <summary>
        /// 事务删除多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> Delete(List<TEntity> entities);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Update(TEntity entity, params string[] fileds);
    }

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }


        public async Task<int> Create(TEntity entity)
        {
            return await _repository.Insert(entity);
        }

        public async Task<int> Create<T>(T entity) where T : class, new()
        {
            return await _repository.Insert(entity);
        }

        public async Task<List<TEntity>> GetEntities(List<int> ids)
        {
            string where = "";
            ids.ForEach(u =>
            {
                where += u.ToString() + ",";
            });

            return await _repository.FindList(string.Format(" id in({0})", where.TrimEnd(',')));
        }

        public async Task<TEntity> Get(int id)
        {
            return await _repository.Find(" id=" + id);
        }

        public async Task<T> Get<T>(int id) where T : class, new()
        {
            return await _repository.Find<T>(" id=" + id);
        }

        public async Task<T> Get<T>(string where) where T : class, new()
        {
            return await _repository.Find<T>(where);
        }

        public async Task<T> GetBySql<T>(string sql) where T : class, new()
        {
            return await _repository.GetBySql<T>(sql);
        }

        public async Task<List<T>> GetEntities<T>(string where) where T : class, new()
        {
            return await _repository.FindList<T>(where);
        }

        public async Task<int> Delete(TEntity entity)
        {
            return await _repository.Delete(entity);
        }

        public async Task<int> Delete(List<TEntity> entities)
        {
            return await _repository.Delete(entities);
        }

        public async Task<TEntity> Update(TEntity entity, params string[] fileds)
        {
            await _repository.Update(entity, fileds);
            return entity;
        }

        /// <summary>
        /// 执行完整sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<int> Query(string sql)
        {
            return await _repository.Query(sql);
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<int> GetCount(string sql)
        {
            return await _repository.GetCount(sql);
        }

        /// <summary>
        /// 完整sql查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task< List<T>> FindListBySql<T>(string sql)
        {
            return await _repository.FindListBySql<T>(sql);
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public async Task<bool> TransQuery(List<string> sqls)
        {
            return await _repository.TransQuery(sqls);
        }

        /// <summary>
        /// 执行事务 指定链接字符串
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public async Task<bool> TransQuery(List<string> sqls, string connStr)
        {
            return await _repository.TransQuery(sqls, connStr);
        }

        #region 返回数据
        /// <summary>
        /// 成功返回格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="operateName">存在则写日志</param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public ResponseModel<T> Success<T>(T data, string operateName = "", string loginName = "")
        {
            ResponseModel<T> model = new ResponseModel<T>()
            {
                result = true,
                errorInfo = "",
                data = data
            };
            return model;
        }

        /// <summary>
        /// 成功返回格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="operateName">存在则写日志</param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public ResponseModel<T> Success<T>() where T : class, new()
        {
            ResponseModel<T> model = new ResponseModel<T>()
            {
                result = true,
                errorInfo = "",
                data = null
            };
            return model;
        }

        /// <summary>
        /// 失败返回格式
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <param name="operateName"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public ResponseModel<T> Fail<T>(string errorInfo, string operateName = "", string loginName = "")
            where T : class, new()
        {
            ResponseModel<T> model = new ResponseModel<T>()
            {
                result = false,
                errorInfo = errorInfo,
                data = null
            };

            return model;
        }
        #endregion
    }
}
