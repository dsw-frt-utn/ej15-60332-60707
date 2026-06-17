using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Dsw2026Ej15.Domain.Entities
{
    public abstract class BaseEntity
    {
        //[JsonPropertyName("id")]
        public Guid Id { get; }
        protected BaseEntity(Guid? id = null)
        {

            Id = id ?? Guid.NewGuid();
        }

    }
}
