﻿using API_ArtworkSharingPlatform.Repository.Data;
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

        public CommentRepository(Artwork_SharingContext context)
        {
            this._context = context;
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

                return new ResponeModel { Status = "Success", Message = "Created Interact successfully", DataObject = Interact };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while Creating the interact" };
            }
        }
        public async Task<ResponeModel> LikeOrUnLike(int commentId)
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
            else
            {
                return new ResponeModel { Status = "Error", Message = "Interact not found" };
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
                return new ResponeModel { Status = "Error", Message = "Interact not found" };
            }
        }
        public async Task<ResponeModel> GetUseLikerOrNotLike(string userId, int artworkId)
        {
            var foundInteract = await _context.Comments
                .Where(c => c.ArtworkPId == artworkId && c.ContentComment == null && c.IsLikePost == true && c.UserId==userId)
                .ToListAsync();
            bool likeOrNotLike = false;
            if (foundInteract.Count > 0)
            {
                likeOrNotLike = true;
                return new ResponeModel { Status = "Success", Message = "Interact found", DataObject = likeOrNotLike };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Interact not found", DataObject = likeOrNotLike };
            }
        }
        public async Task<ResponeModel> GetCommentForPost(int artworkId)
        {
            var comment = await _context.Comments
                .Where(c => c.ArtworkPId == artworkId && c.ContentComment != null && c.IsLikePost == false && c.Status==true)
                .ToListAsync();
            if (comment.Count > 0)
            {
                
                return new ResponeModel { Status = "Success", Message = "Comment found", DataObject = comment };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Comment not found", DataObject = comment };
            }
        }
    }
}