﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Linq;
using NHibernate.Dialect;
using NHibernate.Linq;
using NHibernate.Transform;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.GH1994
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		protected override void OnSetUp()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				var a = new Asset();
				a.Documents.Add(new Document { IsDeleted = true });
				a.Documents.Add(new Document { IsDeleted = false });

				session.Save(a);
				transaction.Commit();
			}
		}

		protected override void OnTearDown()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				// The HQL delete does all the job inside the database without loading the entities, but it does
				// not handle delete order for avoiding violating constraints if any. Use
				// session.Delete("from System.Object");
				// instead if in need of having NHibernate ordering the deletes, but this will cause
				// loading the entities in the session.

				session.Delete("from System.Object");

				transaction.Commit();
			}
		}

		[Test]
		public async Task TestUnfilteredLinqQueryAsync()
		{
			using (var s = OpenSession())
			{
				var query = await (s.Query<Asset>()
				             .FetchMany(x => x.Documents)
				             .ToListAsync());
				
				Assert.That(query.Count, Is.EqualTo(1), "unfiltered assets");
				Assert.That(query[0].Documents.Count, Is.EqualTo(2), "unfiltered asset documents");
			}
		}

		[Test]
		public async Task TestFilteredByWhereCollectionLinqQueryAsync()
		{
			if(Dialect is PostgreSQLDialect)
				Assert.Ignore("Dialect doesn't support 0/1 to bool implicit cast");

			using (var s = OpenSession())
			{
				var query = await (s.Query<Asset>()
				             .FetchMany(x => x.DocumentsFiltered)
				             .ToListAsync());
				
				Assert.That(query.Count, Is.EqualTo(1), "unfiltered assets");
				Assert.That(query[0].DocumentsFiltered.Count, Is.EqualTo(1), "unfiltered asset documents");
			}
		}

		[Test]
		public async Task TestFilteredLinqQueryAsync()
		{
			using (var s = OpenSession())
			{
				s.EnableFilter("deletedFilter").SetParameter("deletedParam", false);
				var query = await (s.Query<Asset>()
				             .FetchMany(x => x.Documents)
				             .ToListAsync());

				Assert.That(query.Count, Is.EqualTo(1), "filtered assets");
				Assert.That(query[0].Documents.Count, Is.EqualTo(1), "filtered asset documents");
			}
		}

		[Test]
		public async Task TestFilteredQueryOverAsync()
		{
			using (var s = OpenSession())
			{
				s.EnableFilter("deletedFilter").SetParameter("deletedParam", false);

				var query = await (s.QueryOver<Asset>()
				             .Fetch(SelectMode.Fetch, x => x.Documents)
				             .TransformUsing(Transformers.DistinctRootEntity)
				             .ListAsync<Asset>());

				Assert.That(query.Count, Is.EqualTo(1), "filtered assets");
				Assert.That(query[0].Documents.Count, Is.EqualTo(1), "filtered asset documents");
			}
		}
	}
}