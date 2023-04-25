using Nest;
using System;

namespace Capstone.Models
{
    public class Review
    {
        public int Review_ID { get; set; }
        public string Reviewable_ID { get; set; }
        public string Rating { get; set; }
        public string ReviewerUsername { get; set; }
        public int Reviewer_ID { get; set; }
        public string ReviewText { get; set; }

        public IntReview ConvertToIntReview(Review rev)
        {
            IntReview intR = new IntReview()
            {
                Review_ID = rev.Review_ID,
                Reviewable_ID = Convert.ToInt32(rev.Reviewable_ID),
                Rating = Convert.ToInt32(rev.Rating),
                ReviewerUsername = rev.ReviewerUsername,
                Reviewer_ID = rev.Reviewer_ID,
                ReviewText = rev.ReviewText
            };

            return intR;
        }
    }
    public class IntReview
    {
        public int Review_ID { get; set; }
        public int Reviewable_ID { get; set; }
        public int Rating { get; set; }
        public string ReviewerUsername { get; set; }
        public int Reviewer_ID { get; set; }
        public string ReviewText { get; set; }
    }
}
