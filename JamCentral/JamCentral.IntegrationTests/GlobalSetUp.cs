using JamCentral.Models;
using NUnit.Framework;
using System.Data.Entity.Migrations;

namespace JamCentral.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            var configuration = new JamCentral.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
