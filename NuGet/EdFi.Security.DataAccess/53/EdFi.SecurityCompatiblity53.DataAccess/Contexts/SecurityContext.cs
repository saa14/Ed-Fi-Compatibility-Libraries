﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System.Data.Entity;
using EdFi.SecurityCompatiblity53.DataAccess.Models;
using EdFi.SecurityCompatiblity53.DataAccess.Utils;

namespace EdFi.SecurityCompatiblity53.DataAccess.Contexts
{
    public abstract class SecurityContext : DbContext, ISecurityContext
    {
        protected SecurityContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new ValidateDatabase<SecurityContext>());
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Action> Actions { get; set; }

        public DbSet<AuthorizationStrategy> AuthorizationStrategies { get; set; }

        public DbSet<ClaimSet> ClaimSets { get; set; }

        public DbSet<ClaimSetResourceClaim> ClaimSetResourceClaims { get; set; }

        public DbSet<ResourceClaim> ResourceClaims { get; set; }

        public DbSet<ResourceClaimAuthorizationMetadata> ResourceClaimAuthorizationMetadatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceClaim>()
                .HasOptional(rc => rc.ParentResourceClaim)
                .WithMany()
                .HasForeignKey(fk => fk.ParentResourceClaimId);
        }
    }
}
