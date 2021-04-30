using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.Models;
using TenantFile.Api.Models.Entities;
using Xunit;
using FluentAssertions;

namespace Api.IntegrationTests
{
    public class AddressIntegrationTests
    {
        [Fact]
        public async Task CanSaveAddress()
        {
            using var dbContext = new TenantFileContext(new DbContextOptionsBuilder<TenantFileContext>()
                .UseNpgsql("Server=127.0.0.1; Port=5432; User Id=postgres; Password=example")
                .Options
            );

            var address = new Address
            {
                Line1 = "1234 Main",
                City = "Anytown",
                State = "Texas",
                PostalCode = "12345"
            };

            using var transaction = dbContext.Database.BeginTransaction();

            await dbContext.AddAsync(address);
            await dbContext.SaveChangesAsync();

            var actual = (dbContext.Addresses as IQueryable<Address>)
                .Where(a => a.PostalCode == "12345");
            var count = await actual.CountAsync();

            count.Should().Be(1);
        }
    }
}
