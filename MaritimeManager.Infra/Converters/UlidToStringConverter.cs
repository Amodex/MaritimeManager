using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MaritimeManager.Infra.Converters;

public class UlidToStringConverter : ValueConverter<Ulid, string>
{
    public UlidToStringConverter() : base(
        convertToProviderExpression: ulid => ulid.ToString(),
        convertFromProviderExpression: value => Ulid.Parse(value)
    ) {}
}