using FluentMigrator.Runner.Generators;
using FluentMigrator.Runner.Processors.Oracle;

using Xunit;

namespace FluentMigrator.Tests.Integration.Processors.Oracle
{
	[Trait("Category", "Integration")]
	public class OracleConstraintTests : OracleConstraintTestsBase {
		[SetUp]
		public void SetUp( ) {
			base.SetUp( new OracleDbFactory() );
		}
	}
}
