// Copyright 2009 Josh Close
// This file is a part of FluentDatabase and is licensed under the MS-PL
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html

using System.IO;
using FluentDatabase.Resources;

namespace FluentDatabase.Oracle
{
	public class Column : ColumnBase
	{
		protected override void WriteColumn( StreamWriter writer )
        {
            throw new FluentDatabaseOracleException(Strings.NotImplementedErrorMessage);
        }

		protected override IConstraint CreateConstraint()
		{
			return new Constraint();
		}
	}
}