using Job.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Job.Core.Domain;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;


namespace Job.Data
{
    public class DataContext : DbContext, IDbContext
    {
        public DataContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
           
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users").HasKey(x=>x.Id);
            modelBuilder.Entity<News>().ToTable("News").HasKey(x => x.Id);
            var careerMapping = modelBuilder.Entity<CareerNews>().ToTable("CareerNews").HasKey(x => x.Id);
             careerMapping.HasRequired(x=>x.CareerNewCate).WithMany().HasForeignKey(x=>x.CateId);

             var news_shop_map = modelBuilder.Entity<CareerNews_Shop_Mapping>().ToTable("CareerNews_Shop_Mapping").HasKey(x => x.Id);
             news_shop_map.HasRequired(x => x.CareerNews).WithMany(x => x.CareerNewsShop).HasForeignKey(x => x.NewsId).WillCascadeOnDelete(true);
             news_shop_map.HasRequired(x => x.Shop).WithMany().HasForeignKey(x => x.ShopId).WillCascadeOnDelete(true);             
                        
            modelBuilder.Entity<District>().ToTable("District").HasKey(x => x.Id).HasRequired(x=>x.StateProvice).WithMany().HasForeignKey(x=>x.StateProviceId);
            modelBuilder.Entity<StateProvice>().ToTable("StateProvice").HasKey(x => x.Id);
            modelBuilder.Entity<WorkedCompany>().ToTable("WorkedCompany").HasKey(x => x.Id);
            modelBuilder.Entity<CareerNewCate>().ToTable("CareerNewCate").HasKey(x => x.Id);
            var recuiment = modelBuilder.Entity<Recuitment>().ToTable("Recuitment").HasKey(x => x.Id);
            recuiment.HasRequired(x => x.Cate).WithMany().HasForeignKey(x => x.CateId);
            recuiment.HasRequired(x => x.DistrictModel).WithMany().HasForeignKey(x => x.District);
            modelBuilder.Entity<Shop>().ToTable("Shop").HasKey(x => x.Id).HasRequired(x=>x.District).WithMany().HasForeignKey(x=>x.LocationId);
            base.OnModelCreating(modelBuilder);
        }
      
        public int SaveChanges()
        {
          return  base.SaveChanges();
        }

        public void Dispose()
        {
            base.Dispose();
        }


        /// <summary>
        /// Execute stores procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Entities</returns>
        public IList<T> ExecuteStoredProcedureList<T>(string commandText, params SqlParameter[] parameters) where T:class
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += string.Format("@{0}=@{1}1", p.ParameterName, p.ParameterName);
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                    p.ParameterName = p.ParameterName + "1";
                }
            }
            var result =this.Database.SqlQuery<T>(commandText,parameters).ToList();
          
            ////performance hack applied as described here - http://www.nopcommerce.com/boards/t/25483/fix-very-important-speed-improvement.aspx
            //bool acd = this.Configuration.AutoDetectChangesEnabled;
            //try
            //{
            //    this.Configuration.AutoDetectChangesEnabled = false;

            //    for (int i = 0; i < result.Count; i++)
            //        result[i] = AttachEntityToContext(result[i]);
            //}
            //finally
            //{
            //    this.Configuration.AutoDetectChangesEnabled = acd;
            //}

            return result;
        }

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }
        /// <summary>
        /// Attach an entity to the context or return an already attached entity (if it was already attached)
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>
        protected virtual T AttachEntityToContext<T>(T entity)  where T:class
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
             bool isDetached =(entity as EntityObject).EntityState==System.Data.Entity.EntityState.Detached;
             if (isDetached)
             {
                 //attach new entity
                 Set<T>().Attach(entity);
              }
             return entity;
        }


      public DataTable ExecuteSqlStringWithConnectString(string connectString, string commandText, params object[] parameters)
      {
        using(SqlConnection cnn=new SqlConnection(connectString))
        {
            try
            {
                if (parameters != null && parameters.Length > 0)
                {
                    for (int i = 0; i <= parameters.Length - 1; i++)
                    {
                        var p = parameters[i] as DbParameter;
                        if (p == null)
                            throw new Exception("Not support parameter type");

                        commandText += i == 0 ? " " : ", ";

                        commandText += "@" + p.ParameterName;
                        if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                        {
                            //output parameter
                            commandText += " output";
                        }
                    }
                }
                cnn.Open();
                SqlCommand cmd = new SqlCommand(commandText, cnn);
                var reader =  cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                cnn.Close();
                return dt;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
      }
    }
}
