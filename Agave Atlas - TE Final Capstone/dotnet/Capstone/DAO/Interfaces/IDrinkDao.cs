using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO.Interfaces
{
    public interface IDrinkDao
    {
        IList<Drink> GetAllDrinks();

        IList<Drink> GetRestaurantDrinks(int restID);

        Drink GetDrinkById(int drinkID);

        Drink AddDrink(Drink drink);

        Drink UpdateDrink(int drinkID, Drink newDrink);

        bool DeleteDrink(int drinkID);
    }

}
