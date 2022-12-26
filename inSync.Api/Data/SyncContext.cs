using System;
using inSync.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace inSync.Api.Data
{
	public class SyncContext : DbContext
	{
		public SyncContext()
		{
		}

		public SyncContext(DbContextOptions options) : base(options)
		{
			
		}

		public virtual DbSet<Item> Items { get; set; }
		public virtual DbSet<ItemList> ItemLists { get; set; }
	}
}

