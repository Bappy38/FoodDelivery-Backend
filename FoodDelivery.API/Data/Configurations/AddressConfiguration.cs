using FoodDelivery.API.Constants;
using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FoodDelivery.API.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .Property(a => a.Latitude)
            .IsRequired();

        builder
            .Property(a => a.Longitude)
            .IsRequired();

        var addressesJson = File.ReadAllText(ResourcePaths.Addresses);
        var addresses = JsonConvert.DeserializeObject<List<Address>>(addressesJson);

        builder.HasData(addresses);
    }
}