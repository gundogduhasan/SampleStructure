global using System;

namespace Common.Entites
{
    public class BaseEntity : BaseEntity<Guid> { }

    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }

}
