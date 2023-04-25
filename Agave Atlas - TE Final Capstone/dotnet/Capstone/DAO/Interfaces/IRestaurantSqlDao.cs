using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IRestaurantSqlDao
    {
        IList<Restaurant> GetAllRestaurants();
        IList<Restaurant> GetRestaurantsByZipCode(string zipcode);
        Restaurant GetRestaurantByID(int ID);
        Restaurant AddRestaurant(YelpRestaurant inputRestaurant);
        Restaurant UpdateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(int restaurantID);

    }
}
