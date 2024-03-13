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
    public class RequestRepository : IRequestRepository
    {
        private readonly Artwork_SharingContext _context;

        public RequestRepository(Artwork_SharingContext context)
        {
            this._context = context;
        }
        public async Task<ResponeModel> CreateRequest(CreateRequestModel createRequestModel)
        {
            try
            {
               var countFollow = await _context.Follows
                .Where(c => c.UserIdFollow == createRequestModel.UserId).ToListAsync();
                if(countFollow.Count >= 10) 
                {
                    var request = new Request
                    {
                        UserId = createRequestModel.UserId,
                        Description = "I want to increase my credibility",
                        Status = true,
                    };
                    _context.Requests.Add(request);
                    await _context.SaveChangesAsync();
                    return new ResponeModel { Status = "Success", Message = "Created Request successfully", DataObject = request };
                }
                else
                {
                    return new ResponeModel { Status = "Error", Message = "Your following is not over 10" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while Creating the request" };
            }
        }
        public async Task<ResponeModel> GetAllRequest()
        {
            var getRequest = await _context.Requests.ToListAsync();
            if (getRequest != null)
            {
                return new ResponeModel { Status = "Success", Message = "Found Request", DataObject = getRequest };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Request not found" };
            }
        }
        public async Task<ResponeModel> CountRequest()
        {
            var getRequest = await _context.Requests.Where(c=> c.Status==true).ToListAsync();
            if (getRequest.Count > 0)
            {
                int countRequest = (int) getRequest.Count();
                return new ResponeModel { Status = "Success", Message = "Found Request", DataObject = countRequest };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Request not found", DataObject = 0 };
            }
        }
        public async Task<ResponeModel> UpdateStatusRequest(int requestId)
        {
            try
            {
                var existingRequest = await _context.Requests.FirstOrDefaultAsync(a => a.RequestId == requestId && a.Status==true);

                if (existingRequest == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Request not found" };
                }

                existingRequest = HideRequest(existingRequest);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Request deleted successfully", DataObject = existingRequest };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while detele request" };
            }
        }
        private Request HideRequest(Request request)
        {
            request.Status = false;
            return request;
        }

        public async Task<ResponeModel> GetAllRequestByUserID(string userID)
        {
            var getRequest = await _context.Requests.Where(c=> c.UserId== userID).ToListAsync();
            if (getRequest != null)
            {
                return new ResponeModel { Status = "Success", Message = "Found Request", DataObject = getRequest };
            }
            else
            {
                return new ResponeModel { Status = "Success", Message = "Request not found" };
            }
        }
    }
}
