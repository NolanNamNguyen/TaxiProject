using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Domain.Interfaces
{
    public interface IForumRepository
    {
        //review 
        IEnumerable<Review> GetAllReviews();
        Review GetReviewById(int reviewId);
        Review CreateReview(Review review);
        void DeleteReview(Review review);

        //like review
        void LikeReview(Like like);
        void UnlikeReview(Like like);

        //comment review
        IEnumerable<Comment> GetCommentsOfReview(int reviewId);
        Comment AddComment(Comment comment);
        void DeleteComment(int cmtId);

        //helper method
        int getCustomerId(int userId);
        int getDriverId(int userId);
        int getUserIdInCmt(int cmtId);
    }
}
