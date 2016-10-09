using System.Data.SqlClient;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Generators.Hana;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.Hana;
using Xunit;
using Sap.Data.Hana;

namespace FluentMigrator.Tests.Integration.Processors.Hana
{
    [Trait("Category", "Integration")]
    public class HanaSchemaTests : BaseSchemaTests
    {
        public HanaConnection Connection { get; set; }
        public HanaProcessor Processor { get; set; }

        public void SetUp()
        {
            Connection = new HanaConnection(IntegrationTestOptions.Hana.ConnectionString);
            Processor = new HanaProcessor(Connection, new HanaGenerator(), new TextWriterAnnouncer(System.Console.Out), new ProcessorOptions(), new HanaDbFactory());
            Connection.Open();
            Processor.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            Processor.CommitTransaction();
            Processor.Dispose();
        }

        [Fact]
        public override void CallingSchemaExistsReturnsFalseIfSchemaDoesNotExist()
        {
            Assert.Ignore("HANA does not support schema like us know schema in hana is a database name");

            Processor.SchemaExists("DoesNotExist").ShouldBeFalse();
        }

        [Fact]
        public override void CallingSchemaExistsReturnsTrueIfSchemaExists()
        {
            Assert.Ignore("HANA does not support schema like us know schema in hana is a database name");

            Processor.SchemaExists("dbo").ShouldBeTrue();
        }
    }
}