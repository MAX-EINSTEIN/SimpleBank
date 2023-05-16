using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBank.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Infrastructure.EntityConfigurations
{
    internal class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.Street);
            builder.Property(a => a.City);
            builder.Property(a => a.Region);
            builder.Property(a => a.Country);
            builder.Property(a => a.ZipCode);
        }
    }
}
