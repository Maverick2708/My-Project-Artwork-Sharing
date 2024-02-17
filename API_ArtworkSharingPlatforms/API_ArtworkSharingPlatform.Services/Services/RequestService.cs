using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            this._requestRepository = requestRepository;
        }
        public async Task<ResponeModel> CreateRequest(CreateRequestModel createRequestModel)
        {
            return await _requestRepository.CreateRequest(createRequestModel);
        }
        public async Task<ResponeModel> GetAllRequest()
        {
            return await _requestRepository.GetAllRequest();
        }
        public async Task<ResponeModel> CountRequest()
        {
            return await _requestRepository.CountRequest();
        }
        public async Task<ResponeModel> UpdateStatusRequest(int requestId)
        {
            return await _requestRepository.UpdateStatusRequest(requestId);
        }
    }
}
