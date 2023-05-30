namespace Athena.DataAccess.Extensions;

public static class ObjectExtensions
{
    public static void UpdateModifiedFields<T, TDto>(ref T entity, TDto dto) where T : class where TDto : class
    {
        foreach (var prop in entity.GetType().GetProperties())
        {
            if (dto.GetType().GetProperty(prop.Name)?.GetValue(dto) is not null)
            {
                prop.SetValue(entity, dto.GetType().GetProperty(prop.Name)?.GetValue(dto));
            }
        }
    }
}