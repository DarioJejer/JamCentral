using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JamCentral.Tests.Extensions
{
    public static class MockDbSetExtensions
    {
        public static void PopulateGigs<T> (this Mock<DbSet<T>> mockSet, IList<T> list) where T : class
        {
            var queryable = list.AsQueryable();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        }
    }
}
