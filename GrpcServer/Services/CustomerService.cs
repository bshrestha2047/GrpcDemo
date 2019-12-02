using Grpc.Core;
using GrpcServer.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Bikash";
                output.LastName = "Shrestha";
                output.EmailAddress = "Test@testmail.com";
                output.IsAlive = true;
                output.Age = DateTime.Now.Year - 1990;
            }
            else if (request.UserId == 1)
            {
                output.FirstName = "Jon";
                output.LastName = "Doe";
                output.EmailAddress = "Test@testmail.com";
                output.IsAlive = true;
                output.Age = DateTime.Now.Year - 1990;
            }
            else
            {
                output.FirstName = "John";
                output.LastName = "Smith";
                output.EmailAddress = "Test@testmail.com";
                output.IsAlive = true;
                output.Age = DateTime.Now.Year - 1990;
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Bikash",
                    LastName = "Shrestha",
                    EmailAddress = "Test@testmail.com",
                    IsAlive = true,
                    Age = DateTime.Now.Year - 1990
                },
                new CustomerModel
                {
                    FirstName = "Bidhya",
                    LastName = "KC",
                    EmailAddress = "Test@testmail.com",
                    IsAlive = true,
                    Age = DateTime.Now.Year - 1991
                },
                new CustomerModel
                {
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "Test@testmail.com",
                    IsAlive = true,
                    Age = DateTime.Now.Year - 1992
                },
            };

            foreach (var cust in customers)
            {
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
