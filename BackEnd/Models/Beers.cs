namespace BackEnd.Models
{
    public class Beers
    {
        public IEnumerable<Beer>? ListBeers { get; set; }
        public int TotalPrice { get; set; }
    }
}
