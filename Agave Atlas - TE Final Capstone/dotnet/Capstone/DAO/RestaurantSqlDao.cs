using Capstone.DAO.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;

namespace Capstone.DAO
{
    public class RestaurantSqlDao : IRestaurantSqlDao
    {

        private readonly string connectionString;

        public RestaurantSqlDao(string connString)
        { connectionString = connString; }



        public IList<Restaurant> GetAllRestaurants()
        {
            IList<Restaurant> allRestaurants = new List<Restaurant>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants ORDER BY rating DESC", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Restaurant restaurant = CreateRestaurantFromReader(reader);
                        allRestaurants.Add(restaurant);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error getting restaurants");
            }
            return allRestaurants;
        }
        public IList<Restaurant> GetRestaurantsByZipCode(string zipcode)
        {
            IList<Restaurant> restaurants = new List<Restaurant>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT * FROM restaurants " +
                        "WHERE zip_code = @zip_code", conn);
                    cmd.Parameters.AddWithValue("@zip_code", zipcode);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Restaurant restaurant = CreateRestaurantFromReader(reader);
                        restaurants.Add(restaurant);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error getting restaurants by zipcode");
            }
            return restaurants;

        }

        public Restaurant GetRestaurantByID(int ID)
        {
            Restaurant restaurant = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE restaurant_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", ID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                       restaurant = CreateRestaurantFromReader(reader);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error getting restaurant by restaurant ID");
            }
            return restaurant;
        }

        public Restaurant AddRestaurant(YelpRestaurant inputRestaurant)
        {
            Restaurant restaurant = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO restaurants " +
                        "(name, zip_code, yelp_id, phone, display_phone, address, city, state, country, rating, review_count, is_closed, latitude, longitude, image_url, yelp_url ) " + 
                        "OUTPUT INSERTED.restaurant_id " +
                        "VALUES (@name, @zip_code, @yelp_id, @phone, @display_phone, @address, @city, @state, @country, @rating, @review_count, @is_closed, @latitude, @longitude, @image_url, @yelp_url ) ;", conn);
                    cmd.Parameters.AddWithValue("@name", inputRestaurant.Name);
                    cmd.Parameters.AddWithValue("@zip_code", inputRestaurant.ZipCode);
                    cmd.Parameters.AddWithValue("@yelp_id", inputRestaurant.YelpId);
                    cmd.Parameters.AddWithValue("@phone", inputRestaurant.Phone);
                    cmd.Parameters.AddWithValue("@display_phone", inputRestaurant.DisplayPhone);
                    cmd.Parameters.AddWithValue("@address", inputRestaurant.Address);
                    cmd.Parameters.AddWithValue("@city", inputRestaurant.City);
                    cmd.Parameters.AddWithValue("@state", inputRestaurant.State);
                    cmd.Parameters.AddWithValue("@country", inputRestaurant.Country);
                    cmd.Parameters.AddWithValue("@rating", inputRestaurant.Rating);
                    cmd.Parameters.AddWithValue("@review_count", inputRestaurant.ReviewCount);
                    cmd.Parameters.AddWithValue("@is_closed", inputRestaurant.IsClosed);
                    cmd.Parameters.AddWithValue("@latitude", inputRestaurant.Latitude);
                    cmd.Parameters.AddWithValue("@longitude", inputRestaurant.Longitude);
                    cmd.Parameters.AddWithValue("@image_url", inputRestaurant.ImageUrl);
                    cmd.Parameters.AddWithValue("@yelp_url", inputRestaurant.YelpUrl);


                    int restaurantID = Convert.ToInt32(cmd.ExecuteScalar());
                    restaurant = GetRestaurantByID(restaurantID);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error adding new restaurant to server");
            }
            return restaurant;
        }


        public Restaurant UpdateRestaurant(Restaurant inputRestaurant)
        {
            Restaurant restaurant = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("" +
                        "UPDATE restaurants " +
                        "SET name = @name, zip_code = @zip_code, yelp_id = @yelp_id, phone = @phone, " +
                        "display_phone = @display_phone, address = @address, city = @city, state = @state, country = @country, " +
                        "rating = @rating, review_count = @review_count, is_closed = @is_closed  " +
                        "latitude = @latitude, longitude = @longitude, image_url = @image_url, yelp_url = @yelp_url " +
                        "WHERE restaurant_id = @restaurant_id;"
                        ,conn);
                    cmd.Parameters.AddWithValue("@restaurant_id", inputRestaurant.ID);

                    cmd.Parameters.AddWithValue("@name", inputRestaurant.Name);
                    cmd.Parameters.AddWithValue("@zip_code", inputRestaurant.ZipCode);
                    cmd.Parameters.AddWithValue("@yelp_id", inputRestaurant.YelpId);
                    cmd.Parameters.AddWithValue("@phone", inputRestaurant.Phone);
                    cmd.Parameters.AddWithValue("@display_phone", inputRestaurant.DisplayPhone);
                    cmd.Parameters.AddWithValue("@address", inputRestaurant.Address);
                    cmd.Parameters.AddWithValue("@city", inputRestaurant.City);
                    cmd.Parameters.AddWithValue("@state", inputRestaurant.State);
                    cmd.Parameters.AddWithValue("@country", inputRestaurant.Country);
                    cmd.Parameters.AddWithValue("@rating", inputRestaurant.Rating);
                    cmd.Parameters.AddWithValue("@review_count", inputRestaurant.ReviewCount);
                    cmd.Parameters.AddWithValue("@is_closed", inputRestaurant.IsClosed);
                    cmd.Parameters.AddWithValue("@latitude", inputRestaurant.Latitude);
                    cmd.Parameters.AddWithValue("@longitude", inputRestaurant.Longitude);
                    cmd.Parameters.AddWithValue("@image_url", inputRestaurant.ImageUrl);
                    cmd.Parameters.AddWithValue("@yelp_url", inputRestaurant.YelpUrl);

                    cmd.ExecuteNonQuery();
                    restaurant = GetRestaurantByID(inputRestaurant.ID);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error updating restaurant on the server");
            }
            return restaurant;
        }


        public void DeleteRestaurant(int restaurantID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"DELETE FROM restaurant_drinks WHERE restaurant_id = @restaurant_id; 
                                                    DELETE FROM restaurants WHERE restaurant_id = @restaurant_id;"
                        , conn);
                    cmd.Parameters.AddWithValue("@restaurant_id", restaurantID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error updating restaurant on the server");
            }
        }

        public Restaurant CreateRestaurantFromReader(SqlDataReader sdr)
    {
        Restaurant restaurant = new Restaurant()
        {
            ID = Convert.ToInt32(sdr["restaurant_id"]),
            Name = Convert.ToString(sdr["name"]),
            ZipCode = Convert.ToString(sdr["zip_code"]),
            YelpId = Convert.ToString(sdr["yelp_id"]),
            Phone = Convert.ToString(sdr["phone"]),
            DisplayPhone = Convert.ToString(sdr["display_phone"]),
            Address = Convert.ToString(sdr["address"]),
            City = Convert.ToString(sdr["city"]),
            State = Convert.ToString(sdr["state"]),
            Country = Convert.ToString(sdr["country"]),
            Rating = Convert.ToDouble(sdr["rating"]),
            ReviewCount = Convert.ToInt32(sdr["review_count"]),
            IsClosed = Convert.ToBoolean(sdr["is_closed"]),
            Latitude = Convert.ToDouble(sdr["latitude"]),
            Longitude = Convert.ToDouble(sdr["longitude"]),
            ImageUrl = Convert.ToString(sdr["image_url"]),
            YelpUrl = Convert.ToString(sdr["yelp_url"])
        };
        return restaurant;
    }

    }
}
