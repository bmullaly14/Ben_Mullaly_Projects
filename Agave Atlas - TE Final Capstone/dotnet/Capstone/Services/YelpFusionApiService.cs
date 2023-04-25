using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http;
using RestSharp.Authenticators;
using System.Threading;
using System.IO;
using Capstone.Services;
using System;
using Nest;
using Newtonsoft.Json;
using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Services
{
    public class YelpFusionApiService 
    {
        private string apiKey = File.ReadAllText("YelpApiKey.txt");
        public string apiUrl = "https://api.yelp.com/v3/";
        private string connectionString;

        public YelpFusionApiService(string connectionString)
        {
            this.connectionString = connectionString;
        }



        public YelpRestaurant GetRestaurantInfo(NameAndZip nameandzip)
        {
            RestClient client = new RestClient(apiUrl);
            //RestRequest request = new RestRequest("businesses/search?location=44113&term=Barrio&sort_by=best_match&limit=5");
            RestRequest request = new RestRequest($"businesses/search?location={nameandzip.Zipcode}&term={nameandzip.Name}&sort_by=best_match&limit=5");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", apiKey);
            RestResponse response = client.Execute(request);


            string content = response.Content;
            if(content == null)
            {
                return null;
            }

            // Deserialize the JSON data into a C# object
            var result = JsonConvert.DeserializeObject<dynamic>(content);
            List<YelpRestaurant> businesses = new List<YelpRestaurant>();
            if (result.businesses == null)
            {
                return null;
            }
            foreach (var business in result.businesses)
            {
                YelpRestaurant yelp = new YelpRestaurant()
                {
                    Name = business.name,
                    YelpId = business.id,
                    ImageUrl = business.image_url,
                    Address = business.location.address1,
                    City = business.location.city,
                    State = business.location.state,
                    Country = business.location.country,
                    ZipCode = business.location.zip_code,
                    Phone = business.phone,
                    DisplayPhone = business.display_phone,
                    Rating = business.rating,
                    ReviewCount = business.review_count,
                    IsClosed = business.is_closed,
                    YelpUrl = business.url,
                    Latitude = business.coordinates.latitude,
                    Longitude = business.coordinates.longitude
                };
                businesses.Add(yelp);
            }
            if(businesses.Count == 0)
            {
                return null;
            }
            return businesses[0];
        }

    }
}
