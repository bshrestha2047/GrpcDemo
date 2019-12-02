using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using GrpcServer.Protos;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(new HelloRequest { Name = "Bikash" });

            //Console.WriteLine(reply.Message);

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var customerClient = new Customer.CustomerClient(channel);

            var customer = await customerClient.GetCustomerInfoAsync(new CustomerLookupModel { UserId = 2 });

            Console.WriteLine($"First Name: {customer.FirstName} \nLast Name: {customer.LastName} \nAge: {customer.Age} years \nEmail Address: {customer.EmailAddress} \nIs Alive: {customer.IsAlive}");
            Console.WriteLine("----------------------");
            Console.WriteLine("");
            Console.WriteLine("New Customer List");
            Console.WriteLine("----------------------");
            Console.WriteLine("");
            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"First Name: {currentCustomer.FirstName} \nLast Name: {currentCustomer.LastName} \nAge: {currentCustomer.Age} years \nEmail Address: {currentCustomer.EmailAddress} \nIs Alive: {currentCustomer.IsAlive}");
                    Console.WriteLine("----------------------");
                }
            }

            Console.ReadLine();
        }
    }
}
