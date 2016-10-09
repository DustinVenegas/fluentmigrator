using System;
using System.IO;
using FluentMigrator.Builders.Execute;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Generators.MySql;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.MySql;
using MySql.Data.MySqlClient;
using Xunit;

namespace FluentMigrator.Tests.Integration.Processors.MySql
{
    [Trait("Category", "Integration")]
    public class MySqlProcessorTests
    {
        protected MySqlProcessor Processor;

        [CLSCompliant(false)]
        protected MySqlConnection Connection;

        public void SetUp()
        {
            Connection = new MySqlConnection(IntegrationTestOptions.MySql.ConnectionString);
            Processor = new MySqlProcessor(Connection, new MySqlGenerator(), new TextWriterAnnouncer(System.Console.Out), new ProcessorOptions(), new MySqlDbFactory());
            Connection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            Processor.Dispose();
        }

        [Fact]
        public void CallingProcessWithPerformDBOperationExpressionWhenInPreviewOnlyModeWillNotMakeDbChanges()
        {
            var output = new StringWriter();

            var connection = new MySqlConnection(IntegrationTestOptions.MySql.ConnectionString);

            var processor = SetupMySqlProcessorWithPreviewOnly(output, connection);

            bool tableExists;

            try
            {
                var expression =
                    new PerformDBOperationExpression
                    {
                        Operation = (con, trans) =>
                        {
                            var command = con.CreateCommand();
                            command.CommandText = "CREATE TABLE processtesttable (test int NULL) ";
                            command.Transaction = trans;

                            command.ExecuteNonQuery();
                        }
                    };

                processor.Process(expression);

                var com = connection.CreateCommand();
                com.CommandText = "";

                tableExists = processor.TableExists("", "processtesttable");
            }
            finally
            {
                processor.RollbackTransaction();
            }

            tableExists.ShouldBeFalse();
        }

        [Fact]
        public void CallingExecuteWithPerformDBOperationExpressionWhenInPreviewOnlyModeWillNotMakeDbChanges()
        {
            var output = new StringWriter();

            var connection = new MySqlConnection(IntegrationTestOptions.MySql.ConnectionString);

            var processor = SetupMySqlProcessorWithPreviewOnly(output, connection);

            bool tableExists;

            try
            {
                processor.Execute("CREATE TABLE processtesttable (test int NULL) ");

                tableExists = processor.TableExists("", "processtesttable");
            }
            finally
            {
                processor.RollbackTransaction();
            }

            tableExists.ShouldBeFalse();
        }

        [Fact]
        public void CallingDefaultValueExistsReturnsTrueWhenMatches()
        {
            try
            {
                Processor.Execute("CREATE TABLE dftesttable (test int NULL DEFAULT 1) ");
                Processor.DefaultValueExists(null, "dftesttable", "test", 1).ShouldBeTrue();
            }
            finally
            {
                Processor.Execute("DROP TABLE dftesttable");
            }
        }

        [Fact]
        public void CallingReadTableDataQuotesTableName()
        {
            try
            {
                Processor.Execute("CREATE TABLE `infrastructure.version` (test int null) ");
                Processor.ReadTableData(null, "infrastructure.version");
            }
            finally
            {
                Processor.Execute("DROP TABLE `infrastructure.version`");
            }
        }

        private static MySqlProcessor SetupMySqlProcessorWithPreviewOnly(StringWriter output, MySqlConnection connection)
        {
            var processor = new MySqlProcessor(
                connection,
                new MySqlGenerator(),
                new TextWriterAnnouncer(output),
                new ProcessorOptions {PreviewOnly = true},
                new MySqlDbFactory());
            return processor;
        }
    }
}