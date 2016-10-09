using System.Linq;
using System.Collections.Generic;
using System;

using FluentMigrator.Runner.Processors.Oracle;

using Xunit;

namespace FluentMigrator.Tests.Integration.Processors.Oracle {
	[Trait("Category", "Integration")]
	public class OracleManagedTableTests : OracleTableTestsBase {
		public OracleManagedTableTests()
            : base(new OracleManagedDbFactory())
        {
		}
	}
}