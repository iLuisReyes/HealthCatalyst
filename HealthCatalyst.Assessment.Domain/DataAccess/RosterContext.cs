using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using HealthCatalyst.Assessment.Domain.Models;

namespace HealthCatalyst.Assessment.Domain.DataAccess
{
    internal class RosterContext : DbContext
    {
        /// <summary>
        /// Parameterless constructor for unit tests
        /// </summary>
        protected RosterContext() { }

        /// <summary>
        /// Default Constructor for dependency injection
        /// </summary>
        public RosterContext(string connectionStringName) : base(connectionStringName) { }

        /// <summary>
        /// The Team database set (table)
        /// </summary>
        public virtual DbSet<Teammate> Team { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
