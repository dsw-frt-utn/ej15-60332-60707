using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("[api/doctors]")]
    public class DoctorsController : ControllerBase
    {
        //[HttpGet(Name = "PostDoctorsController")]
        //public IEnumerable<DoctorsController> POST()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new DoctorsController
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //.ToArray();
        //}

        //[HttpGet(Name = "GetDoctorsController")]
        //public IEnumerable<DoctorsController> GET()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new DoctorsController
        //    {
        //        Name = "",
        //        licenseNumber = "",
        //        specialityId = ""
        //    })
        //    .ToArray();
        //}

    }
}
