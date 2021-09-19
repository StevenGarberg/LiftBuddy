using System.Collections.Generic;
using System.Threading.Tasks;
using LiftBuddy.Models;
using RestSharp;

namespace LiftBuddy.Web.Clients
{
    public class UserClient : IClient<User>
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
        
        public async Task<User> Create(object requestBody)
        {
            var request = new RestRequest("users", Method.POST);
            request.AddJsonBody(requestBody);
            var response = await _restClient.ExecuteAsync<User>(request);
            return response.Data;
        }
        
        public async Task<User> Update(string id, object requestBody)
        {
            var request = new RestRequest($"users/{id}", Method.PUT);
            request.AddJsonBody(requestBody);
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