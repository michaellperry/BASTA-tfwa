﻿using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Extensions;
using TenantFile.Api.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Tenants
{
    [ExtendObjectType(Name = "Query")]
    public class TenantQueries
    {
        [UseTenantFileContext]
        [UsePaging]
        [HotChocolate.Data.UseFiltering(typeof(TenantFilterInputType))]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Tenant> GetTenants([ScopedService] TenantFileContext tenantContext) => tenantContext.Tenants.AsQueryable();

        public Task<Tenant> GetTenantAsync(int id, TenantByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    }
}