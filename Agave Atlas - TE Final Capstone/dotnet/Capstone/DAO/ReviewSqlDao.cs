using Capstone.DAO.Interfaces;
using Capstone.Models;
using Nest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class ReviewSqlDao :IReviewDao
    {
        private readonly string connectionString;
        private readonly IUserDao userDao;

        public ReviewSqlDao(string connString)
        {
            connectionString = connString;
            this.userDao = new UserSqlDao(connectionString);
        }
        //public ReviewSqlDao(string connString) {
        //    connectionString = connString;
        //}
        public Review GetReviewByID(int reviewID)
        {
            Review rev = new Review();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE review_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", reviewID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        rev = CreateReviewFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return rev;
        }
        public IList<Review> GetRestaurantReviews(int restID)
        {
            List<Review> reviews = new List<Review>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM reviews 
                                                     JOIN restaurant_review ON reviews.review_id = restaurant_review.review_id
                                                     JOIN restaurants ON restaurants.restaurant_id = restaurant_review.restaurant_id
                                                     WHERE restaurants.restaurant_id = @id", conn);

                    cmd.Parameters.AddWithValue("@id", restID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        reviews.Add(CreateReviewFromReader(reader, restID));
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Error getting reviews for Restaurant ID {restID}");
            }
            return reviews;
        }
        public IList<Review> GetReviewsByUser(int userID)
        {
            List<Review> reviews = new List<Review>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM reviews WHERE user_id = @id", conn);

                    cmd.Parameters.AddWithValue("@id", userID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reviews.Add(CreateReviewFromReader(reader));
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Error getting reviews for User ID {userID}");
            }
            return reviews;
        }
        public IList<Review> GetDrinkReviews(int drinkID)
        {
            List<Review> reviews = new List<Review>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM reviews 
                                                     JOIN drink_review ON reviews.review_id = drink_review.review_id
                                                     WHERE drink_review.drink_id = @id", conn);

                    cmd.Parameters.AddWithValue("@id", drinkID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reviews.Add(CreateReviewFromReader(reader));
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Error getting reviews for Restaurant ID {drinkID}");
            }
            return reviews;
        }
        public Review AddRestaurantReview(Review rev)
        {
            IntReview review = rev.ConvertToIntReview(rev);
            review.Reviewer_ID = userDao.GetUser(review.ReviewerUsername).UserId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@" BEGIN TRANSACTION
                    DECLARE @inserted TABLE ([ID] int);
                    INSERT INTO reviews (user_id, rating, review_text)
                    OUTPUT INSERTED.review_id INTO @inserted
                    VALUES (@user_id, @rating, @review_text);
                    INSERT INTO restaurant_review (review_id, restaurant_id) 
                    VALUES ((SELECT TOP 1 ID FROM @inserted ORDER BY ID DESC), @restaurant_id);
                    SELECT TOP 1 ID FROM @inserted ORDER BY ID DESC;
                    COMMIT;", conn);

                    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(review.Reviewer_ID));
                    cmd.Parameters.AddWithValue("@rating", Convert.ToInt32(review.Rating));
                    cmd.Parameters.AddWithValue("@review_text", review.ReviewText);
                    cmd.Parameters.AddWithValue("@restaurant_id", Convert.ToInt32(review.Reviewable_ID));

                    int reviewID = Convert.ToInt32(cmd.ExecuteScalar());
                    rev = GetReviewByID(reviewID);
                    rev.ReviewerUsername = userDao.GetUserByID(rev.Reviewer_ID).Username;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Error adding review.");
            }
            return rev;
        }
        public bool DeleteRestaurantReview(int reviewID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"BEGIN TRANSACTION
                    DELETE FROM restaurant_review WHERE review_id = @id;
                    DELETE FROM reviews WHERE review_id = @id;
                    COMMIT;", conn);

                    cmd.Parameters.AddWithValue("@id", reviewID);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"Error deleting Review {reviewID}");
                return false;
            }
        }
        public Review CreateReviewFromReader(SqlDataReader read, int reviewableID)
        {
            Review rev = new Review()
            {
                Review_ID = Convert.ToInt32(read["review_id"]),
                Reviewable_ID = Convert.ToString(reviewableID),
                Rating = Convert.ToString(read["rating"]),
                ReviewerUsername = userDao.GetUserByID(Convert.ToInt32(read["user_id"])).Username,
                Reviewer_ID = Convert.ToInt32(read["user_id"]),
                ReviewText = Convert.ToString(read["review_text"])
            };
            return rev;
        }
        public Review CreateReviewFromReader(SqlDataReader read)
        {
            Review rev = new Review()
            {
                Review_ID = Convert.ToInt32(read["review_id"]),
                Reviewable_ID = "0",
                Rating = Convert.ToString(read["rating"]),
                ReviewerUsername = userDao.GetUserByID(Convert.ToInt32(read["user_id"])).Username,
                Reviewer_ID = Convert.ToInt32(read["user_id"]),
                ReviewText = Convert.ToString(read["review_text"])
            };
            return rev;
        }
    }
}
