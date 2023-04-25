using System.Collections.Generic;

namespace Capstone.Models
{
    public class Restaurant :IReviewable
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string YelpId { get; set; }
        public string Phone { get; set; }
        public string DisplayPhone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public bool IsClosed { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImageUrl { get; set; }
        public string YelpUrl { get; set; }
        

        public IList<Review> Reviews { get; set; }

        //public List<Drink> Drinks { get; set; } - Took out for now since getting a restaurant from the database doesn't include

    }
    public class NewRestaurantInput
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
    }
}
