using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Unity;
using System.Data.Entity;

namespace HealthCatalyst.Assessment.API.Filters
{
    /// <summary>
    /// Automatically open and close dbConnection with a transaction. 
    /// </summary>
    public class DBTransactionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Creates and manage db sessions.
        /// </summary>
        [Dependency]
        public DbContext DbSession { get; set; }


        /// <summary>
        /// Manage database initiation and transactions for API when a request is received.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (DbSession.Database.Connection.State == System.Data.ConnectionState.Closed)
                DbSession.Database.Connection.Open();

            if (DbSession.Database.CurrentTransaction == null)
            {
                DbSession.Database.BeginTransaction();
            }
        }

        /// <summary>
        /// Manage database initiation and transactions for API when a response is to be sent.
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            DbContextTransaction transaction = null;
            try
            {
                using (transaction = this.DbSession.Database.CurrentTransaction)
                {
                    if (transaction != null)
                    {
                        if (actionExecutedContext.Exception != null)
                        {
                            transaction.Rollback();
                        }
                        else
                        {
                            transaction.Commit();
                        }
                    }
                    else
                    {
                        throw new ApplicationException("No transaction present even though one must be");
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                DbSession.Database.UseTransaction(null);
                if (DbSession.Database.Connection?.State != System.Data.ConnectionState.Closed)
                {
                    DbSession.Database.Connection?.Close();
                }    
            }
        }
    }
}