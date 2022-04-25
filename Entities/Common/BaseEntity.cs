using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Common
{
    public interface IEntity
    {

    }
   public abstract class BaseEntity<TKey>:IEntity
    {
        public TKey Id { get; set; }
        public bool IsActive { get; set; }
    }
    public abstract class BaseEntity:BaseEntity<long>
    {
    }
}
