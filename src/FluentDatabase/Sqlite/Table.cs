﻿// Copyright 2009 Josh Close
// This file is a part of FluentDatabase and is licensed under the MS-PL
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html

using System.IO;

namespace FluentDatabase.Sqlite
{
	/// <summary>
	/// SQLite table.
	/// </summary>
	public class Table : TableBase
	{
		protected override void WriteTableBegin( StreamWriter writer )
		{
			if( string.IsNullOrEmpty( Name ) )
			{
				throw new FluentDatabaseSqliteException( Resources.Strings.TableNameEmptyErrorMessage );
			}

			writer.WriteLine( "CREATE TABLE {0}", Name );
			writer.WriteLine( "(" );
		}

		protected override void WriteTableEnd( StreamWriter writer )
		{
			writer.WriteLine( ");" );
		}

		protected override IColumn CreateColumn()
		{
			return new Column();
		}
	}
}
