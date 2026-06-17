using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Dsw2026Ej15.Domain.Interfaces;
namespace Dsw2026Ej15.Domain.Entities
{
    public class Speciality : BaseEntity
    {
        //[JsonPropertyName("name")]
        public string Name { get; set; }

        //[JsonPropertyName("description")]
        public string Description { get; set; }
        public Speciality(string name, string description, Guid? id = null) : base(id)
        {
            Name = name;
            Description = description;
        }
        
    }
}
