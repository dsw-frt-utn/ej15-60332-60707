using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public abstract class BaseEntity
    {
        private readonly Guid _id;

        public BaseEntity()
        {
            _id = Guid.NewGuid();
        }

    }
}
