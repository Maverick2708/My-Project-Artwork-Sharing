using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Repository.Repositories;
using API_ArtworkSharingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this._commentRepository = commentRepository;
        }

        public async Task<ResponeModel> CreateComment(CreateComment createComment)
        {
            return await _commentRepository.CreateComment(createComment);
        }
        public async Task<ResponeModel> CreateInteract(CreateInteract createInteract)
        {
            return await _commentRepository.CreateInteract(createInteract);
        }
        public async Task<ResponeModel> DisLike(int commentId)
        {
            return await _commentRepository.DisLike(commentId);
        }
        public async Task<ResponeModel> HideOrShowCommentById(int commentId)
        {
            return await _commentRepository.HideOrShowCommentById(commentId);
        }
        public async Task<ResponeModel> CountInteractByArtworkId(int artworkId)
        {
            return await _commentRepository.CountInteractByArtworkId(artworkId);
        }
        public async Task<ResponeModel> UpdateComment(UpdateCommentModel updateComment, int commentId)
        {
            return await _commentRepository.UpdateComment(updateComment, commentId);
        }
        public async Task<ResponeModel> CountCommentByArtworkId(int artworkId)
        {
            return await _commentRepository.CountCommentByArtworkId(artworkId);
        }
        public async Task<ResponeModel> GetUseLikerOrNotLike(string userId, int artworkId)
        {
            return await _commentRepository.GetUseLikerOrNotLike(userId,artworkId);
        }
        public async Task<ResponeModel> GetCommentForPost(int artworkId)
        {
            return await _commentRepository.GetCommentForPost(artworkId);
        }
    }
}
