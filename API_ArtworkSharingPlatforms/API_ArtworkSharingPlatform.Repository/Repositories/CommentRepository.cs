using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly Artwork_SharingContext _context;
        private readonly Artwork_SharingContext _context2;
        public CommentRepository(Artwork_SharingContext context, Artwork_SharingContext context2)
        {
            this._context = context;
            this._context2 = context2;

        }
        public async Task<ResponeModel> CreateComment(CreateComment createComment)
        {
            try
            {
                var Comment = new Comment
                {
                    ContentComment = createComment.ContentComment,
                    DateSub = DateTime.Now,
                    UserId = createComment.UserId,
                    ArtworkPId = createComment.ArtworkPId,
                    IsLikePost = false,  
                    Status = true,
                };
                _context.Comments.Add(Comment);
                await _context.SaveChangesAsync();

                // create notification
                var fullName = await _context.People
                    .Where(c => c.Id == createComment.UserId)
                    .Select(x => x.FullName).FirstOrDefaultAsync();
                var userIdReceive = await _context2.Artworks
                    .Where(c=>c.ArtworkPId==createComment.ArtworkPId)
                    .Select(c=> c.UserId).FirstOrDefaultAsync();
                var notification = new Notification
                {
                    UserId = createComment.UserId,
                    ContentNoti = $"{fullName} - commented on the post",
                    DateNoti = DateTime.Now,
                    Status = true,
                    UserIdReceive= userIdReceive,
                };
                _context2.Notifications.Add(notification);
                await _context2.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Created Comment successfully", DataObject = Comment };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while Creating the comment" };
            }
        }
        public async Task<ResponeModel> CreateInteract(CreateInteract createInteract)
        {
            try
            {
                var Interact = new Comment
                {
                    ContentComment = null,
                    DateSub = DateTime.Now,
                    UserId = createInteract.UserId,
                    ArtworkPId = createInteract.ArtworkPId,
                    IsLikePost = true,
                    Status = true,
                };
                _context.Comments.Add(Interact);
                await _context.SaveChangesAsync();

                // create notification
                var fullName = await _context.People
                    .Where(c => c.Id == createInteract.UserId)
                    .FirstOrDefaultAsync();
                var userIdReceive = await _context2.Artworks
                    .Where(c => c.ArtworkPId == createInteract.ArtworkPId)
                    .FirstOrDefaultAsync();
                if (fullName.Id== userIdReceive.UserId) 
                {
                    var notification = new Notification
                    {
                        UserId = createInteract.UserId,
                        ContentNoti = "You liked your own post",
                        DateNoti = DateTime.Now,
                        Status = true,
                        UserIdReceive = userIdReceive.UserId,
                    };
                    _context2.Notifications.Add(notification);
                    await _context2.SaveChangesAsync();
                }
                else
                {
                    var notification = new Notification
                    {
                        UserId = createInteract.UserId,
                        ContentNoti = $"{fullName.FullName} - liked the article",
                        DateNoti = DateTime.Now,
                        Status = true,
                        UserIdReceive = userIdReceive.UserId,
                    };
                    _context2.Notifications.Add(notification);
                    await _context2.SaveChangesAsync();
                }

                return new ResponeModel { Status = "Success", Message = "Created Interact successfully", DataObject = Interact };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while Creating the interact" };
            }
        }
        public async Task<ResponeModel> DisLike(int commentId)
        {
            try
            {
                var existingComment = await _context.Comments.FirstOrDefaultAsync(a => a.CommentId == commentId);

                if (existingComment == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Like not found" };
                }

               // existingComment = HideOrShowInteract(existingComment);
                _context.Comments.Remove(existingComment);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Like deleted successfully", DataObject = existingComment };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while detele Like" };
            }
        }
        //private Comment HideOrShowInteract(Comment comment)
        //{
        //    if (comment.IsLikePost == true)
        //    {
        //        comment.IsLikePost = false;
        //    }
        //    else
        //    {
        //        comment.IsLikePost = true;
        //    }
        //    return comment;
        //}

        public async Task<ResponeModel> HideOrShowCommentById(int commentId)
        {
            try
            {
                var existingComment = await _context.Comments.FirstOrDefaultAsync(a => a.CommentId == commentId);

                if (existingComment == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Comment not found" };
                }

                existingComment = HideOrShowComment(existingComment);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Comment deleted successfully", DataObject = existingComment };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while detele comment" };
            }
        }
        private Comment HideOrShowComment(Comment comment)
        {
            if (comment.Status == true)
            {
                comment.Status = false;
            }
            else
            {
                comment.Status = true;
            }
            return comment;
        }

        public async Task<ResponeModel> CountInteractByArtworkId(int artworkId)
        {  
            var ArtworkId = await _context.Comments
                .Where(c => c.ArtworkPId == artworkId && c.ContentComment==null && c.IsLikePost == true).ToListAsync();
            if (ArtworkId.Count > 0)
            {
                int SumLike = ArtworkId.Count();
                return new ResponeModel { Status = "Success", Message = "Interact found", DataObject = SumLike };
            }
            else if (ArtworkId.Count <=0)
            {
                return new ResponeModel { Status = "Success", Message = "Interact not found", DataObject =0 };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Something Wrong", DataObject = 0 };
            }
        }

        public async Task<ResponeModel> UpdateComment(UpdateCommentModel updateComment, int commentId)
        {
            try
            {
                var existingComment = await _context.Comments
                    .FirstOrDefaultAsync(a => a.CommentId == commentId && a.ContentComment !=null && a.IsLikePost ==false);

                if (existingComment == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Comment not found" };
                }

                existingComment = submitCommentChanges(existingComment, updateComment);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Comment deleted successfully", DataObject = existingComment };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while detele comment" };
            }
        }
        private Comment submitCommentChanges(Comment comment, UpdateCommentModel updateComment)
        {
            comment.ContentComment = updateComment.ContentComment;
            return comment;
        }
        public async Task<ResponeModel> CountCommentByArtworkId(int artworkId)
        {
            var ArtworkId = await _context.Comments
                .Where(c => c.ArtworkPId == artworkId && c.ContentComment != null && c.IsLikePost == false).ToListAsync();
            if (ArtworkId.Count > 0)
            {
                int SumComment = ArtworkId.Count();
                return new ResponeModel { Status = "Success", Message = "Interact found", DataObject = SumComment };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Interact not found", DataObject = 0 };
            }
        }
        public async Task<ResponeModel> GetUseLikerOrNotLike(string userId, int artworkId)
        {
            var foundInteract = await _context.Comments
                      .Where(c => c.ArtworkPId == artworkId && c.ContentComment == null && c.IsLikePost == true && c.UserId == userId)
                      .FirstOrDefaultAsync();
             bool likeOrNotLike = false;

            if (foundInteract != null)
            {
                likeOrNotLike = true;
                return new ResponeModel { Status = "Success", Message = "Interact found", DataObject = new { likeOrNotLike = likeOrNotLike, Id = foundInteract.CommentId } };
            }
            else if (foundInteract == null) 
            {
                // Trường hợp foundInteract bằng null cũng trả về mã trạng thái 200
                return new ResponeModel { Status = "Success", Message = "Interact not found", DataObject = new { likeOrNotLike = likeOrNotLike } };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Something Wrong", DataObject = new { likeOrNotLike = likeOrNotLike } };
            }
        }
        public async Task<ResponeModel> GetCommentForPost(int artworkId)
        {
            //var comment = await _context.Comments
            //    .Where(c => c.ArtworkPId == artworkId && c.ContentComment != null && c.IsLikePost == false && c.Status==true)
            //    .ToListAsync();

            var commentsWithUserInfo = await _context.Comments
                 .Where(c => c.ArtworkPId == artworkId && c.ContentComment != null && c.IsLikePost == false && c.Status == true)
                 .Join(
                       _context.People,
                       comment => comment.UserId,
                       person => person.Id,
                       (comment, person) => new
                     {
                         CommentId = comment.CommentId,
                         ContentComment = comment.ContentComment,
                         DateSub = comment.DateSub,
                         UserId = comment.UserId,
                         ArtworkPId = comment.ArtworkPId,
                         IsLikePost = comment.IsLikePost,
                         Status = comment.Status,
                           // Include additional fields from Person table
                         accountMail = person.UserName,
                         avatar = person.Avatar,
                    }
                      )
                    .ToListAsync();
            if (commentsWithUserInfo.Count > 0)
            {
                
                return new ResponeModel { Status = "Success", Message = "Comment found", DataObject = commentsWithUserInfo };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Comment not found", DataObject = commentsWithUserInfo };
            }
        }
    }
}
