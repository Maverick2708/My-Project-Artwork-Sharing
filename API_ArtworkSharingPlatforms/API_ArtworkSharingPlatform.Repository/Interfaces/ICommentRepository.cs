﻿using API_ArtworkSharingPlatform.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Interfaces
{
    public interface ICommentRepository
    {
        public Task<ResponeModel> CreateComment(CreateComment createComment);
        public Task<ResponeModel> CreateInteract(CreateInteract createInteract);
        public Task<ResponeModel> LikeOrUnLike(int commentId);
        public Task<ResponeModel> HideOrShowCommentById(int commentId);
        public Task<ResponeModel> CountInteractByArtworkId(int artworkId);
        public Task<ResponeModel> CountCommentByArtworkId(int artworkId);
        public Task<ResponeModel> UpdateComment(UpdateCommentModel updateComment, int commentId);
        public Task<ResponeModel> GetUseLikerOrNotLike(string userId,int artworkId);
        public Task<ResponeModel> GetCommentForPost(int artworkId);
    }
}