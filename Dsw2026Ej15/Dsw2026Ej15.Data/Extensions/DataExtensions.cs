
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data.Extensions
{
    public static class DataExtensions
    {
        public static void SeedWorkSpecialities(this Dsw2026Ej15DbContext context, string dataSourse)
        {
            if (context.Set<Speciality>().Any()) return;
            {

                    string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", dataSourse);
                    var json = File.ReadAllText(jsonPath);
                    var especialidadesData = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
                        new JsonSerializerOptions()
                        {
                            PropertyNameCaseInsensitive = true
                        }) ?? [];

                    var specialities = especialidadesData.Select(s => new Speciality(s.Name, s.Description, s.Id));
                    
                    context.Set<Speciality>().AddRange(specialities);
                    context.SaveChanges();

            }
    }
}
}
