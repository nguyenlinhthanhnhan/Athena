using System.ComponentModel.DataAnnotations;

namespace Athena.Core.Common;

/// <summary>
/// Base class for all entities
/// </summary>
/// <typeparam name="T">Type of id</typeparam>
public abstract class Entity<T> where T:struct
{
    [Key]
    public T Id { get; set; }
}