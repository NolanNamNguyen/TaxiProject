using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Taxi.Domain.Entities;
using Taxi.Domain.Interfaces;
using Taxi.Infrastructure.Data;

namespace Taxi.Infrastructure.Services
{
    public class ForumService : IForumRepository
    {
        private TaxiContext _context;

        public ForumService(TaxiContext context)
        {
            _context = context;
        }

        //review (post)
        public IEnumerable<Review> GetAllReviews()
        {
            return _context.Reviews.OrderByDescending(e => e.Created);
        }
        public Review GetReviewById(int reviewId)
        {
            return _context.Reviews.Find(reviewId);
        }
        public Review CreateReview(Review review)
        {
            review.Created = DateTime.Now;

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return review;
        }
        public void DeleteReview(Review review)
        {
            if (review != null)
            {
                var likes = review.Likes;
                if (likes != null)
                {
                    foreach (var like in likes)
                        _context.Remove(like);
                }
                var cmts = review.Comments;
                if (cmts != null)
                {
                    foreach (var cmt in cmts)
                        _context.Remove(cmt);
                }

                _context.Reviews.Remove(review);
            }
            _context.SaveChanges();
        }

        //like
        public void LikeReview(Like like)
        {
            like.Created = DateTime.Now;
            var _like = _context.Likes.SingleOrDefault(e => e.ReviewId == like.ReviewId && e.UserId == like.UserId);
            if (_like == null)
            {
                _context.Likes.Add(like);
                _context.SaveChanges();
            }

        }
        //delete
        public void UnlikeReview(Like like)
        {
            var _like = _context.Likes.SingleOrDefault(e => e.ReviewId == like.ReviewId && e.UserId == like.UserId);
            if (_like != null)
            {
                _context.Likes.Remove(_like);
                _context.SaveChanges();
            }
        }

        //cmt review
        public IEnumerable<Comment> GetCommentsOfReview(int reviewId)
        {
            return _context.Comments.Where(e => e.ReviewId == reviewId).OrderByDescending(e => e.Created);
        }
        public Comment AddComment(Comment comment)
        {
            comment.Created = DateTime.Now;

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return comment;
        }
        public void DeleteComment(int cmtId)
        {
            var cmt = _context.Comments.Find(cmtId);
            if (cmt != null)
            {
                _context.Comments.Remove(cmt);
                _context.SaveChanges();
            }
        }

        //helper method
        public int getCustomerId(int userId)
        {
            return _context.Customers.SingleOrDefault(e => e.UserId == userId).CustomerId;
        }
        public int getDriverId(int userId)
        {
            return _context.Drivers.SingleOrDefault(e => e.UserId == userId).DriverId;
        }
        public int getUserIdInCmt(int cmtId)
        {
            return _context.Comments.Find(cmtId).UserId;
        }
    }
}
