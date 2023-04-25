using Capstone.Models;
using System.Collections;
using System.Collections.Generic;

namespace Capstone.DAO.Interfaces
{
    public interface IReviewDao
    {
        IList<Review> GetRestaurantReviews(int restID);
        IList<Review> GetReviewsByUser(int userID);
        IList<Review> GetDrinkReviews(int drinkID);
        Review AddRestaurantReview(Review review);
        Review GetReviewByID(int id);
        bool DeleteRestaurantReview(int reviewID);
    }
}
