using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmlReaderController : ControllerBase
    {
        public List<string> errors = new();

        // GET: api/XmlReader
        [HttpGet]
        public ActionResult<XElement?> GetXmlContent()
        {
            var fileName = @"C:\Users\Kodake\Desktop\XmlReaderAPI\BackEnd\BackEnd\Beers.xml";

            XElement storedXML = XElement.Load(fileName);

            var listBeers = from x in storedXML.Element("beers")?.Elements("beer")
                            select new Beer
                            {
                                Name = x.Value,
                                Price = Convert.ToInt32(x.Attribute("price")?.Value),
                                Style = x.Attribute("style")?.Value,
                            };

            ValidateNodes(listBeers);

            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }

            Beers beers = new()
            {
                ListBeers = listBeers,
                TotalPrice = listBeers.Select(x => x.Price).Sum()
            };

            return Ok(beers);
        }

        private void ValidateNodes(IEnumerable<Beer> listBeers)
        {
            if (listBeers.Any(x => string.IsNullOrEmpty(x.Name)))
            {
                errors.Add("A name for the beer cannot be null or empty");
            }

            if (listBeers.Any(x => x.Price == 0))
            {
                errors.Add("A price for the beer cannot be zero");
            }

            if (listBeers.Any(x => string.IsNullOrEmpty(x.Style)))
            {
                errors.Add("An style beer cannot be null or empty");
            }
        }
    }
}
