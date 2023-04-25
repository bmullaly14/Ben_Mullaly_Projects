using Capstone.DAO.Interfaces;
using System.Collections.Generic;
using Capstone.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Capstone.DAO
{
    public class DrinkSqlDao : IDrinkDao
    {
        private readonly string connectionString;

        public DrinkSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Drink> GetAllDrinks()
        {
            IList<Drink> allDrinks = new List<Drink>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT drinks.drink_id, drink_name, description, isFrozen, name, restaurants.restaurant_id FROM drinks
                                                    JOIN restaurant_drinks ON restaurant_drinks.drink_id = drinks.drink_id
                                                    JOIN restaurants ON restaurants.restaurant_id = restaurant_drinks.restaurant_id;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Drink drink = CreateDrinkFromReader(reader);
                        allDrinks.Add(drink);
                    }
                }
            }
            catch(SqlException)
            {
                Console.WriteLine("Error getting drinks");
            }
            return allDrinks;
        }

        public IList<Drink> GetRestaurantDrinks(int restID)
        {
            IList<Drink> drinks = new List<Drink>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($@"SELECT drinks.drink_id, drink_name, description, isFrozen, name, restaurants.restaurant_id FROM drinks
                                                    JOIN restaurant_drinks ON restaurant_drinks.drink_id = drinks.drink_id
                                                    JOIN restaurants ON restaurants.restaurant_id = restaurant_drinks.restaurant_id
                    WHERE restaurants.restaurant_id = @restID",conn);
                    cmd.Parameters.AddWithValue("@restID", restID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Drink newDrink = CreateDrinkFromReader(reader);
                        drinks.Add(newDrink);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error getting drinks for restaurant");
            }
            return drinks;
        }

        public Drink GetDrinkById(int drinkID) {
            Drink drink = new Drink();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($@"SELECT * FROM drinks
                    WHERE drinks.drink_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", drinkID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        drink = CreateDrinkFromReader(reader);
                    }
                    
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error getting drink by ID");
            }
            return drink;
        }

        public Drink AddDrink(Drink newDrink)
        {
            Drink drink = null;
            try

            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                                                  INSERT INTO drinks (drink_name, description, isFrozen) 
                                                  OUTPUT INSERTED.drink_id 
                                                  VALUES (@drink_name, @description, @isFrozen) 

                                                  ;", conn);
                    cmd.Parameters.AddWithValue("@drink_name", newDrink.Name);
                    cmd.Parameters.AddWithValue("@description", newDrink.Description);
                    cmd.Parameters.AddWithValue("@isFrozen", newDrink.IsFrozen);
                   //cmd.Parameters.AddWithValue("@restaurant_id", newDrink.RestaurantID);

                    int drinkId = Convert.ToInt32(cmd.ExecuteScalar());
                    drink = GetDrinkById(drinkId);

          
                    SqlCommand comd = new SqlCommand(@"INSERT INTO restaurant_drinks (drink_id , restaurant_id)   
                                                      VALUES (@drink_id, @restaurant_id);", conn);

                    comd.Parameters.AddWithValue("@restaurant_id", newDrink.RestaurantID);
                    comd.Parameters.AddWithValue("@drink_id", drinkId);
                    comd.ExecuteNonQuery();
                }
            }
            catch (Exception) { Console.WriteLine("error adding new drink");}
            return drink;
        }



        public Drink UpdateDrink(int drinkID,Drink newDrink)
        {
            Drink drink = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE drinks 
                                                      SET drink_name = @drink_name, description = @description, isFrozen = @isFrozen 
                                                      WHERE drink_id = @drink_id",conn);
                    cmd.Parameters.AddWithValue("@drink_name",newDrink.Name);
                    cmd.Parameters.AddWithValue("@description", newDrink.Description);
                    cmd.Parameters.AddWithValue("@isFrozen", newDrink.IsFrozen);
                    cmd.Parameters.AddWithValue("@drink_id", drinkID);

                    cmd.ExecuteNonQuery();
                    drink = GetDrinkById(drinkID);

                }
            }
            catch (Exception) { Console.WriteLine("Error updating drink"); }
            return drink;
        }

        public bool DeleteDrink(int drinkID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"BEGIN TRANSACTION
                                                      DELETE from restaurant_drinks where drink_id = @drink_id; 
                                                      DELETE FROM drinks WHERE drink_id = @drink_id; 
                                                      COMMIT;"
                        , conn);
                    cmd.Parameters.AddWithValue("@drink_id", drinkID);
                    cmd.ExecuteNonQuery();
                   
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error updating drink on the server");
            }
            return true;
        }


        private Drink CreateDrinkFromReader(SqlDataReader reader)
        {
            Drink newDrink = new Drink();

            newDrink.DrinkID = Convert.ToInt32(reader["drink_id"]);
            newDrink.Name = Convert.ToString(reader["drink_name"]);
            newDrink.Description = Convert.ToString(reader["description"]);
            newDrink.IsFrozen = Convert.ToBoolean(reader["isFrozen"]);
            newDrink.RestaurantID = Convert.ToInt32(reader["restaurant_id"]);
            newDrink.RestaurantName = Convert.ToString(reader["name"]);

            return newDrink;

        }
    }
}
