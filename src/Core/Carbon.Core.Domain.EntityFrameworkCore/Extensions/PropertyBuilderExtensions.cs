using Carbon.Core.Domain.Models.Base;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carbon.Core.Domain.EntityFrameworkCore.Extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TWrapper> IsValueObjectWrapper<TBase, TWrapper>(this PropertyBuilder<TWrapper> propertyBuilder)
        where TWrapper : ValueObjectWrapper<TBase, TWrapper>, new()
        where TBase : notnull
    {
        propertyBuilder.HasConversion(
            valueObject => valueObject.Value,
            value => ValueObjectWrapper<TBase, TWrapper>.Create(value));

        return propertyBuilder;
    }
}