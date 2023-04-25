using System.Collections;
using System.Collections.Generic;

namespace Capstone.Models
{
    public interface IReviewable
    {
        int ID { get; set; }
        string Name { get; set; }
        IList<Review> Reviews { get; set; }
    }
}
