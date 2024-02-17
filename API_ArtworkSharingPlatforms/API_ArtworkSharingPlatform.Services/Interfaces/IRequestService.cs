using API_ArtworkSharingPlatform.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Interfaces
{
    public interface IRequestService
    {
        public Task<ResponeModel> CreateRequest(CreateRequestModel createRequestModel);
        public Task<ResponeModel> GetAllRequest();
        public Task<ResponeModel> CountRequest();
        public Task<ResponeModel> UpdateStatusRequest(int requestId);
    }
}
