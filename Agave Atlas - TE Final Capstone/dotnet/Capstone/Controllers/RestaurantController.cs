using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Capstone.Models;
using Capstone.Security;
using Capstone.DAO.Interfaces;
using System.Collections.Generic;
using Capstone.DAO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Newtonsoft.Json;
using System;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantSqlDao restaurantDao;
        private readonly IUserDao userDao;
        private readonly IDrinkDao drinkDao;
        private readonly IReviewDao reviewDao;

        public RestaurantController(IRestaurantSqlDao restaurantDao, IUserDao userDao, IDrinkDao drinkDao, IReviewDao reviewDao)
        {
            this.restaurantDao = restaurantDao;
            this.userDao = userDao;
            this.drinkDao = drinkDao;
            this.reviewDao = reviewDao;
        }

        [HttpGet]
        public ActionResult<IList<Restaurant>> GetAllRestaurants()
        {
            IList<Restaurant> restaurants = restaurantDao.GetAllRestaurants();

            if (restaurants.Count == 0)
            {
                return NoContent();
            }
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetRestaurantById(int id)
        {
            Restaurant restaurant = restaurantDao.GetRestaurantByID(id);
            if (restaurant == null)
            {
                return NoContent();
            }
            return Ok(restaurant);
        }
        [HttpGet("{id}/reviews")]
        public ActionResult<IList<Review>> GetReviewsForRestaurant(int id)
        {
            IList<Review> reviews = reviewDao.GetRestaurantReviews(id);

            if(reviews.Count == 0)
            {
                return NoContent();
            } else
            {
                return Ok(reviews);
            }
        }
        [HttpPost("{id}/reviews")]
        [AllowAnonymous]
        public ActionResult<Review> AddReviewToRestaurant(int id, [FromBody]Review review)
        {
            if(GetRestaurantById(id) == null)
            {
                return NotFound();
            }

           review.Reviewable_ID = Convert.ToString(id);
           Review rev = reviewDao.AddRestaurantReview(review);
            if(rev == null)
            {
                return NoContent();
            } else
            {
                return Ok(rev);
            }
            
        }
        [HttpDelete("{id}/reviews/{review_id}")]
        public ActionResult DeleteReview(int id, int review_id)
        {
            if(reviewDao.GetReviewByID(review_id) == null)
            {
                return NotFound();
            }
            if (reviewDao.DeleteRestaurantReview(review_id))
            {
                return Ok();
            } else { return NotFound(); }

        }
        [HttpGet("zipcode/{zip_code}")]
        public ActionResult<IList<Restaurant>> GetRestaurantByZipcode(string zip_code)
        {
            IList<Restaurant> restaurants = restaurantDao.GetRestaurantsByZipCode(zip_code);
            if (restaurants.Count == 0)
            {
                return NoContent();
            }
            return Ok(restaurants);
        }

        [HttpGet("{id}/drinks")]
        public ActionResult<IList<Drink>> GetDrinksForRestaurant(int id)
        {
            IList<Drink> drinks = drinkDao.GetRestaurantDrinks(id);

            if (drinks.Count == 0)
            {
                return NoContent();
            }
            return Ok(drinks);
        }

        [HttpPost("new")]
        public ActionResult<Restaurant> AddRestaurantToDB(YelpRestaurant newRestaurantInput)
        {
            Restaurant restaurant = restaurantDao.AddRestaurant(newRestaurantInput);
            if (restaurant == null)
            {
                return NoContent();
            }
            return restaurant;
        }

        [HttpPut("{Restaurant_ID}")]
        public ActionResult<Restaurant> UpdateRestaurant(Restaurant inputRestaurant)
        {
            if (GetRestaurantById(inputRestaurant.ID) == null)
            {
                return NotFound();
            }
            else
            {
                Restaurant restaurant = restaurantDao.UpdateRestaurant(inputRestaurant);
                if(restaurant == null)
                {
                    return NoContent();
                }
                return Ok(restaurant);
            }
        }
        
        [HttpDelete("{restaurantID}")]
        public ActionResult DeleteRestaurantFromDB(int restaurantID)
        {
            Restaurant checkDelete = restaurantDao.GetRestaurantByID(restaurantID);
            if (checkDelete == null)
            {
                return NotFound();
            }
            else
            {
                restaurantDao.DeleteRestaurant(restaurantID);
                checkDelete = restaurantDao.GetRestaurantByID(restaurantID);
                if (checkDelete == null)
                {
                    return NoContent();
                }
                else {
                    return NotFound();
                }
            }
        }
    }
}