using System;
using System.Data;
using System.IO;
using FluentDatabase;
using NUnit.Framework;

namespace FluentDb.Tests
{
	[TestFixture]
	public class DatabaseTest
	{
		[Test]
		public void Test()
		{
			foreach (DatabaseType databaseType in Enum.GetValues(typeof(DatabaseType))) {
				var fileName = $"{Environment.CurrentDirectory}\\{databaseType}.sql";
				using (var stream = File.OpenWrite(fileName))
				using (var writer = new StreamWriter(stream)) {
					DatabaseFactory.Create(databaseType)
						.WithName("Business")
						.UsingSchema("Test")
						.HasTable(
							table => table
								.WithName("Companies")
								.HasColumn(
									column => column.WithName("Id").OfType(SqlDbType.Int).IsAutoIncrementing()
										.HasConstraint(constraint => constraint.OfType(ConstraintType.NotNull))
										.HasConstraint(
											constraint =>
												constraint.OfType(ConstraintType.PrimaryKey)
													.WithName("PK_Companies_Id"))
								)
								.HasColumn(
									column => column.WithName("Name").OfType(SqlDbType.NVarChar, 100)
										.HasConstraint(constraint => constraint.OfType(ConstraintType.NotNull))
								)
						)
						.HasTable(
							table => table
								.WithName("Employees")
								.HasColumn(
									column => column.WithName("Id").OfType(SqlDbType.Int).IsAutoIncrementing()
										.HasConstraint(constraint => constraint.OfType(ConstraintType.NotNull))
										.HasConstraint(
											constraint =>
												constraint.OfType(ConstraintType.PrimaryKey)
													.WithName("PK_Employees_Id"))
								)
								.HasColumn(
									column => column.WithName("CompanyId").OfType(SqlDbType.Int)
										.HasConstraint(constraint => constraint.OfType(ConstraintType.NotNull))
										.HasConstraint(
											constraint =>
												constraint.WithName("FK_Employees_CompanyId")
													.OfType(ConstraintType.ForeignKey)
													.HasReferenceTo("Companies", "Id"))
								)
								.HasColumn(
									column => column.WithName("Name").OfType(SqlDbType.NVarChar, 50)
										.HasConstraint(constraint => constraint.OfType(ConstraintType.NotNull))
								)
								.HasColumn(
									column => column.WithName("Bio").OfType(SqlDbType.NVarChar, ColumnSize.Max)
								)
						).Write(writer);
				}
			}
		}
	}
}