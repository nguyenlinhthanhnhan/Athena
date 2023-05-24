﻿using System.Reflection;
using AutoMapper;

namespace Athena.Application.Commons.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile() => ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes().Where(x =>
            x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IMapFrom<>))).ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping") ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}