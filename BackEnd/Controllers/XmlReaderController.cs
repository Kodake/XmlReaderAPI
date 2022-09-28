using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml.Linq;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmlReaderController : ControllerBase
    {
        // GET: api/XmlReader
        [HttpGet]
        public ActionResult<XElement?> GetXmlContent()
        {
            var fileName = @"C:\Users\Kodake\Desktop\XmlReaderAPI\Beers.xml";

            XElement storedXML = XElement.Load(fileName);

            var listBeers = from x in storedXML.Element("beers")?.Elements("beer")
                            select new Beer
                            {
                                Name = x.Value,
                                Price = Convert.ToInt32(x.Attribute("price")?.Value),
                                Style = x.Attribute("style")?.Value,
                            };

            Beers beers = new()
            {
                ListBeers = listBeers,
                TotalPrice = listBeers.Select(x => x.Price).Sum()
            };

            return Ok(beers);
        }
    }
}
