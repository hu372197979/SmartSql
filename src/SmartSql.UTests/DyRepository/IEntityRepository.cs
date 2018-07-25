﻿using SmartSql.Abstractions;
using SmartSql.DyRepository;
using SmartSql.UTests.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SmartSql.UTests.DyRepository
{
    public interface IEntityRepository : IRepository<T_Entity, long>, IRepositoryAsync<T_Entity, long>
    {
        IEnumerable<long> QueryId();
        IEnumerable<EntityStatus> QueryStatus();
        IEnumerable<EntityStatus?> QueryNullStatus();
        [Statement(Id = "Update")]
        long UpdateEntity(T_Entity entity);

        IEnumerable<T_Entity> Query([Param("FString")]String fString);

        [Statement(Id = "MultiQuery")]
        IMultipleResult QueryMultiple();
        [Statement(Id = "MultiQuery")]
        (IEnumerable<T_Entity>, IEnumerable<T_Entity>) QueryMultiple_VT();
        [Statement(Id = "MultiQuery")]
        DataTable QueryDataTable();

        [Statement(Id = "MultiQuery")]
        DataSet QueryDataSet();

        [Statement(Sql = "Select Top(@taken) T.* From T_Entity T With(NoLock);")]
        IEnumerable<T_Entity> QueryBySql(int taken);

        Task DeleteAsync(long Id);
        Task<IEnumerable<T_Entity>> QueryAsync(int Taken);
        Task<T_Entity> GetEntityAsync(long Id);
        [Statement(Id = "MultiQuery", SourceChoice = Abstractions.DataSourceChoice.Write)]
        Task<DataTable> QueryDataTableAsync();
        [Statement(Id = "MultiQuery")]
        Task<DataSet> QueryDataSetAsync();
        [Statement(Sql = "Select Top(@taken) T.* From T_Entity T With(NoLock);")]
        Task<IEnumerable<T_Entity>> QueryBySqlAsync(int taken);
    }
}
