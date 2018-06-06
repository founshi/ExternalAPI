using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExternalAPI
{
    internal interface IBaseService<T> where T : class,new()
    {
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="type">升序还是降序排序</param>
        /// <returns></returns>
        ISugarQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);

        ISugarQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda, Expression<Func<T, object>> orderExpression, OrderByType type = OrderByType.Asc);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="orderExpression">排序表达式</param>
        /// <param name="type">升序还是降序排序</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalNumber">总页数</param>
        /// <returns>查询结果</returns>
        List<T> LoadPageList(Expression<Func<T, bool>> whereLambda, Expression<Func<T, object>> orderExpression, OrderByType type, int pageIndex, int pageSize, ref int totalNumber);
        /// <summary>
        /// 通过SQL语句 加载数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">条件参数</param>
        /// <returns>返回查询出的结果</returns>
        DataTable LoadEntitiesBySql(string sql, List<SugarParameter> parameters);

        /// <summary>
        /// 当个实体插入返回的影响行数
        /// </summary>
        /// <param name="t">插入的实体</param>
        /// <returns>影响的行数</returns>
        int AddEntity(T t);
        /// <summary>
        /// 多行实体插入
        /// </summary>
        /// <param name="_list">插入的实体队列</param>
        /// <returns>影响的行数</returns>
        int AddEntitys(List<T> _list);
        /// <summary>
        /// 根据主键更新全部列
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int UpdateEntity(T t);

        /// <summary>
        /// 指定更新哪些列来更新实体(只更新指定列),通过主键更新
        /// </summary>
        /// <param name="t">需要更新的实体</param>
        /// <param name="columns">需要更新列的表达式</param>
        /// <returns>影响的行数</returns>
        int UpdateEntityBySomeColums(T t, Expression<Func<T, object>> columns);
        /// <summary>
        /// 指定忽略哪些列来更新实体(除了指定的列其他列都更新),通过主键更新
        /// </summary>
        /// <param name="t">需要更新的实体</param>
        /// <param name="columns">不需要更新列的表达式</param>
        /// <returns>影响的行数</returns>
        int UpdateEntityByIgnoreColumns(T t, Expression<Func<T, object>> columns);

        /// <summary>
        /// 删除实体,根据主键删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int DeleteEntity(T t);
        /// <summary>
        /// 批量删除实体,根据主键删除
        /// </summary>
        /// <param name="_list"></param>
        /// <returns></returns>
        int DeleteEntitys(List<T> _list);
        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="t">实体</param>
        /// <param name="whereExpression">删除条件表达式</param>
        /// <returns>受影响的函数</returns>
        int DeleteEntityBySomeColum(T t, Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// 根据条件批量删除实体
        /// </summary>
        /// <param name="_list">实体集合</param>
        /// <param name="whereExpression">删除条件表达式</param>
        /// <returns>受影响的函数</returns>
        int DeleteEntitysBySomeColum(List<T> _list, params Expression<Func<T, bool>>[] whereExpression);
        /// <summary>
        /// 通过SQL 语句来更新或者插入数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteCommand(string sql, List<SugarParameter> parameters);
        /// <summary>
        /// 通过SQL 语句来更新或者插入数据
        /// </summary>
        /// <param name="sqlArray">语句数组</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteCommand(string[] sqlArray, List<SugarParameter>[] parameters);


    }
}