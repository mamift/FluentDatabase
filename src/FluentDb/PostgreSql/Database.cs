// Copyright 2009 Josh Close
// This file is a part of FluentDatabase and is licensed under the MS-PL
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html

using System.IO;

namespace FluentDb.PostgreSql
{
	/// <summary>
	/// PostgreSQL database.
	/// </summary>
	public class Database : DatabaseBase
	{
		protected override ITable CreateTable()
		{
			return new Table();
		}

		protected override void WriteUse( StreamWriter writer )
		{
		}
	}
}