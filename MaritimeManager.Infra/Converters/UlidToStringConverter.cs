using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MaritimeManager.Infra.Converters;

public class UlidToStringConverter : ValueConverter<Ulid, string>
{
    private static readonly ConverterMappingHints DefaultHints = new ConverterMappingHints(size: 26);
    
    public UlidToStringConverter() : this(null)
    {
    }

    public UlidToStringConverter(ConverterMappingHints? mappingHints) : base(
        convertToProviderExpression: ulid => ulid.ToString(),
        convertFromProviderExpression: value => Ulid.Parse(value)
    ) {}
}