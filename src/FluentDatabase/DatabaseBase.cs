// Copyright 2009 Josh Close
// This file is a part of FluentDatabase and is licensed under the MS-PL
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html

using System;
using System.Collections.Generic;
using System.IO;

namespace FluentDatabase
{
	/// <summary>
	/// Base class for creating a database. Use this instead
	/// of implementing <see cref="IDatabase"/> directly.
	/// </summary>
	public abstract class DatabaseBase : IDatabase
	{
		public string Name { get; set; }
		public string Schema { get; set; } = "dbo";
		public IList<ITable> Tables { get; set; } = new List<ITable>();

		protected abstract ITable CreateTable();
		protected abstract void WriteUse(StreamWriter writer);

		protected DatabaseBase() { }

		public IDatabase WithName(string name)
		{
			Name = name;
			return this;
		}

		public IDatabase UsingSchema(string schema)
		{
			Schema = schema;
			return this;
		}

		public IDatabase HasTable(Action<ITable> table)
		{
			ITable newTable = CreateTable();
			table.Invoke(newTable);
			Tables.Add(newTable);
			return this;
		}

		public void Write(StreamWriter writer)
		{
			WriteUse(writer);
			writer.WriteLine();
			foreach (ITable table in Tables) {
				table.UsingSchema(Schema);
				table.Write(writer);
				writer.WriteLine();
			}
		}
	}
}