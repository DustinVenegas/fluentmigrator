﻿using FluentMigrator.Runner.Generators.MySql;
using Xunit;

namespace FluentMigrator.Tests.Unit.Generators.MySql
{
    public class MySqlIndexTests : BaseIndexTests
    {
        protected MySqlGenerator Generator;

        public MySqlIndexTests()
        {
            Generator = new MySqlGenerator();
        }

        [Fact]
        public override void CanCreateIndexWithCustomSchema()
        {
            var expression = GeneratorTestHelper.GetCreateIndexExpression();
            expression.Index.SchemaName = "TestSchema";

            var result = Generator.Generate(expression);
            result.ShouldBe("CREATE INDEX `TestIndex` ON `TestTable1` (`TestColumn1` ASC)");
        }

        [Fact]
        public override void CanCreateIndexWithDefaultSchema()
        {
            var expression = GeneratorTestHelper.GetCreateIndexExpression();

            var result = Generator.Generate(expression);
            result.ShouldBe("CREATE INDEX `TestIndex` ON `TestTable1` (`TestColumn1` ASC)");
        }

        [Fact]
        public override void CanCreateMultiColumnIndexWithCustomSchema()
        {
            var expression = GeneratorTestHelper.GetCreateMultiColumnCreateIndexExpression();
            expression.Index.SchemaName = "TestSchema";

            var result = Generator.Generate(expression);
            result.ShouldBe("CREATE INDEX `TestIndex` ON `TestTable1` (`TestColumn1` ASC, `TestColumn2` DESC)");
        }

        [Fact]
        public override void CanCreateMultiColumnIndexWithDefaultSchema()
        {
            var expression = GeneratorTestHelper.GetCreateMultiColumnCreateIndexExpression();

            var result = Generator.Generate(expression);
            result.ShouldBe("CREATE INDEX `TestIndex` ON `TestTable1` (`TestColumn1` ASC, `TestColumn2` DESC)");
        }

        [Fact]
        public override void CanCreateMultiColumnUniqueIndexWithCustomSchema()
        {
            var expression = GeneratorTestHelper.GetCreateUniqueMultiColumnIndexExpression();
            expression.Index.SchemaName = "TestSchema";

            var result = Generator.Generate(expression);
            result.ShouldBe("CREATE UNIQUE INDEX `TestIndex` ON `TestTable1` (`TestColumn1` ASC, `TestColumn2` DESC)");
        }

        [Fact]
        public override void CanCreateMultiColumnUniqueIndexWithDefaultSchema()
        {
        }

        [Fact]
        public override void CanCreateUniqueIndexWithCustomSchema()
        {
            var expression = GeneratorTestHelper.GetCreateUniqueIndexExpression();
            expression.Index.SchemaName = "TestSchema";

            var result = Generator.Generate(expression);
            result.ShouldBe("CREATE UNIQUE INDEX `TestIndex` ON `TestTable1` (`TestColumn1` ASC)");
        }

        [Fact]
        public override void CanCreateUniqueIndexWithDefaultSchema()
        {
            var expression = GeneratorTestHelper.GetCreateUniqueIndexExpression();

            var result = Generator.Generate(expression);
            result.ShouldBe("CREATE UNIQUE INDEX `TestIndex` ON `TestTable1` (`TestColumn1` ASC)");
        }

        [Fact]
        public override void CanDropIndexWithCustomSchema()
        {
            var expression = GeneratorTestHelper.GetDeleteIndexExpression();
            expression.Index.SchemaName = "TestSchema";

            var result = Generator.Generate(expression);
            result.ShouldBe("DROP INDEX `TestIndex` ON `TestTable1`");
        }

        [Fact]
        public override void CanDropIndexWithDefaultSchema()
        {
            var expression = GeneratorTestHelper.GetDeleteIndexExpression();

            var result = Generator.Generate(expression);
            result.ShouldBe("DROP INDEX `TestIndex` ON `TestTable1`");
        }
    }
}
