using System;
using System.Collections.Generic;
using SqlSugar;
using System.Data;
using System.Linq.Expressions;

namespace ExternalAPI
{

    internal class CoreAPIList : IBaseCore<APIList>
    {
         ServiceAPIList _ServiceAPIList = new ServiceAPIList();
         
          public ISugarQueryable<APIList> LoadEntities(Expression<Func<APIList, bool>> whereLambda)
        {
            return _ServiceAPIList.LoadEntities(whereLambda);
        }

         public ISugarQueryable<APIList> LoadEntities(Expression<Func<APIList, bool>> whereLambda, Expression<Func<APIList, object>> orderExpression, OrderByType type = OrderByType.Asc)
        {
            return _ServiceAPIList.LoadEntities(whereLambda, orderExpression, type);
        }

         public List<APIList> LoadPageList(Expression<Func<APIList, bool>> whereLambda, Expression<Func<APIList, object>> orderExpression, OrderByType type, int pageIndex, int pageSize, ref int totalNumber)
        {
            return _ServiceAPIList.LoadPageList(whereLambda, orderExpression, type, pageIndex, pageSize, ref totalNumber);
        }

        
        public DataTable LoadEntitiesBySql(string sql, List<SugarParameter> parameters)
        {
            return _ServiceAPIList.LoadEntitiesBySql(sql, parameters);
        }
        public int AddEntity(APIList t)
        {
            return _ServiceAPIList.AddEntity(t);
        }
         public int AddEntitys(List<APIList> _list)
        {
            return _ServiceAPIList.AddEntitys(_list);
        }
         
          public int UpdateEntity(APIList t)
        {
            return _ServiceAPIList.UpdateEntity(t);
        }
         
         public int UpdateEntityBySomeColums(APIList t, Expression<Func<APIList, object>> columns)
        {
            return _ServiceAPIList.UpdateEntityBySomeColums(t, columns);
        }
         
          public int UpdateEntityByIgnoreColumns(APIList t, Expression<Func<APIList, object>> columns)
        {
            return _ServiceAPIList.UpdateEntityByIgnoreColumns(t, columns);
        }
         
          public int DeleteEntity(APIList t)
        {
            return _ServiceAPIList.DeleteEntity(t);
        }
          public int DeleteEntitys(List<APIList> _list)
        {
            return _ServiceAPIList.DeleteEntitys(_list);
        }
        
        public int DeleteEntityBySomeColum(APIList t, Expression<Func<APIList, bool>> whereExpression)
        {
            return _ServiceAPIList.DeleteEntityBySomeColum(t, whereExpression);
        }

        public int DeleteEntitysBySomeColum(List<APIList> _list, params Expression<Func<APIList, bool>>[] whereExpression)
        {
            return _ServiceAPIList.DeleteEntitysBySomeColum(_list, whereExpression);
        }
        
          public int ExecuteCommand(string sql, List<SugarParameter> parameters)
        {
            return _ServiceAPIList.ExecuteCommand(sql, parameters);
        }
         public int ExecuteCommand(string[] sqlArray, List<SugarParameter>[] parameters)
        {
            return _ServiceAPIList.ExecuteCommand(sqlArray, parameters);
        }

    }
}
