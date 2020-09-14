using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taxi.Domain.Entities;
using Taxi.Domain.Helpers;
using Taxi.Domain.Interfaces;
using Taxi.Domain.Models.Forum;

namespace Taxi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private IForumRepository _forumService;
        private IMapper _mapper;

        public ForumController(
            IForumRepository forumService,
            IMapper mapper)
        {
            _forumService = forumService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get posts list
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("reviews")]
        public IActionResult GetAllReviews()
        {
            var reviews = _forumService.GetAllReviews();
            var model = _mapper.Map<IList<ReviewModel>>(reviews);
            return Ok(model);
        }

        /// <summary>
        /// get a post by reviewId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("reviews/{id}")]
        public IActionResult GetReview(int id)
        {
            var review = _forumService.GetReviewById(id);
            var model = _mapper.Map<ReviewModel>(review);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        /// <summary>
        /// post review
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("reviews")]
        public IActionResult PostReview([FromBody] AddReviewModel model)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var review = _mapper.Map<Review>(model);
            if (User.IsInRole(Role.Customer))
                review.CustomerId = _forumService.getCustomerId(currentUserId);
            if (User.IsInRole(Role.Driver))
                review.DriverId = _forumService.getDriverId(currentUserId);

            try
            {
                _forumService.CreateReview(review);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { mesasge = ex.Message });
            }
        }

        /// <summary>
        /// Delete a review(post) by reviewId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("reviews/{id}")]
        public IActionResult DeleteReview(int id)
        {
            var review = _forumService.GetReviewById(id);
            var currentUserId = int.Parse(User.Identity.Name);
            if (User.IsInRole(Role.Customer))
            {
                if (_forumService.getCustomerId(currentUserId) != review.CustomerId)
                    return Forbid();
            }
            if (User.IsInRole(Role.Driver))
            {
                if (_forumService.getDriverId(currentUserId) != review.DriverId)
                    return Forbid();
            }
            try
            {
                _forumService.DeleteReview(review);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //like 

        /// <summary>
        /// like a review
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("likes")]
        public IActionResult LikeReview([FromBody] AddLikeModel model)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var like = _mapper.Map<Like>(model);
            like.UserId = currentUserId;
            try
            {
                _forumService.LikeReview(like);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Unlike a review
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("likes")]
        public IActionResult UnlikeReview([FromBody] AddLikeModel model)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var like = _mapper.Map<Like>(model);
            like.UserId = currentUserId;
            try
            {
                _forumService.UnlikeReview(like);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //comment

        /// <summary>
        /// Get a comments list of a post(review) by reviewId 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("comments/{id}")]
        public IActionResult GetAllCommentOfReview(int id)
        {
            var cmt = _forumService.GetCommentsOfReview(id);
            var model = _mapper.Map<IList<CommentModel>>(cmt);
            return Ok(model);
        }

        /// <summary>
        /// comment to a post(review)
        /// </summary>
        /// <remarks>
        ///     by reviewId
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("comments/{id}")]
        public IActionResult AddCommentToReview(int id, [FromBody] AddCommentModel model)
        {

            var cmt = _mapper.Map<Comment>(model);
            cmt.UserId = int.Parse(User.Identity.Name);
            cmt.ReviewId = id;
            try
            {
                _forumService.AddComment(cmt);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete a comment by commentId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("comments/{id}")]
        public IActionResult DeleteComment(int id)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (!User.IsInRole(Role.Admin) && currentUserId != _forumService.getUserIdInCmt(id))
                return Forbid();
            try
            {
                _forumService.DeleteComment(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
