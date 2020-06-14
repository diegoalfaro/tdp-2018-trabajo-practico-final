﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;
using Service.Common;
using Service.Connectors;
using Service.DTO;
using Service.Interfaces;

namespace Service
{
    public class ApiService: IApiService
    {
        private Connector Connector { get; set; }

        public ApiService(string pBaseURL)
        {
            Connector = new Connector(pBaseURL);
        }

        public async Task<Client> GetClientInfo(int pClientId, int pPassword)
        {
            Request request = new Request {
                Method = Method.GET,
                Path = "clients",
                Params = new KeyValuePair<string, string>[]{
                     new KeyValuePair<string, string>("id", pClientId.ToString()),
                     new KeyValuePair<string, string>("password", pPassword.ToString())
                }
            };

            Response response = await Connector.SendAsync<Response>(request);
            List<ClientDTO> dto = JsonConvert.DeserializeObject<List<ClientDTO>>(response.Body);

            if (dto != null && dto.Count > 0) return dto[0];
            else return null;
        }

        public async Task<List<Product>> GetProductsByClientID(int pClientId)
        {
            Request request = new Request
            {
                Method = Method.GET,
                Path = "products",
                Params = new KeyValuePair<string, string>[]{
                     new KeyValuePair<string, string>("id", pClientId.ToString())
                }
            };

            Response response = await Connector.SendAsync<Response>(request);
            List<ProductListDTO> dto = JsonConvert.DeserializeObject<List<ProductListDTO>>(response.Body);

            if (dto != null && dto.Count > 0 && dto[0].Products.Count > 0) return (from product in dto[0].Products select product as Product).ToList();
            else return null;
        }

        public async Task<AccountBalance> GetBalanceByClientID(int pClientId)
        {
            Request request = new Request
            {
                Method = Method.GET,
                Path = "account-balance",
                Params = new KeyValuePair<string, string>[]{
                     new KeyValuePair<string, string>("id", pClientId.ToString())
                }
            };

            Response response = await Connector.SendAsync<Response>(request);
            List<AccountBalanceDTO> dto = JsonConvert.DeserializeObject<List<AccountBalanceDTO>>(response.Body);

            if (dto != null && dto.Count > 0) return dto[0];
            else return null;
        }

        public async Task<List<AccountMovement>> GetMovementsByClientID(int pClientId)
        {
            Request request = new Request
            {
                Method = Method.GET,
                Path = "account-movements",
                Params = new KeyValuePair<string, string>[]{
                     new KeyValuePair<string, string>("id", pClientId.ToString())
                }
            };

            Response response = await Connector.SendAsync<Response>(request);
            List<AccountMovementListDTO> dto = JsonConvert.DeserializeObject<List<AccountMovementListDTO>>(response.Body);

            if (dto != null && dto.Count > 0 && dto[0].Movements.Count > 0) return (from movement in dto[0].Movements select movement as AccountMovement).ToList();
            else return null;
        }

        public async Task<ProductReset> ResetProductByProductNumber(string pProductNumber)
        {
            Request request = new Request
            {
                Method = Method.GET,
                Path = "product-reset",
                Params = new KeyValuePair<string, string>[]{
                     new KeyValuePair<string, string>("number", pProductNumber.ToString())
                }
            };

            Response response = await Connector.SendAsync<Response>(request);
            List<ProductResetDTO> dto = JsonConvert.DeserializeObject<List<ProductResetDTO>>(response.Body);

            if (dto != null && dto.Count > 0) return dto[0];
            else return null;
        }
    }
}