using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LiftBuddy.Models;
using LiftBuddy.Models.Requests;
using RestSharp;

namespace LiftBuddy.Web.Clients
{
    public class UserClient
    {
        private readonly IRestClient _restClient;
        
        public UserClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<List<User>> GetAll()
        {
            var request = new RestRequest("users");
            var response = await _restClient.ExecuteAsync<List<User>>(request);
            return response.Data;
        }
        
        public async Task<User> Get(string id)
        {
            var request = new RestRequest($"users/{id}");
            var response = await _restClient.ExecuteAsync<User>(request);
            return response.Data;
        }
        
        public async Task<User> Create(UserCreateRequest user)
        {
            var request = new RestRequest("users", Method.POST);
            request.AddJsonBody(user);
            var response = await _restClient.ExecuteAsync<User>(request);
            return response.Data;
        }
        
        public async Task<User> Update(string id, UserUpdateRequest updateRequest)
        {
            var request = new RestRequest($"users/{id}", Method.PUT);
            request.AddJsonBody(updateRequest);
            var response = await _restClient.ExecuteAsync<User>(request);
            return response.Data;
        }
        
        public async Task Delete(string id)
        {
            var request = new RestRequest($"users/{id}", Method.DELETE);
            await _restClient.ExecuteAsync(request);
        }
    }
}