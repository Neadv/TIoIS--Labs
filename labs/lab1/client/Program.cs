using Grpc.Net.Client;

using Lab1Client;
using Lab1Client.Helpers;

int fromNumber = ConsoleHelpers.GetIntInput("Enter the first number:");
int toNumber = ConsoleHelpers.GetIntInput("Enter the second number. It must be greater than or equal to the first number:", (num) => num >= fromNumber);

Console.WriteLine("Sending the request...");
using var channel = GrpcChannel.ForAddress("http://localhost:5167");
var client = new Calculator.CalculatorClient(channel);
var reply = await client.CalculateSumBetweenAsync(new CalculationRequest { From = fromNumber, To = toNumber });
Console.WriteLine($"GRPC Server answer is {reply.Sum}");
